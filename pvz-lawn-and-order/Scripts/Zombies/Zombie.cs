using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
	public partial class Zombie : Area2D
	{
        protected Node2D Game { get; set; }
        protected PackedScene MainScene { get; set; }
        public GameManager GameScript { get; set; }
        protected Node MainInstance { get; set; }
        protected int MaxHealth { get; set; }
		protected int Health {  get; set; }
		protected int ExtraHealth { get; set; }
		protected string Type { get; set; }
		protected int Damage { get; set; }
		protected int Cooldown { get; set; }
		protected float Speed { get; set; }
		protected float timer = 0f;
		public int Lane { get; set; }
		protected bool IsAlive => Health > 0;
		protected bool CanAttack => timer >= Cooldown;
		protected Plant target;
		protected bool IsAttacking => target != null;
		private bool IsBlocked = false;

		public override void _PhysicsProcess(double time)
		{
			if (!IsAlive) return;

			timer += (float)time;

            if (!IsBlocked)
            {
				CheckForPlantInLane();
                Position += Vector2.Left * Speed * (float)time;
            }
            else if (target != null)
            {
                Attack();
            }
        }
		public override void _Ready()
		{
            MaxHealth = 190;
            ExtraHealth = 89;
            Health = MaxHealth += ExtraHealth;
			Damage = 100;
			Speed = 15f;
			GameScript.AddZombie(this);
			AreaEntered += OnEnterPlantTile;
		}

		public void TakeDamage(int damage)
		{
			Health -= damage;
			if (Health <= 0)
			{
				Die();
			}
		}

		private void OnEnterPlantTile(Node2D body)
		{
			if (body is Plant plant)
			{
				StartTarget(plant);
			}
        }

		public void StartTarget(Plant plant)
		{
			IsBlocked = true;
			target = plant;
		}

		public void StopTarget()
		{
			IsBlocked = false;
			target = null;
		}

		protected void Attack()
		{
			if (CanAttack && IsAttacking)
			{
				target.TakeDamage(Damage);
				if(target.Health <= 0)
				{
					StopTarget();
				}
			}
		}
		protected void Die()
		{
			GameScript.RemoveZombie(this);
			QueueFree();
		}

        private void CheckForPlantInLane()
        {
            if (target != null) return;

            foreach (var plant in GameScript.PlantsInLane[Lane])
            {
                float distance = GlobalPosition.DistanceTo(plant.GlobalPosition);
                if (distance <= 32f)
                {
                    StartTarget(plant);
                    break;
                }
            }
        }
    }
}
