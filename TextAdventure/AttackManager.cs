using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class AttackManager
    {
        public Dictionary<string, Attack> attacks { get; set; }

        public World gameWorld { get; set; }

        public bool DoesAttackExist(string name)
        {
            bool doesExist = false;

            if(attacks.ContainsKey(name))
            {
                doesExist = true;
            }

            return doesExist;
        }

        public void RegisterAttack(Attack attack)
        {
            if(!DoesAttackExist(attack.name))
            {
                attacks[attack.name] = attack;
            }
        }

        public int GetAttackDamageByName(string attack)
        {
            int amountOfDamage = 0;

            if(DoesAttackExist(attack))
            {
                amountOfDamage = attacks[attack].amountOfDamage;
            }

            return amountOfDamage;
        }

        public string GetNameByWrittenIdentifier(string writtenIdentifier)
        {
            string result = "";

            result = attacks.Where(x => x.Value.writtenIdentifier.ToLower() == writtenIdentifier.ToLower())
                            .Select(x => x.Value.name)
                            .FirstOrDefault();

            return result;
        }

        public List<string> GetAllWrittenIdentifiers()
        {
            List<string> identifiers = new List<string>();

            foreach(var item in attacks)
            {
                identifiers.Add(item.Value.writtenIdentifier);
            }

            return identifiers;
        }

        public bool IsValidWrittenIdentifier(string identifier)
        {
            bool isValid = false;

            if (attacks.Select(x => x.Value.writtenIdentifier.ToLower()).ToList().Contains(identifier.ToLower()))
            {
                isValid = true;
            }

            return isValid;
        }

        public AttackManager(World world)
        {
            gameWorld = world;
            attacks = new Dictionary<string, Attack>();
        }
    }
}
