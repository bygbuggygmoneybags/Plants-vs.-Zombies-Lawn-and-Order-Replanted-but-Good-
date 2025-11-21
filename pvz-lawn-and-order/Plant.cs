using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public abstract partial class Plant : Node2D
    {
        protected int Health {  get; set; }
        protected string Type { get; set; }
        protected int Damage { get; set; }
        protected Animation PlantAnimation { get; set; }
        protected float Cooldown { get; set; }
        protected float timer = 0f;
        protected bool IsAlive => Health > 0;
        protected bool CanAttack => timer >= Cooldown;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public override void _Process(double time)
        {
            timer += (float)time;

            if(CanAttack)
            {
                Attack();
                timer = 0f;
            }
        }

        protected void OnPlant()
        {
            PlantAnimation.GetClass();
        }

        protected abstract void Attack();
        protected abstract void Die();
    }
}
