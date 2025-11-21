using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
    public abstract class RangedPlant : Plant, IPlantRangedAttacks, Node2D
    {
        protected override void _Process(double delta){

            base._Process(delta);

            timer -= (float)delta;

            if (timer <= Cooldown && zombieInLane){
                RangedAttack();
                timer = Cooldown;
            }
        }

        protected abstract short numProjectiles{get;}
        protected abstract float spreadAngle{get;}
        protected abstract float projectileSpeed{get;}
        protected abstract int pierceCount{get;}
        protected abstract bool ignoreObstacles{get;}
        protected abstract bool zombieInLane{get;}

        protected abstract void RangedAttack(int numProjectiles, float spreadAngle, float speed, int piercingNum, bool ignoreObstacles, bool zombieInLane);

        // var projectile = GD.Load("res://Projectiles/{PROJECTILE NAME}.tscn").Instantiate<Node2D>();
        // projectile.GlobalPosition = GlobalPosition + new Vector2(40, -20);
        // GetTree().CurrentScene.AddChild(projectile);
        //Can't make actual attacks without using GODOT Properely
    }
}
