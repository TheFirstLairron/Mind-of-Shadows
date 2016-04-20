using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Attack
    {
        public string name { get; set; }
        public string writtenIdentifier { get; set; }
        public int amountOfDamage { get; set; }

        public Attack(string Name, string identifier, int amount)
        {
            name = Name;
            writtenIdentifier = identifier;
            amountOfDamage = amount;
        }
    }
}
