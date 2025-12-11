using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
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
                var zombsInLane = GameScript.ZombiesInLane[Lane];

                Zombie target = zombsInLane.Where(z => z.GlobalPosition.X > GlobalPosition.X).OrderBy(z => z.GlobalPosition.X).FirstOrDefault();

                if (target == null)
                {
                    return;
                }

                for (int i = 0; i < numProjectiles; i++)
                {
                    Area2D cabbage = CabbageProj.Instantiate<Area2D>();

                    GetTree().CurrentScene.AddChild(cabbage);
                    cabbage.GlobalPosition = GlobalPosition;

                    if (cabbage is ArcingProjectiles projectile)
                    {
                        projectile.Target = target.GlobalPosition;
                        projectile.Start = GlobalPosition;
                        projectile.Damage = Damage;
                        projectile.Speed = speed;
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
            Damage = 40;
            Cooldown = 2.925f;
            SunCost = 100;
            PlantType = "Cabbage-Pult";
            NumProjectiles = 1;
            SpreadAngle = 0;
            ProjectileSpeed = 20f;
            PierceCount = 0;
            IgnoreObstacles = true;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            ZombieInLane = GameScript.ZombiesInLane[Lane].Any(z => z.GlobalPosition.X > GlobalPosition.X);
        }
    }
}
