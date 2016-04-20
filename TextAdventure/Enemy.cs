using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Enemy : EntityBase
    {
        public string name { get; set; }

        public Enemy() : base() { }

        public Enemy(string name, int health, int attack, int defense, List<string> attacks) : base(health, attack, defense, attacks)
        {
            this.name = name;
        }

        public string GetRandomAttack()
        {
            string attack = "";

            Random rand = new Random();

            attack = attacks[rand.Next(0, attacks.Count)];

            return attack;
        }
    }
}
