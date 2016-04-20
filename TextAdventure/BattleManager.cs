using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class BattleManager
    {
        private AttackManager attackManager { get; set; }
        private Player player { get; set; }
        private Enemy enemy { get; set; }
        private bool isPlayerWinner { get; set; }
        
        public bool hasPlayerWon()
        {
            return isPlayerWinner;
        }

        public void Battle()
        {
            bool wasValidAttack = true;
            do
            {
                int damage = 1;
                string attackChoice = "";

                Console.Clear();
                Console.WriteLine("Entering Battle");
                Console.WriteLine($"Enemy: Health: {enemy.health} Attack: {enemy.attack} Defense: {enemy.defense}");
                Console.WriteLine($"Player: Health: {player.health} Attack: {player.attack} Defense: {player.defense}");
                if(!wasValidAttack)
                {
                    wasValidAttack = true;
                    Console.WriteLine("That was not a valid attack...");
                }

                Console.WriteLine("Choose your action:");
                player.ListAllAttacks();

                attackChoice = Console.ReadLine();

                if(!attackManager.IsValidWrittenIdentifier(attackChoice))
                {
                    wasValidAttack = false;
                    continue;
                }

                string attackName = attackManager.GetNameByWrittenIdentifier(attackChoice);
                damage = attackManager.GetAttackDamageByName(attackName);
                enemy.TakeDamage(damage);

                if (enemy.health > 0)
                {
                    damage = attackManager.GetAttackDamageByName(enemy.GetRandomAttack());
                    player.TakeDamage(damage);
                }

            } while (player.health > 0 && enemy.health > 0);

            if (player.health >= 0 && enemy.health <= 0)
            {
                isPlayerWinner = true;
            }
            Console.Clear();
        }

        public BattleManager(AttackManager manager, Player player, Enemy enemy)
        {
            attackManager = manager;
            this.player = player;
            this.enemy = enemy;
            isPlayerWinner = false;
        }

    }
}
