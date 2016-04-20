using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class World
    {
        public Room currentRoom { get; set; }

        public List<Room> rooms { get; set; }

        public List<Enemy> enemies { get; set; }

        public List<Item> items { get; set; }

        public Inventory inventory { get; set; }

        public CommandManager cmdManager { get; set; }

        public AttackManager attackManager { get; set; }

        public Player player { get; set; }

        public bool isStillPlaying { get; set; }

        // This function will validate the user input
        public bool IsValidInput(string input)
        {
            bool isValid = false;

            input = input.ToLower();

            foreach(var item in cmdManager.GetAllInputIdentifiers(currentRoom.name))
            {
                //Console.WriteLine(item);
                if(input == item)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        // This function moves you to the next room
        public void MoveRoom(Room newRoom, string direction)
        {
            // Check that room isnt null
            if (newRoom == null)
            {
                Console.WriteLine("There is nowhere to go in that direction...");
            }
            else
            {
                // handle the transitions
                switch(direction)
                {
                    case "south":
                        if (currentRoom.southTransition != null)
                        {
                            Console.WriteLine(currentRoom.southTransition);
                        }
                        break;
                    case "north":
                        if (currentRoom.northTransition != null)
                        {
                            Console.WriteLine(currentRoom.northTransition);
                        }
                        break;
                    case "east":
                        if (currentRoom.eastTransition != null)
                        {
                            Console.WriteLine(currentRoom.southTransition);
                        }
                        break;
                    case "west":
                        if (currentRoom.eastTransition != null)
                        {
                            Console.WriteLine(currentRoom.eastTransition);
                        }
                        break;
                    default:
                        break;
                }

                bool shouldLeave = true;

                if (currentRoom.OnLeave != null)
                {
                    shouldLeave = currentRoom.OnLeave(currentRoom.gameWorld);
                }

                if (shouldLeave)
                {
                    currentRoom = newRoom;
                    currentRoom.timesVisited++;
                }

                if(currentRoom.OnEnter != null)
                {
                    currentRoom.OnEnter(currentRoom.gameWorld);
                }
            }
        }

        public void DisplayHelpMenu()
        {
            // TODO
        }

        public void InteractWithInventory()
        {
            // TODO
        }

        public bool InteractWithRoom()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                input = input.ToLower();

                if(!IsValidInput(input))
                {
                    Console.Clear();
                    Console.WriteLine("That is not a valid command");
                }

            } while (!IsValidInput(input));
            Console.Clear();

            string commandName = cmdManager.GetCommandNameByInputIdentifier(input);

            cmdManager.CallCommandByName(commandName);

            return isStillPlaying;
        }

        public Room GetRoomByName(string nameToFind)
        {
            Room roomToReturn = null;

            roomToReturn = rooms.Where(x => x.name == nameToFind).First();

            return roomToReturn;
        }

        public Enemy GetEnemyByName(string name)
        {
            Enemy enemyToReturn = null;

            enemyToReturn = enemies.Where(x => x.name == name).First();

            return enemyToReturn;
        }

        public World()
        {
            isStillPlaying = true;
        }

        public World(List<Room> roomList, Room firstRoom, List<Item> itemList, Inventory inv, CommandManager manager, Player player)
        {
            rooms = roomList;
            currentRoom = firstRoom;
            items = itemList;
            inventory = inv;
            cmdManager = manager;
            isStillPlaying = true;
        }
    }
}
