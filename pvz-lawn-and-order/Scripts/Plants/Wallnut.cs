using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
    public partial class Wallnut : Plant
    {
        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 4000;
            Health = MaxHealth;
            SunCost = 50;
            PlantType = "Wall-Nut";
        }
    }
}
