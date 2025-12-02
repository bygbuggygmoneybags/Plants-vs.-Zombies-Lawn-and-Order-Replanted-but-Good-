using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace pvzlawnandorder.Plants
{
    public abstract partial class DefensivePlant : Plant
    {
        public int Stages;

        public new void TakeDamage(int damage)
        {
            Health -= damage;

            UpdateStage();

            if (Health <= 0)
            {
                Die();
            }
        }

        private void UpdateStage()
        {
            float percent = Health * MaxHealth;

            int stage = (int)((1 - percent) * Stages);

            stage = Mathf.Clamp(stage, 0, Stages);

            PlayStage(stage);
        }

        public abstract void PlayStage(int stage);
    }
}
