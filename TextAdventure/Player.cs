using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Player : EntityBase
    {
        public Player() : base()
        {
            health = 0;
            attack = 0;
            defense = 0;
        }

        public Player(int health, int attack, int defense, List<string> attacks) : base(health, attack, defense, attacks){ }

        public void ListAllAttacks()
        {
            if (attacks.Count != 0)
            {
                foreach (string attack in attacks)
                {
                    Console.WriteLine(attack);
                }
            }
            else
            {
                Console.WriteLine("There are no available attacks");
            }
        }
    }
}
