using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public static class Constants
    {
        public enum ATTACK_TYPES
        {
            HEALING,
            DEFENSE,
            ATTACK
        }

        public static readonly string ENTRYWAY_NAME = "Entryway";
        public static readonly string ENTRYWAY_DESC = "The entry hall to the house.";
        public static readonly string HALLWAY_NAME = "Hallway";
        public static readonly string HALLWAY_DESC = "The hallway off of the main room.";
        public static readonly string BALLROOM_NAME = "Ballroom";
        public static readonly string BALLROOM_DESC = "A large hall for dancing.";
        public static readonly string NORTH_BALCONEY_NAME = "North Balconey";
        public static readonly string NORTH_BALCONEY_DESC = "The balconey off the north side of the ballroom.";
        public static readonly string WEST_BALCONEY_NAME = "West Balconey";
        public static readonly string WEST_BALCONEY_DESC = "The balconey off the west side of the ballroom.";
        public static readonly string EAST_BALCONEY_NAME = "East Balconey";
        public static readonly string EAST_BALCONEY_DESC = "The balconey off the east side of the ballroom.";

        public static readonly string ATTACK_BASIC = "Strike";
    }
}
