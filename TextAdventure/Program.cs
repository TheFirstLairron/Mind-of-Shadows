using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;

            World game = new World();

            List<Room> roomList = new List<Room>();

            List<Item> itemList = new List<Item>();

            Inventory inventory = new Inventory();

            // Create Rooms
            Room entryway = new Room(game, "Entryway", "The entry hall to the house.");
            Room mainHallway = new Room(game, "Hallway", "The hallway off of the main room.");
            Room ballroom = new Room(game, "Ballroom", "A large hall for dancing.");
            Room eastBalconey = new Room(game, "East Balconey", "The balconey off the east side of the ballroom.");
            Room westBalconey = new Room(game, "West Balcony", "The balconey off the west side of the ballroom.");
            Room northBalconey = new Room(game, "North Balconey", "The balconey off the north side of the ballroom.");

            // Create items
            Item testingItem = new Item("Testing Item");
            itemList.Add(testingItem);

            // Populate the different room connections
            entryway.north = mainHallway;
            entryway.northTransition = "You leave the main room and find yourself in a hallway.";

            mainHallway.south = entryway;
            mainHallway.north = ballroom;
            mainHallway.southTransition = "You leave the hallway and find yourself in the main entryway.";
            mainHallway.northTransition = "You leave the hallway and find yourself in the ballroom.";
            mainHallway.item = testingItem;

            ballroom.south = mainHallway;
            ballroom.east = eastBalconey;
            ballroom.west = westBalconey;
            ballroom.north = northBalconey;
            ballroom.southTransition = "You leave the ballroom and enter a hallway.";
            ballroom.eastTransition = "You leave the ballroom and go out to the balconey.";
            ballroom.westTransition = "You leave the ballroom and go out to the balconey.";
            ballroom.northTransition = "You leave the ballroom and go out to the balconey.";

            eastBalconey.north = northBalconey;
            eastBalconey.west = ballroom;
            eastBalconey.westTransition = "You leave the balconey and walk into the ballroom.";

            westBalconey.north = northBalconey;
            westBalconey.east = ballroom;
            westBalconey.eastTransition = "You leave the balconey and walk into the ballroom.";

            northBalconey.west = westBalconey;
            northBalconey.east = eastBalconey;
            northBalconey.south = ballroom;
            northBalconey.southTransition = "You leave the balconey and walk into the ballroom.";


            roomList.Add(entryway);
            roomList.Add(mainHallway);
            roomList.Add(ballroom);
            roomList.Add(eastBalconey);
            roomList.Add(northBalconey);
            roomList.Add(westBalconey);

            // Setting up game world
            game.rooms = roomList;
            game.currentRoom = entryway;
            game.items = itemList;
            game.inventory = inventory;

            #region Lambdas
            // Add the first visit code for room one
            entryway.OnFirstVisit = ((World gameWorld) =>
            {
                // make sure the world isnt null
                if (gameWorld != null)
                {
                    Room entry = gameWorld.GetRoomByName("Entryway");

                    // make sure new r
                    if(entry != null){
                        entry.description = "The main room looks different than the last time you visited, a glowing stone is floating in the air...";

                        #region Add Portal Lambda
                        entry.OnInteractCommand = ((World world) =>
                        {
                            if (gameWorld != null)
                            {
                                Room entryRoom = world.GetRoomByName("Entryway");
                                Room ballRoom = world.GetRoomByName("Ballroom");

                                if (entryRoom != null)
                                {                                    
                                    if (ballRoom != null)
                                    {
                                        entryRoom.description = "The main room is now filled with a mysterious blue light, a portal has taken the place of the stone in the south.";
                                        entryRoom.southTransition = "You enter the strange portal, and appear in the ballroom...";
                                        entryRoom.south = ballRoom;
                                    }
                                }
                            }
                        });
                        #endregion
                    }
                }
            });

            mainHallway.OnItemInteract = ((World gameWorld) =>
            {
                Console.WriteLine("You pick up a sword");
            });
            #endregion


            Console.WriteLine("INTRO HERE");
            // Main game loop
            while (isPlaying)
            {
                isPlaying = game.InteractWithRoom();
            }
        }
    }
}
