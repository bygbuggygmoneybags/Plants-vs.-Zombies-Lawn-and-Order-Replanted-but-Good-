using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
	public partial class Peashooter : RangedPlant
	{
		public override short NumProjectiles { get; set; }
		public override float SpreadAngle { get; set; }
		public override float ProjectileSpeed { get; set; }
		public override int PierceCount { get; set; }
		public override bool IgnoreObstacles { get; set; }
		public override bool ZombieInLane { get; set; }

		[Export] public PackedScene PeaProj;

		public override void RangedAttack(int numProjectiles, float spreadAngle, float speed, int piercingNum, bool ignoreObstacles, bool zombieInLane)
		{
			if (ZombieInLane)
			{
				for (int i = 0; i < numProjectiles; i++)
				{
					Area2D pea = PeaProj.Instantiate<Area2D>();

                    GetTree().CurrentScene.AddChild(pea);
                    pea.GlobalPosition = GlobalPosition;
                    
					if (pea is Projectiles projectile)
					{
						projectile.Damage = Damage;
						projectile.Speed = speed;
						projectile.PierceCount = piercingNum;
						projectile.Direction = Vector2.Right;
					}
				}
			}
			timer = 0f;
		}

		public override void _Ready()
		{
			base._Ready();
			MaxHealth = 300;
			Health = MaxHealth;
			Damage = 20;
			Cooldown = 5f;
			SunCost = 100;
			PlantType = "Peashooter";
			NumProjectiles = 1;
			SpreadAngle = 0;
			ProjectileSpeed = 15f;
			PierceCount = 0;
			IgnoreObstacles = false;
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			ZombieInLane = GameScript.ZombiesInLane[Lane].Any(z => z.GlobalPosition.X > GlobalPosition.X);
		}
	}
}
