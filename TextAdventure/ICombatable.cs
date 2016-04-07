using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public interface ICombatable
    {
        int health { get; set; }
        int attack { get; set; }
        int defense { get; set; }
        Attack firstAttack { get; set; }
        Attack secondAttack { get; set; }
        Attack thirdAttack { get; set; }
        Attack fourthAttack { get; set; }
        int TakeDamage(int amountOfDamage);
        int HealDamage(int amountToHeal);
        int CalculateDamage(int amountOfDamage, int amountOfDefense);
    }
}
