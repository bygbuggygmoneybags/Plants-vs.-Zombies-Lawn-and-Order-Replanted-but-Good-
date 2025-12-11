using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace pvzlawnandorder
{
    public partial class ArcingProjectiles : Area2D
    {
        public Vector2 Start;
        public Vector2 Target;
        public float Speed;
        public int Damage { get; set; }
        private float Progress { get; set; } = 0f;
        private float ArcHeight { get; set; } = 50f;

        public override void _Ready()
        {
            AreaEntered += OnBodyEntered;
        }

        private void OnBodyEntered(Node body)
        {
            if (body is Zombie zombie)
            {
                zombie.TakeDamage(Damage);
                QueueFree();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            Progress += Speed * (float)delta / 300f;

            if (Progress > 1f)
            {
                QueueFree();
                return;
            }

            Vector2 flat = Start.Lerp(Target, Progress);
            float h = -4 * ArcHeight * Progress * (Progress - 1f);

            GlobalPosition = new Vector2(flat.X, flat.Y - h);
        }
    }
}
