using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvzlawnandorder
{
    public abstract class Zombie
    {
        private int health {  get; set; }
        private string name { get; set; }
        private int damage { get; set; }
        private int attackSpeed { get; set; }
        private int speed { get; set; }

        public abstract void Attack(int damage, int attackSpeed);
    }
}
