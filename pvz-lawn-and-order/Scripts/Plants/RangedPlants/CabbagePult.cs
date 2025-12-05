using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pvzlawnandorder.Actors;

namespace pvzlawnandorder.Plants
{
    public partial class CabbagePult : RangedPlant
    {
        public override short NumProjectiles { get; set; }
        public override float SpreadAngle { get; set; }
        public override float ProjectileSpeed { get; set; }
        public override int PierceCount { get; set; }
        public override bool IgnoreObstacles { get; set; }
        public override bool ZombieInLane { get; set; }

        [Export] public PackedScene CabbageProj;

        public override void RangedAttack(int numProjectiles, float spreadAngle, float speed, int piercingNum, bool ignoreObstacles, bool zombieInLane)
        {
            if (ZombieInLane && CanAttack)
            {
                var zombsInLane = Game.ZombiesInLane[Lane];

                Zombie target = zombsInLane.Where(z => z.GlobalPosition.X > GlobalPosition.X).OrderBy(z => z.GlobalPosition.X).FirstOrDefault();

                if (target == null)
                {
                    return;
                }

                for (int i = 0; i < numProjectiles; i++)
                {
                    animPlay.Play("Attack");
                    Node2D cabbage = CabbageProj.Instantiate<Node2D>();
                    if (cabbage is ArcingProjectiles projectile)
                    {
                        projectile.Damage = Damage;
                        projectile.Speed = speed;
                    }

                    GetTree().CurrentScene.AddChild(cabbage);
                }
            }
        }
        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 300;
            Health = MaxHealth;
            Damage = 40;
            Cooldown = 2.925f;
            SunCost = 100;
            Type = "Cabbage-Pult";
            NumProjectiles = 1;
            SpreadAngle = 0;
            ProjectileSpeed = 20f;
            PierceCount = 0;
            IgnoreObstacles = true;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            ZombieInLane = Game.ZombiesInLane[Lane].Any(z => z.GlobalPosition.X > GlobalPosition.X);
        }
    }
}
