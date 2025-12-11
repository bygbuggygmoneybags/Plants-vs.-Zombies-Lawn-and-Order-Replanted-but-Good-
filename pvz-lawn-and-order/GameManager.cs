using Godot;
using System;
using System.Collections.Generic;

namespace pvzlawnandorder
{
	public partial class GameManager : Node
	{
		public List<List<Zombie>> ZombiesInLane = new();
		public List<List<Plant>> PlantsInLane = new();
        private Dictionary<Button, PackedScene> plantButtons;
        [Export] public PackedScene Sun { get; set; }
		[Export] public PackedScene PlantFood { get; set; }
		public PackedScene PlantScene { get; set; }
		public PackedScene ZombieScene { get; set; }
		[Export] public PackedScene PShooter { get; set; }
		[Export] public PackedScene Repeat { get; set; }
		[Export] public PackedScene CPult { get; set; }
		[Export] public PackedScene SFlow { get; set; }
		[Export] public PackedScene WNut { get; set; }
		[Export] public PackedScene ImpScene { get; set; }
		[Export] public PackedScene BucketScene { get; set; }
		[Export] public PackedScene ConeScene { get; set; }
		[Export] public PackedScene NormalScene { get; set; }
		private Random rand = new Random();

		private Dictionary<Vector2I, Plant> plantGrid = new();
		private Timer foodTime;
		private Timer sunTime;
		private Label plantLabel;
		private Button peashooter;
		private Button sunflower;
		private Button repeater;
		private Button walnut;
		private Button cabbagepult;
		private Button SelectedPlantButton;
		public Tiles tiles;
		private Timer downTime;
		[Export] public int score = 500;
		Node sun;


		public override void _Ready()
		{
			tiles = GetNode<Tiles>("Map");

			sunTime = GetNode<Timer>("SunTimer");
			sunTime.Timeout += SpawnSky;
			foodTime = GetNode<Timer>("FoodTimer");
			foodTime.Timeout += OnTimeout;
			downTime = GetNode<Timer>("DownTimer");
			downTime.Timeout += ChooseRandomZombie;

			peashooter = GetNode<Button>("Peashooter Button");
			sunflower = GetNode<Button>("Sunflower Button");
			walnut = GetNode<Button>("Walnut Button");
			repeater = GetNode<Button>("Repeater Button");
			cabbagepult = GetNode<Button>("CabbagePult Button");

			cabbagepult.Pressed += CabbagePult;
			peashooter.Pressed += Peashooter;
			sunflower.Pressed += Sunflower;
			walnut.Pressed += Wallnut;
			repeater.Pressed += Repeater;

			plantLabel = GetNode<Label>("SelectLabel");

            plantButtons = new Dictionary<Button, PackedScene>()
{
				{ peashooter, PShooter },
				{ sunflower, SFlow },
				{ repeater, Repeat },
				{ walnut, WNut },
				{ cabbagepult, CPult }
			};

            for (int i = 0; i < 5; i++)
			{
                PlantsInLane.Add(new List<Plant>());
                ZombiesInLane.Add(new List<Zombie>());
			}
		}

        private void UpdatePlantButtons()
        {
            foreach (var kvp in plantButtons)
            {
                Button btn = kvp.Key;
                PackedScene plantScene = kvp.Value;
                int cost = GetCost(plantScene);

                btn.Disabled = score < cost;
            }
        }

        public override void _Process(double delta)
		{
			if (sun != null)
			{
				score = sun.Get("score").AsInt32();
				UpdatePlantButtons();
			}
		}

		private void SpawnSky()
		{
			var newSun = Sun.Instantiate();
			GetTree().CurrentScene.AddChild(newSun);

			newSun.Call("init_sky");

			newSun.Connect("score_changed", Callable.From<int>((sunScore) =>
			{
				score += 25;   // This updates the public Score
				GD.Print("Sun collected! Total score: ", score);
			}));
		}

		private void OnTimeout()
		{
			SpawnPlantFood(new Vector2((float)GD.RandRange(0, 8), (float)GD.RandRange(0, 4)));
		}

		public bool IsCellOccupied(Vector2I cell)
		{
			return plantGrid.ContainsKey(cell);
		}

		private void SpawnPlantFood(Vector2 worldPosition)
		{
			Area2D food = (Area2D)PlantFood.Instantiate();
			GetTree().CurrentScene.AddChild(food);

			food.GlobalPosition = worldPosition;
		}

		public void AddZombie(Zombie zomb)
		{
			ZombiesInLane[zomb.Lane].Add(zomb);
		}

