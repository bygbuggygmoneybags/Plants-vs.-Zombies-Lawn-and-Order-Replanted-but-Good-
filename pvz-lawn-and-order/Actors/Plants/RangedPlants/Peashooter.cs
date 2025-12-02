using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pvzlawnandorder.Actors;

namespace pvzlawnandorder.Plants
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
            if (ZombieInLane && CanAttack)
            {
                for (int i = 0; i < numProjectiles; i++)
                {
                    Node2D pea = PeaProj.Instantiate<Node2D>();

                    pea.GlobalPosition = Location;

                    if (pea is Projectiles projectile)
                    {
                        projectile.Speed = speed;
                        projectile.PierceCount = piercingNum;

                        projectile.Direction = Vector2.Right;
                    }

                    GetTree().CurrentScene.AddChild(pea);
                }

                animPlay?.Play("Attack");
            }
        }

        public override void _Ready()
        {
            MaxHealth = 300;
            Health = MaxHealth;
            Damage = 20;
            SunCost = 100;
            Type = "Peashooter";
            NumProjectiles = 1;
            SpreadAngle = 0;
            ProjectileSpeed = 15f;
            PierceCount = 0;
            IgnoreObstacles = false;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            ZombieInLane = Game.ZombiesInLane[Lane].Any(z => z.GlobalPosition.X > GlobalPosition.X);
        }
    }
}
