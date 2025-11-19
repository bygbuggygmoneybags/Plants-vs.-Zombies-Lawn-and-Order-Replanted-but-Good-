using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
    public abstract class Zombie
    {
        protected int Health {  get; set; }
        protected string Name { get; set; }
        protected int Damage { get; set; }
        protected int Cooldown { get; set; }
        protected int Speed { get; set; }
        protected float timer = 0f;
        protected bool IsAlive => Health > 0;
        protected bool CanAttack => timer >= Cooldown;
        protected Plant target;
        protected bool IsAttacking => target != null;

        public void Update(int time)
        {
            if (!IsAlive) return;

            timer += time;
            if (IsAttacking)
            {
                if (CanAttack)
                {
                    Attack();
                    timer = 0f;
                }
            } else
            {
                Walk(time);
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

        public void Walk(int timeWalk)
        {

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
