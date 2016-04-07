using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TextAdventure
{
    public class Inventory
    {

        public World world;
        public Dictionary<string, Item> items { get; set; }

        // add an item to the list if it isnt there, or increment if it exists already
        public void AddItem(string keyValue, Item newItem)
        {
            if (items != null)
            {
                if (items.ContainsKey(keyValue))
                {
                    items[keyValue].amount++;
                }
                else
                {
                    if (newItem != null)
                    {
                        items[keyValue] = newItem;
                        items[keyValue].amount = 1;
                    }
                    else
                    {
                        throw new ArgumentNullException("newItem", "the item you passed in was null");
                    }
                }
            }
            else
            {
                Console.WriteLine("INVALID ITEM LIST");
            }
        }

        // this will remove a single unit of the item specified, and removes it if the amount is 0
        public bool RemoveItem(string keyValue)
        {
            bool shouldRemove = false;

            if(items.ContainsKey(keyValue))
            {
                if(items[keyValue].amount <= 1)
                {
                    shouldRemove = false;
                }
                else
                {
                    items[keyValue].amount--;
                }

            }

            if(shouldRemove)
            {
                items.Remove(keyValue);
            }

            return shouldRemove;
        }

        // This is only used if you want to get direct access to the item for some reason
        public Item GetItem(string keyValue)
        {
            return items?[keyValue];
        }

        // Check if you have the item in your inventory
        public bool HasItem(string name)
        {
            bool isFound = false;

            if(items.ContainsKey(name))
            {
                if(items[name].amount > 0)
                {
                    isFound = true;
                }
            }

            return isFound;
        }

        public Inventory()
        {
            items = new Dictionary<string, Item>();
        }
    }
}