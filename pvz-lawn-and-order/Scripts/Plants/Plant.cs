using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder
{
    public abstract partial class Plant : Node2D
    {
        protected AnimationPlayer animPlay;
        public PackedScene Self { get; set; }
        protected Node2D Game { get; set; }
        protected PackedScene MainScene { get; set; }
        protected GameManager GameScript { get; set; }
        protected int Health {  get; set; }
        protected int MaxHealth { get; set; }
        public int SunCost { get; set; }
        public string PlantType { get; set; }
        protected int Damage { get; set; }
        protected int Lane { get; set; }
        protected float Cooldown { get; set; }
        protected float timer = 0f;
        protected Node MainInstance { get; set; }
        public PlantFood Food { get; set; }
        public bool isFed => Food != null;
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

            if (CanAttack)
            {
                timer = 0f;
            }
        }

        public void OnPlant()
        {
            Lane = (int)Position.Y;

            animPlay.Play("Spawn");
            animPlay.Queue("Idle");
        }

        public void PowerUp(string powerUp)
        {
            if (isFed)
            {
                switch (powerUp)
                {
                    case "Attack Speed":
                        Cooldown /= 2;
                        break;
                    case "Health":
                        Health += (MaxHealth * 3);
                        break;
                    case "Duplicate":
                        var duplicate = Self.Instantiate<Plant>();
                        var parent = GetParent() ?? GetTree().CurrentScene;
                        parent.AddChild(duplicate);
                        duplicate.OnPlant();
                        break;
                }
            }
        }

        public void PowerUpGone(string powerUp)
        {
            switch (powerUp) 
            {
                case "Attack Speed":
                    Cooldown *= 2f;
                    break;
                case "Health":
                    Health = MaxHealth;
                    break;
            }

            Food = null;
        }

        protected void Die()
        {
            animPlay.Play("Death");
            QueueFree();
        }

        public override void _Ready()
        {
            animPlay = GetNode<AnimationPlayer>("AnimationPlayer");
        }
    }
}
