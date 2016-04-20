using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public abstract class EntityBase
    {
        public int health { get; protected set; }
        public int attack { get; protected set; }
        public int defense { get; protected set; }
        public List<string> attacks { get; protected set; }

        public void TakeDamage(int amountOfDamage)
        {
            health -= CalculateDamage(amountOfDamage, this.defense);
        }

        public void HealDamage(int amountToHeal)
        {
            health += amountToHeal;
        }

        public int CalculateDamage(int amountOfDamage, int amountOfDefense)
        {
            int damageToReturn = amountOfDamage - amountOfDefense;

            if(damageToReturn <= 0)
            {
                damageToReturn = 1;
            }

            return damageToReturn;
        }

        public EntityBase(int health, int attack, int defense, List<string> attacks)
        {
            this.health = health;
            this.attack = attack;
            this.defense = defense;
            this.attacks = attacks;
        }

        public EntityBase()
        {
            health = 0;
            attack = 0;
            defense = 0;
            attacks = new List<string>();

        }
    }
}
