using Godot;
using System;
using System.Collections.Generic;

namespace pvzlawnandorder
{
	public partial class GameManager : Node
	{
		public List<List<Zombie>> ZombiesInLane = new();
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
		private TileMapLayer tiles;
		private Timer downTime;
		[Export] public int score = 50;
		Node sun;


		public override void _Ready()
		{
            tiles = GetNode<TileMapLayer>("Map");

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

			for (int i = 0; i < 5; i++)
			{ 
				ZombiesInLane.Add(new List<Zombie>());
			}
		}
		
		public override void _Process(double delta)
		{
			if(sun != null){
				score = sun.Get("score").AsInt32();
			}

            if (score < 0)
            {
                score = 0;
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
			SpawnPlantFood(new Vector2((float)GD.RandRange(0,8),(float)GD.RandRange(0,4)));
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

		private int GetCost(PackedScene plantScene)
		{
			var p = plantScene.Instantiate<Plant>();
			int cost = p.SunCost;
			p.QueueFree();
			return cost;
		}

		public void TryPlacePlant(Vector2I cell)
		{
			if (score < GetCost(PlantScene))
			{
				plantLabel.Visible = true;
				plantLabel.Text = "Not enough sun yet!";
				PlantScene = null;
				return;
			}

			if (IsCellOccupied(cell))
			{
				plantLabel.Visible = true;
				plantLabel.Text = "Occupied! Select another cell!";
				return;
			}

			PlacePlant(cell);
		}

		public void SelectPlant(PackedScene scene)
		{
			PlantScene = scene;
		}

		public void SelectZombie(PackedScene scene)
		{
			ZombieScene = scene;
		}

		public void PlacePlant(Vector2I cell)
		{
			Plant plant = PlantScene.Instantiate<Plant>();
			plantLabel.Visible = true;
			plantLabel.Text = $"Select a tile to place your {plant.PlantType}";

			Vector2 localPos = tiles.MapToLocal(cell);
			
			tiles.AddChild(plant);
            plant.Position = localPos;
            
			plantGrid[cell] = plant;

			score -= plant.SunCost;
			plant.OnPlant();
			plantLabel.Visible = false;
			PlantScene = null;
		}

		public void SpawnZombie()
		{
            int rY = rand.Next(5);
			Zombie zomb = ZombieScene.Instantiate<Zombie>();

			Vector2 localPos = tiles.MapToLocal(new Vector2I(8, rY));

			GetParent().AddChild(zomb);
			zomb.Position = localPos;
		}
		public void Sunflower()
		{
			SelectPlant(SFlow);
		}

		public void Peashooter()
		{
			SelectPlant(PShooter);
		}

		public void Repeater()
		{
			SelectPlant(Repeat);
		}

		public void Wallnut()
		{
			SelectPlant(WNut);
		}

		public void CabbagePult()
		{
			SelectPlant(CPult);
		}
		
		public void ChooseRandomZombie()
		{
			int randomZom = rand.Next(4);

			if(randomZom == 0)
			{
				SelectZombie(BucketScene);
			}else if (randomZom == 1)
			{
				SelectZombie(ConeScene);
			}else if (randomZom == 2)
			{
				SelectZombie(ImpScene);
			} else
			{
				SelectZombie(NormalScene);
			}

			SpawnZombie();
		}
	}
}
