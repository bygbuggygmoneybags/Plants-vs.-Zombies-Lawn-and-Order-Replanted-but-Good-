using Godot;
using System;
using System.Collections.Generic;

namespace pvzlawnandorder
{
	public partial class GameManager : Node
	{
		public List<List<Zombie>> ZombiesInLane = new();
		public override void _Ready()
		{
			for (int i = 0; i < 5; i++)
			{
				ZombiesInLane.Add(new List<Zombie>());
			}
		}
		public override void _Process(double delta)
		{
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
