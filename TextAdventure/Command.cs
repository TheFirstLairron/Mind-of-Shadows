using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Command
    {
        public string CommandName { get; set; }
        public string WrittenCommandText { get; set; }
        public string RoomNameWhereUsable { get; set; }
        public Action<World> CommandAction { get; set; }

        public void ExecuteActions(World world)
        {
            if (world.currentRoom.name == RoomNameWhereUsable || this.RoomNameWhereUsable == "all")
            {
                this?.CommandAction.Invoke(world);
            }
        }

        public Command(string name, string cmdText, string roomName, Action<World> method)
        {
            CommandName = name;
            WrittenCommandText = cmdText;
            RoomNameWhereUsable = roomName;
            CommandAction = method;
        }
    }
}
