using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder.Actors
{
    public partial class ArcingProjectiles : Node2D
    {
        public Vector2 Start;
        public Vector2 Target;
        public float Speed;
        private float Progress { get; set; } = 0f;
        private float ArcHeight { get; set; } = 50f;

        public override void _Process(double delta)
        {
            Progress += Speed * (float)delta / 300f;

            if (Progress > 1f)
            {
                QueueFree();
                return;
            }

            Vector2 flat = Start.Lerp(Targer, Progress);
            float h = -4 * ArcHeight * Progress * (Progress - 1f);

            GlobalPosition = new Vector2(flat.X, flat.Y - h);
        }
    }
}
