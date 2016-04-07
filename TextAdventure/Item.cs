using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }

        public Item(string itemName, string desc)
        {
            name = itemName;
            description = desc;
        }
    }
}
