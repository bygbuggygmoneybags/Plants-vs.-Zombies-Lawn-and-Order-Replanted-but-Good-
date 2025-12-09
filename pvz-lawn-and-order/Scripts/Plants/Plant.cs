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
        protected Vector2I Location { get; set; }
        protected GameManager Game { get; set; }
        protected int Health {  get; set; }
        protected int MaxHealth { get; set; }
        protected int SunCost { get; set; }
        protected string Type { get; set; }
        protected int Damage { get; set; }
        protected int Lane { get; set; }
        protected float Cooldown { get; set; }
        protected float timer = 0f;
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

        private void OnSpawnFinish(StringName name)
        {
            if (name == "Spawn")
            {
                animPlay.Play("Idle");
            }    
        }

        protected void OnPlant(Vector2I location, GameManager game)
        {
            Location = location;
            Game = game;
            Lane = Location.Y;

            animPlay.AnimationFinished += OnSpawnFinish;
            animPlay.Play("Spawn");
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
                        duplicate.OnPlant(Location + new Vector2I(50, 0), Game);
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
