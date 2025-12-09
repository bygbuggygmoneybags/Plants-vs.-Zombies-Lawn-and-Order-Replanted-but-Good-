using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public partial class PlantFood : Node
    {
        Plant AppliedPlant { get; set; }
        private string Type { get; set; }
        [Export] public Timer Uptime { get; set; }
        public override void _Ready()
        {
            Random r = new Random();
            int typeNum = r.Next(1, 3);
            switch (typeNum)
            {
                case 1:
                    Type = "Attack Speed";
                    ApplyEffect();
                    break;
                case 2:
                    Type = "Health";
                    ApplyEffect();
                    break;
                case 3:
                    Type = "Duplicate";
                    ApplyEffect();
                    break;
            }
        }

        public void ApplyEffect()
        {
            if (AppliedPlant != null)
            {
                AppliedPlant.isFed = true;
                AppliedPlant.Food = this;
                AppliedPlant.PowerUp(Type);
            }
        }
    }
}
