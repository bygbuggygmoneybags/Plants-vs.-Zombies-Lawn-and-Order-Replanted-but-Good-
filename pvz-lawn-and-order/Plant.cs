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
        private int health {  get; set; }
        private string name { get; set; }
        private int damage { get; set; }
        private int cooldown { get; set; }

        public abstract void Attack(int damage, int cooldown);
    }
}
