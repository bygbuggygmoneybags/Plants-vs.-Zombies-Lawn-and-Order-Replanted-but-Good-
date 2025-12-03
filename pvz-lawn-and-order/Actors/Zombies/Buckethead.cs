using Godot;
using System;

namespace pvzlawnandorder
{
	public partial class Buckethead : Zombie
	{
		public override void _Ready()
		{
			base._Ready();
			MaxHealth += 1100;
			Health = MaxHealth;
		}
	}
}