		public void RemoveZombie(Zombie zomb)
		{
			ZombiesInLane[zomb.Lane].Remove(zomb);
		}

		public void AddPlant(Plant plant)
		{
			PlantsInLane[plant.Lane].Add(plant);
		}

		public void RemovePlant(Plant plant)
		{
			PlantsInLane[plant.Lane].Remove(plant);
		}

		private int GetCost(PackedScene plantScene)
		{
			int cost = int.MaxValue;
            if (plantScene == null)
                return int.MaxValue;

			var instance = plantScene.Instantiate();
			if (instance is not Plant plant)
			{
				GD.PrintErr("GetCost: PackedScene does not contain a Plant");
				return int.MaxValue;
			}
			else if (instance is Plant p)
			{
				cost = p.SunCost;
				instance.QueueFree();
			}
			return cost;
		}

		public void TryPlacePlant(Vector2I cell)
		{
            if (PlantScene == null)
                return;

            int cost = GetCost(PlantScene);

            if (score < cost)
            {
                plantLabel.Visible = true;
                plantLabel.Text = "Not enough sun!";
                ClearSelectedPlant();
                return;
            }

            if (!IsCellPlantable(cell))
            {
                plantLabel.Visible = true;
                plantLabel.Text = "Cell not plantable!";
                return;
            }

            PlacePlant(cell);
            ClearSelectedPlant();
        }

		private bool IsCellPlantable(Vector2I cell)
		{
            if (!tiles.PlantableCells.Contains(cell))
                return false;

            if (IsCellOccupied(cell))
                return false;

            return true;
        }

        public void SelectPlant(PackedScene scene)
		{
			int cost = GetCost(scene);

			if (PlantScene == scene)
			{
				PlantScene = null;
				plantLabel.Visible = false;
				return;
			}

			if (score < cost)
			{
				plantLabel.Visible = true;
				plantLabel.Text = "Not enough sun!";
				ClearSelectedPlant();
				return;
			}

			PlantScene = scene;
			plantLabel.Visible = true;
			plantLabel.Text = "Select a tile to place your plant";


		}

		public void SelectZombie(PackedScene scene)
		{
			ZombieScene = scene;
		}

		public void PlacePlant(Vector2I cell)
		{
			Plant plant = PlantScene.Instantiate<Plant>();

			plant.GameScript = this;

			Vector2 localPos = tiles.MapToLocal(cell);

			tiles.AddChild(plant);
			plant.Position = localPos;
			plant.Lane = (int)localPos.Y;

			plantGrid[cell] = plant;

			score -= plant.SunCost;
			plant.OnPlant();
			plantLabel.Visible = false;
			SelectPlant(null);
		}

		public void SpawnZombie()
		{
			int rY = rand.Next(5);
			Zombie zomb = ZombieScene.Instantiate<Zombie>();
			zomb.GameScript = this;

			Vector2 localPos = tiles.MapToLocal(new Vector2I(9, rY));

			GetParent().AddChild(zomb);
			zomb.GlobalPosition = localPos;
            zomb.Lane = rY;
        }

        private void SelectPlantButton(Button button, PackedScene scene)
        {
            int cost = GetCost(scene);

            if (score < cost)
            {
                plantLabel.Visible = true;
                plantLabel.Text = "Not enough sun!";
                ClearSelectedPlant();
                return;
            }

            if (SelectedPlantButton != null)
            {
                SelectedPlantButton.Disabled = false;
            }

            SelectedPlantButton = button;
            SelectPlant(scene);
        }
        public void Sunflower()
		{
            SelectPlantButton(sunflower, SFlow);
		}

		public void Peashooter()
		{
			SelectPlantButton(peashooter, PShooter);
		}

		public void Repeater()
		{
			SelectPlantButton(repeater, Repeat);
		}

		public void Wallnut()
		{
            SelectPlantButton(walnut, WNut);
		}

		public void CabbagePult()
		{
			SelectPlantButton(cabbagepult, CPult);
		}

		public void ChooseRandomZombie()
		{
			int randomZom = rand.Next(4);

			if (randomZom == 0)
			{
				SelectZombie(BucketScene);
			}
			else if (randomZom == 1)
			{
				SelectZombie(ConeScene);
			}
			else if (randomZom == 2)
			{
				SelectZombie(ImpScene);
			}
			else
			{
				SelectZombie(NormalScene);
			}

			SpawnZombie();
		}

		public void ClearSelectedPlant()
		{
			PlantScene = null;
			SelectedPlantButton = null;
		}
	}
}
