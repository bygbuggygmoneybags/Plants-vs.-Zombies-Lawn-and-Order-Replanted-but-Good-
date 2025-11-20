using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public abstract partial class Zombie : Node2D
    {
        protected int Health {  get; set; }
        protected string Type { get; set; }
        protected int Damage { get; set; }
        protected int Cooldown { get; set; }
        protected int Speed { get; set; }
        protected float timer = 0f;
        protected bool IsAlive => Health > 0;
        protected bool CanAttack => timer >= Cooldown;
        protected Plant target;
        protected bool IsAttacking => target != null;

        public override void _Process(double time)
        {
            if (!IsAlive) return;

            timer += (float)time;
            if (IsAttacking)
            {
                if (CanAttack)
                {
                    Attack();
                    timer = 0f;
                }
            } else
            {
                Walk((float)time);
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public void Walk(float timeWalk)
        {
            Position += new Vector2(-Speed * timeWalk, 0);
        }

        private void OnEnterPlantTile(Area2D area)
        {
            if(area.GetParent() is Plant plant)
            {
                StartTarget(plant);
            }    
        }

        public void StartTarget(Plant plant)
        {
            target = plant;
        }

        public void StopTarget()
        {
            target = null;
        }

        protected abstract void Attack();
        protected abstract void Die();
    }
}
