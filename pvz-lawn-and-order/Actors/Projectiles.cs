using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder.Actors
{
    public partial class Projectiles : Node2D
    {
        public float Speed { get; set; }
        public int PierceCount { get; set; }
        public Vector2 Direction { get; set; }

        public override void _PhysicsProcess(double delta)
        {
            GlobalPosition += Direction * Speed * (float)delta;
        }
    }
}
