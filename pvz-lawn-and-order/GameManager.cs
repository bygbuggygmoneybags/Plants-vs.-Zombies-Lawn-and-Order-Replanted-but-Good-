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
		private Timer foodTime;
		private Timer sunTime;
		[Export] public int score = 50;
		Node sun;


		public override void _Ready()
		{
			sunTime = GetNode<Timer>("SunTimer");
			sunTime.Timeout += SpawnSky;
			foodTime = GetNode<Timer>("FoodTimer");
			foodTime.Timeout += OnTimeout;
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
	}
}
