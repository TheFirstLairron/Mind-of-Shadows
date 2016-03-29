using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class World
    {
        public List<Room> rooms { get; set; }

        public Room currentRoom { get; set; }

        public List<Item> items { get; set; }

        public Inventory inventory { get; set; }

        // This function will validate the user input
        public bool IsValidInput(string input)
        {
            bool isValid = false;

            input = input.ToLower();

            if (input == "south")
            {
                isValid = true;
            }

            if (input == "north")
            {
                isValid = true;
            }

            if (input == "west")
            {
                isValid = true;
            }

            if (input == "east")
            {
                isValid = true;
            }

            if (input == "quit")
            {
                isValid = true;
            }

            if (input == "exit")
            {
                isValid = true;
            }

            if (input == "interact")
            {
                isValid = true;
            }
            
            if (input == "look around")
            {
                isValid = true;
            }

            if (input == "search")
            {
                isValid = true;
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

                currentRoom = newRoom;
                currentRoom.timesVisited++;

                // Check and trigger OnFirstVisit events
                if (currentRoom.timesVisited == 1)
                {
                    if (currentRoom.OnFirstVisit != null)
                    {
                        currentRoom.OnFirstVisit(currentRoom.gameWorld);
                    }
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

        public void AddItemToInventory(Room curRoom)
        {
            if (curRoom != null)
            {
                if (curRoom.item != null)
                {
                    if (curRoom.item.name != null)
                    {
                        inventory.AddItem(curRoom.item.name, curRoom.item);
                        if (curRoom.OnItemInteract != null)
                        {
                            curRoom.OnItemInteract(this);
                        }
                    }
                }
            }
        }

        public bool InteractWithRoom()
        {
            string input;
            bool isStillPlaying = true;
            do
            {
                input = Console.ReadLine();
                input = input.ToLower();

                if(!IsValidInput(input))
                {
                    Console.WriteLine("That is not a valid command");
                }

            } while (!IsValidInput(input));

            switch (input)
            {
                case "south":
                    MoveRoom(currentRoom.south, "south");
                    break;
                case "north":
                    MoveRoom(currentRoom.north, "north");
                    break;
                case "east":
                    MoveRoom(currentRoom.east, "east");
                    break;
                case "west":
                    MoveRoom(currentRoom.west, "west");
                    break;
                case "interact":
                    if (currentRoom.OnInteractCommand != null)
                    {
                        currentRoom.OnInteractCommand(this);
                    }
                    break;
                case "quit":
                    isStillPlaying = false;
                    break;
                case "exit":
                    isStillPlaying = false;
                    break;
                case "look around":
                    Console.WriteLine(currentRoom.description);
                    break;
                case "search":
                    AddItemToInventory(currentRoom);
                    break;
                default:
                    break;
            }

            return isStillPlaying;
        }

        public Room GetRoomByName(string nameToFind)
        {
            Room roomToReturn = null;
            foreach (Room room in rooms)
            {
                if(room.name == nameToFind)
                {
                    roomToReturn = room;
                }
            }

            return roomToReturn;
        }

        public World(){}

        public World(List<Room> roomList, Room firstRoom, List<Item> itemList, Inventory inv)
        {
            rooms = roomList;
            currentRoom = firstRoom;
            items = itemList;
            inventory = inv;
        }
    }
}
