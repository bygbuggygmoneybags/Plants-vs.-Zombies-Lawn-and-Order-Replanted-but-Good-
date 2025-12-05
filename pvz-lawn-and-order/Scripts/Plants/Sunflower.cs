using Godot;
using System;
using System.Collections.Concurrent;

namespace pvzlawnandorder
{
	public partial class Sunflower : Plant
	{
		[Export] public PackedScene Sun { get; set; }
		private Timer produceTime;
		public override void _Ready()
		{
			base._Ready();
			MaxHealth = 300;
			Health = MaxHealth;
			SunCost = 50;
			Type = "Sunflower";
			produceTime = GetNode<Timer>("ProductionTimer");
			produceTime.Timeout += ProduceSun;
		}

		private void ProduceSun()
		{
			Node sun = Sun.Instantiate();
			GetTree().CurrentScene.AddChild(sun);

			sun.Call("init_sunflower", GlobalPosition.X, GlobalPosition.Y);
            animPlay.Play("Produce");
		}
	}
}
