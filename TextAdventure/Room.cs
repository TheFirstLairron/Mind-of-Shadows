using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Room
    {
        public World gameWorld { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int timesVisited { get; set; }
        public Room north { get; set; }
        public string northTransition { get; set; }
        public Room east { get; set; }
        public string eastTransition { get; set; }
        public Room south { get; set; }
        public string southTransition { get; set; }
        public Room west { get; set; }
        public string westTransition { get; set; }
        public Action<World> OnFirstVisit { get; set; }
        public Action<World> OnInteractCommand { get; set; }

        public Room(World game, string roomName, string desc, Room westRoom = null, Room eastRoom = null, Room northRoom = null, Room southRoom = null)
        {
            gameWorld = game;
            name = roomName;
            description = desc;
            west = westRoom;
            east = eastRoom;
            north = northRoom;
            south = southRoom;
            timesVisited = 0;
        }
    }
}
