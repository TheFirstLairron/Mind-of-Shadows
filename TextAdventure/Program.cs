using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            List<Enemy> enemyList = new List<Enemy>();

            Inventory inventory = new Inventory();

            CommandManager manager = new CommandManager(game);

            AttackManager attackManager = new AttackManager(game);

            Player player = new Player(10, 10, 10, new List<string> { Constants.ATTACK_BASIC });

            // Create Rooms
            Room entryway = new Room(game, Constants.ENTRYWAY_NAME, Constants.ENTRYWAY_DESC);
            Room mainHallway = new Room(game, Constants.HALLWAY_NAME, Constants.HALLWAY_DESC);
            Room ballroom = new Room(game, Constants.BALLROOM_NAME, Constants.BALLROOM_DESC);
            Room eastBalconey = new Room(game, Constants.EAST_BALCONEY_NAME, Constants.EAST_BALCONEY_DESC);
            Room westBalconey = new Room(game, Constants.WEST_BALCONEY_NAME, Constants.WEST_BALCONEY_DESC);
            Room northBalconey = new Room(game, Constants.NORTH_BALCONEY_NAME, Constants.NORTH_BALCONEY_DESC);

            // Create items
            Item testingItem = new Item("Sword", "A rusty old sword with a dull blade");
            itemList.Add(testingItem);

            Enemy testingEnemy = new Enemy("Testing Enemy", 10, 10, 10, new List<string> { Constants.ATTACK_BASIC });
            enemyList.Add(testingEnemy);

            Attack strikeAttack = new Attack(Constants.ATTACK_BASIC, Constants.ATTACK_BASIC, 15);
            attackManager.RegisterAttack(strikeAttack);

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

            Command touchBlueStone = new Command("Touch Blue Stone", "touch blue stone", Constants.ENTRYWAY_NAME, (gameWorld) =>
            {
                Room entryRoom = gameWorld.GetRoomByName("Entryway");
                Room ballRoom = gameWorld.GetRoomByName("Ballroom");

                if (entryRoom != null)
                {
                    if (ballRoom != null)
                    {
                        //Console.BackgroundColor = ConsoleColor.Blue;
                        //Console.Clear();

                        Console.WriteLine("The blue stone starts to spin around violently...");
                        Console.WriteLine("The stone explodes and releases a blinding blue light.");
                        entryRoom.description = "The main room is now filled with a mysterious blue light, a portal has taken the place of the stone in the south.";
                        entryRoom.southTransition = "You enter the strange portal, and appear in the ballroom...";
                        entryRoom.south = ballRoom;
                        manager.UnregisterCommandByName("Touch Blue Stone");
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
            eastBalconey.northTransition = "You walk along the balconey towards the north.";
            eastBalconey.west = ballroom;
            eastBalconey.westTransition = "You leave the balconey and walk into the ballroom.";

            westBalconey.north = northBalconey;
            eastBalconey.northTransition = "You walk along the balconey towards the north.";
            westBalconey.east = ballroom;
            westBalconey.eastTransition = "You leave the balconey and walk into the ballroom.";

            northBalconey.west = westBalconey;
            northBalconey.westTransition = "You walk along the balconey towards the west.";
            northBalconey.east = eastBalconey;
            northBalconey.eastTransition = "You walk along the balconey towards the east.";
            northBalconey.south = ballroom;
            northBalconey.southTransition = "You leave the balconey and walk into the ballroom.";
            #endregion Room Connections

            #region Room Events
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
                    }
                }
                return true;
            });

            mainHallway.OnEnter = ((gameWorld) =>
            {
                BattleManager battleManager = new BattleManager(gameWorld.attackManager, gameWorld.player, gameWorld.GetEnemyByName("Testing Enemy"));
                battleManager.Battle();
                if(battleManager.hasPlayerWon())
                {
                    Console.WriteLine("Player has won!");
                }
                else
                {
                    gameWorld.isStillPlaying = false;
                    Console.WriteLine("Player has lost!");
                }
                Console.WriteLine(gameWorld.currentRoom.description);
                mainHallway.OnEnter = null;
            });

            #endregion Room Events

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
            game.enemies = enemyList;
            game.attackManager = attackManager;
            game.player = player;


            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("INTRO HERE");
            // Main game loop
            while (isPlaying)
            {
                isPlaying = game.InteractWithRoom();
            }
        }
    }
}