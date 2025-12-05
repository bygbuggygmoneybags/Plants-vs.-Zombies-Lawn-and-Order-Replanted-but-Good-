using Godot;
using System;
using System.Collections.Generic;

namespace pvzlawnandorder
{
	public partial class GameManager : Node
	{
		public List<List<Zombie>> ZombiesInLane = new();
		[Export] public PackedScene Sun { get; set; }
		private Timer sunTime;

		public override void _Ready()
		{
			sunTime = GetNode<Timer>("SunTimer");
			sunTime.Timeout += SpawnSky;
			for (int i = 0; i < 5; i++)
			{
				ZombiesInLane.Add(new List<Zombie>());
			}
		}
		public override void _Process(double delta)
		{
		}

		private void SpawnSky()
		{
			Node sun = Sun.Instantiate();
			GetTree().CurrentScene.AddChild(sun);

			sun.Call("init_sky");
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
