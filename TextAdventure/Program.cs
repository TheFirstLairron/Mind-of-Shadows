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

            CommandManager manager = new CommandManager(game);

            // Create Rooms
            Room entryway = new Room(game, "Entryway", "The entry hall to the house.");
            Room mainHallway = new Room(game, "Hallway", "The hallway off of the main room.");
            Room ballroom = new Room(game, "Ballroom", "A large hall for dancing.");
            Room eastBalconey = new Room(game, "East Balconey", "The balconey off the east side of the ballroom.");
            Room westBalconey = new Room(game, "West Balcony", "The balconey off the west side of the ballroom.");
            Room northBalconey = new Room(game, "North Balconey", "The balconey off the north side of the ballroom.");

            // Create items
            Item testingItem = new Item("Sword", "A rusty old sword with a dull blade");
            itemList.Add(testingItem);

            #region Commands
            Command moveNorth = new Command("Move North", "north", "all", (gameWorld) =>
            {
                gameWorld.MoveRoom(gameWorld.currentRoom.north, "north");
            });

            manager.RegisterCommand(moveNorth);

            Command moveEast = new Command("Move East", "east", "all", (gameWorld) =>
            {
                gameWorld.MoveRoom(gameWorld.currentRoom.east, "east");
            });

            manager.RegisterCommand(moveEast);

            Command moveSouth = new Command("Move South", "south", "all", (gameWorld) =>
            {
                gameWorld.MoveRoom(gameWorld.currentRoom.south, "south");
            });

            manager.RegisterCommand(moveSouth);

            Command moveWest = new Command("Move West", "west", "all", (gameWorld) =>
            {
                gameWorld.MoveRoom(gameWorld.currentRoom.west, "west");
            });

            manager.RegisterCommand(moveWest);

            Command lookAround = new Command("Look Around", "look around", "all", (gameWorld) =>
            {
                Console.WriteLine(gameWorld?.currentRoom?.description);
            });

            manager.RegisterCommand(lookAround);

            Command quit = new Command("Quit", "quit", "all", (gameWorld) =>
            {
                gameWorld.isStillPlaying = false;
            });

            manager.RegisterCommand(quit);

            Command exit = new Command("Exit", "exit", "all", (gameWorld) =>
            {
                gameWorld.isStillPlaying = false;
            });

            manager.RegisterCommand(exit);

            Command touchBlueStone = new Command("Touch Blue Stone", "touch blue stone", "Entryway", (gameWorld) =>
            {
                Room entryRoom = gameWorld.GetRoomByName("Entryway");
                Room ballRoom = gameWorld.GetRoomByName("Ballroom");

                if (entryRoom != null)
                {
                    if (ballRoom != null)
                    {
                        entryRoom.description = "The main room is now filled with a mysterious blue light, a portal has taken the place of the stone in the south.";
                        entryRoom.southTransition = "You enter the strange portal, and appear in the ballroom...";
                        entryRoom.south = ballRoom;
                    }
                }
            }, false);

            manager.RegisterCommand(touchBlueStone);

            #endregion Commands

            #region Room Connections
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
            #endregion Room Connections

            entryway.OnEnter = ((gameWorld) =>
            {
                if (gameWorld.currentRoom.timesVisited == 1)
                {
                    gameWorld.cmdManager.RegisterCommand(touchBlueStone.CommandName);
                }
            });

            entryway.OnLeave = ((gameWorld) =>
            {
                Room entry = gameWorld.GetRoomByName("Entryway");
                Room ballRoom = gameWorld.GetRoomByName("Ballroom");

                if (entry != null)
                {
                    if (entry != null)
                    {
                        entry.description = "The room looks largely the same, except for a glowing blue stone that is floating in front of the south wall";
                        entry.south = ballRoom;
                        entry.southTransition = "You pass through the portal, and find yourself in the ballroom of the house.";
                        gameWorld.cmdManager.UnregisterCommandByName("Touch Blue Stone");
                    }
                }
                return true;
            });

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
            game.cmdManager = manager;


            Console.WriteLine("INTRO HERE");
            // Main game loop
            while (isPlaying)
            {
                isPlaying = game.InteractWithRoom();
            }
        }
    }
}