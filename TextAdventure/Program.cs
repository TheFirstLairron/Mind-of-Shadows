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

            // Create Rooms
            Room entryway = new Room(game, "Entryway", "The entry hall to the house.");
            Room mainHallway = new Room(game, "Hallway", "The hallway off of the main room.");
            Room ballroom = new Room(game, "Ballroom", "A large hall for dancing.");
            Room eastBalconey = new Room(game, "East Balconey", "The balconey off the east side of the ballroom.");
            Room westBalconey = new Room(game, "West Balcony", "The balconey off the west side of the ballroom.");
            Room northBalconey = new Room(game, "North Balconey", "The balconey off the north side of the ballroom.");

            // Populate the different room connections

            entryway.north = mainHallway;

            mainHallway.south = entryway;
            mainHallway.north = ballroom;

            ballroom.south = mainHallway;
            ballroom.east = eastBalconey;
            ballroom.west = westBalconey;
            ballroom.north = northBalconey;

            eastBalconey.north = northBalconey;
            eastBalconey.west = ballroom;

            westBalconey.north = northBalconey;
            westBalconey.east = ballroom;

            northBalconey.west = westBalconey;
            northBalconey.east = eastBalconey;
            northBalconey.south = ballroom;


            roomList.Add(entryway);
            roomList.Add(mainHallway);
            roomList.Add(ballroom);
            roomList.Add(eastBalconey);
            roomList.Add(northBalconey);
            roomList.Add(westBalconey);

            game.rooms = roomList;
            game.currentRoom = entryway;

            // Add the first visit code for room one
            entryway.OnFirstVisit = ((World gameWorld) =>
            {
                // make sure the world isnt null
                if (gameWorld != null)
                {
                    Room entry = gameWorld.GetRoomByName("Entryway");

                    // make sure new r
                    if(entry != null){
                        entry.description = "The main room looks different than the last time you visited...";

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

            // Main game loop
            while (isPlaying)
            {
                isPlaying = game.InteractWithRoom();
            }
        }
    }
}
