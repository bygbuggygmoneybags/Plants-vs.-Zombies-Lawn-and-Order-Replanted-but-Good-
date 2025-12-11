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
		[Export] public PackedScene PShooter { get; set; }
		[Export] public PackedScene Repeat { get; set; }
		[Export] public PackedScene CPult { get; set; }
		[Export] public PackedScene SFlow { get; set; }
		[Export] public PackedScene WNut { get; set; }

        private Dictionary<Vector2I, Plant> plantGrid = new();
        private InputEvent place;
		private Timer foodTime;
		private Timer sunTime;
		private Label plantLabel;
		private Button peashooter;
		private Button sunflower;
		private Button repeater;
		private Button walnut;
		private Button cabbagepult;
		private TileMapLayer tiles;
		int score = 0;
		Node sun;


		public override void _Ready()
		{
			tiles = GetNode<TileMapLayer>("Map");

			sunTime = GetNode<Timer>("SunTimer");
			sunTime.Timeout += SpawnSky;
			foodTime = GetNode<Timer>("FoodTimer");
			foodTime.Timeout += OnTimeout;

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
		}

		private void SpawnSky()
		{
			sun = Sun.Instantiate();
			GetTree().CurrentScene.AddChild(sun);

			sun.Call("init_sky");
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
				plantLabel.Visible = false;
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

		public void PlacePlant(Vector2I cell)
		{
			Plant plant = PlantScene.Instantiate<Plant>();
            plantLabel.Visible = true;
			plantLabel.Text = $"Select a tile to place your {plant.PlantType}";

			Vector2 worldPos = tiles.MapToLocal(cell);
			plant.GlobalPosition = worldPos;
			
			AddChild(plant);
			plantGrid[cell] = plant;

			score -= plant.SunCost;
			plantLabel.Visible = false;
			PlantScene = null;
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
    }
}
