using Godot;
using System;

namespace pvzlawnandorder
{
	public partial class Conehead : Zombie
	{
		public override void _Ready()
		{
			base._Ready();
			MaxHealth += 370;
			Health = MaxHealth;
		}
	}
}
