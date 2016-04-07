using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class CommandManager
    {
        public Dictionary<string, Command> commands { get; set; }

        public World gameWorld { get; set; }

        public bool IsCommandRegisteredUnderName(string name)
        {
            bool isCommandRegistered = false;

            if(commands.ContainsKey(name))
            {
                isCommandRegistered = true;
            }

            return isCommandRegistered;
        }

        public void RegisterCommand(Command command)
        {
            if(!IsCommandRegisteredUnderName(command.CommandName))
            {
                commands.Add(command.CommandName, command);
            }
        }

        public void RegisterCommand(string commandName)
        {
            if(IsCommandRegisteredUnderName(commandName))
            {
                commands[commandName].isRegistered = true;
            }
        }

        public void CallCommandByName(string nameOfCommand)
        {
            if(IsCommandRegisteredUnderName(nameOfCommand))
            {
                commands[nameOfCommand].ExecuteActions(gameWorld);
            }
        }

        public bool UnregisterCommandByName(string nameOfCommand)
        {
            bool commandRemoved = false;

            if(IsCommandRegisteredUnderName(nameOfCommand))
            {
                commands[nameOfCommand].isRegistered = false;
                commandRemoved = true;
            }

            return commandRemoved;
        }

        public string GetCommandNameByInputIdentifier(string inputIdentifier)
        {
            string name = "";

            foreach (var item in commands)
            {
                if (item.Value.WrittenCommandText == inputIdentifier && item.Value.isRegistered)
                {
                    name = item.Value.CommandName;
                }
            }

            return name;

        }

        public List<string> GetAllInputIdentifiers()
        {
            List<string> inputIdentifiers = new List<string>();

            foreach(var item in commands)
            {
                if (item.Value.isRegistered)
                {
                    inputIdentifiers.Add(item.Value.WrittenCommandText);
                }
            }

            return inputIdentifiers;
        }

        public List<string> GetAllInputIdentifiers(string roomName)
        {
            List<string> inputIdentifiers = new List<string>();

            foreach(var item in commands)
            {
                if (item.Value.isRegistered)
                {
                    if (item.Value.RoomNameWhereUsable == roomName || item.Value.RoomNameWhereUsable == "all")
                    {
                        inputIdentifiers.Add(item.Value.WrittenCommandText);
                    }
                }
            }

            return inputIdentifiers;
        }

        public CommandManager(World world)
        {
            commands = new Dictionary<string, Command>();
            gameWorld = world;
        }

    }
}
