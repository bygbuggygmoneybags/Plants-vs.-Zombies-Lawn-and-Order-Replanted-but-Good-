using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder.Plants
{
    public partial class Wallnut : DefensivePlant
    {
        public override void _Ready()
        {
            MaxHealth = 4000;
            Health = MaxHealth;
            SunCost = 50;
            Type = "Wall-Nut";
            Stages = 2;
        }

        public override void PlayStage(int stage)
        {
            switch (stage)
            {
                case 0:
                    animPlay.Play("Idle");
                    break;
                case 1:
                    animPlay.Play("WallnutCracked1");
                    break;
                case 2:
                    animPlay.Play("WallnutCracked2");
                    break;
            }
        }
    }
}
