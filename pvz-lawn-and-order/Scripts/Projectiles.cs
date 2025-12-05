using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder.Actors
{
    public partial class Projectiles : Area2D
    {
        public float Speed { get; set; }
        public int Damage { get; set; }
        public int PierceCount { get; set; }
        public Vector2 Direction { get; set; }

        public override void _Ready()
        {
            Connect("BodyEntered", new Callable(this, "OnBodyEntered"));
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
            GlobalPosition += Direction * Speed * (float)delta;
        }
    }
}
