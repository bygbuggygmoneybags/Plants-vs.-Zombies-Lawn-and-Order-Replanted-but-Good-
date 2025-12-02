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
        protected AnimationPlayer animPlay;
        protected Vector2I Location { get; set; }
        protected GameManager Game { get; set; }
        protected int MaxHealth { get; set; }
        protected int Health {  get; set; }
        protected string Type { get; set; }
        protected int Damage { get; set; }
        protected int Cooldown { get; set; }
        protected int Speed { get; set; }
        protected float timer = 0f;
        public int Lane { get; set; }
        protected bool IsAlive => Health > 0;
        protected bool CanAttack => timer >= Cooldown;
        protected Animation Spawn { get; set; }
        protected Animation Idle { get; set; }
        protected Animation Death { get; set; }
        protected Animation AttackAnim { get; set; }
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

        public override void _Ready()
        {
            Game.AddZombie(this);
        }

        public override void _ExitTree()
        {
            Game.RemoveZombie(this);
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
        protected void Die()
        {
                animPlay.Play("Death");
                QueueFree();
        }
    }
}
