using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public partial class PlantFood : Area2D
    {
        Plant AppliedPlant { get; set; }
        private string Type { get; set; }
        [Export] public Timer Uptime { get; set; }
        public override void _Ready()
        {
            List<string> types = new List<string>() { "Attack Speed", "Health", "Duplicate"};
            Random r = new Random();
            Type = types[r.Next(types.Count)];

            this.BodyEntered += OnEnterBody;

            ApplyEffect();

            Uptime.Timeout += OnTimeout;
            Uptime.Start();
        }

        public void ApplyEffect()
        {
            if (AppliedPlant == null)
                return;

            AppliedPlant.Food = this;
            AppliedPlant.PowerUp(Type);
        }

        private void OnTimeout()
        {
            if (AppliedPlant != null)
                AppliedPlant.PowerUpGone(Type);
            QueueFree();
        }

        private void OnEnterBody(Node2D body)
        {
            if (body is Plant plant)
            {
                AppliedPlant = plant;
                QueueFree();
            }
        }
    }
}
