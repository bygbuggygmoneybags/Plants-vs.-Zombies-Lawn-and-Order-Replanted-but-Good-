using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
    public abstract class Plant
    {
        protected int Health {  get; set; }
        protected string Name { get; set; }
        protected int Damage { get; set; }
        protected int Cooldown { get; set; }
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

        public void Update(int time)
        {
            timer += time;

            if(CanAttack)
            {
                Attack();
                timer = 0f;
            }
        }

        protected abstract void Attack();
        protected abstract void OnPlant();
        protected abstract void Die();
    }
}
