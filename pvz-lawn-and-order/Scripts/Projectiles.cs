using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public partial class Projectiles : Area2D
    {
        public float Speed { get; set; }
        public int Damage { get; set; }
        public int PierceCount { get; set; }
        public Vector2 Direction { get; set; }

        public override void _Ready()
        {
            AreaEntered += OnBodyEntered;
        }

        private void OnBodyEntered(Node2D body)
        {
            if (body is Zombie zombie)
            {
                zombie.TakeDamage(Damage);
                QueueFree();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            Position += Direction * Speed * (float)delta;
        }
    }
}
