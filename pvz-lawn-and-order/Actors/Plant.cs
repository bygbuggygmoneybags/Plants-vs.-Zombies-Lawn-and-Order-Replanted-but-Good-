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
        protected AnimationPlayer animPlay;
        protected Vector2I Location { get; set; }
        protected GameManager Game { get; set; }
        protected int Health {  get; set; }
        protected int MaxHealth { get; set; }
        protected int SunCost { get; set; }
        protected string Type { get; set; }
        protected int Damage { get; set; }
        protected int Lane { get; set; }
        protected Animation Spawn { get; set; }
        protected Animation Idle { get; set; }
        protected Animation Death { get; set; }
        protected Animation Attack { get; set; }
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
            animPlay = GetNode<AnimationPlayer>("AnimationPlayer");
            Location = location;
            Game = game;

            animPlay.AnimationFinished += OnSpawnFinish;
            animPlay.Play("Spawn");
        }
        protected void Die()
        {
            if (IsAlive)
            {
                animPlay.Play("Death");
                _ExitTree();
            }
    }
}
