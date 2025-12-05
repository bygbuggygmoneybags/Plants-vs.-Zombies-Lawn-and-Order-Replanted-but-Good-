using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public abstract partial class RangedPlant : Plant, IPlantRangedAttacks
    {
        public override void _Process(double delta){

            base._Process(delta);

            timer -= (float)delta;

            if (timer <= Cooldown && ZombieInLane){
                RangedAttack(NumProjectiles, SpreadAngle, ProjectileSpeed, PierceCount, IgnoreObstacles, ZombieInLane);
                timer = Cooldown;
            }
        }

        public abstract short NumProjectiles{ get; set; }
        public abstract float SpreadAngle{ get; set; }
        public abstract float ProjectileSpeed{ get; set; }
        public abstract int PierceCount{ get; set; }
        public abstract bool IgnoreObstacles{ get; set; }
        public abstract bool ZombieInLane{ get; set; }

        public abstract void RangedAttack(int numProjectiles, float spreadAngle, float speed, int piercingNum, bool ignoreObstacles, bool zombieInLane);
    }
}
