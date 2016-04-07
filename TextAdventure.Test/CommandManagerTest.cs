using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TextAdventure.Test
{
    [TestFixture]
    public class CommandManagerTest
    {
        [Test]
        public void CommandNameByIdentifierTest()
        {
            World game = new World();
            CommandManager manager = new CommandManager(game);

            Command command = new Command("Testing", "test", "all", (gameWorld) => { });
            manager.RegisterCommand(command);

            string name = manager.GetCommandNameByInputIdentifier(command.WrittenCommandText);

            Assert.That(command.CommandName, Is.EqualTo(name));
        }

        [Test]
        public void CorrectIdentifiersForRoomTest()
        {
            World game = new World();
            CommandManager manager = new CommandManager(game);


            // This command is valid for this test
            Command validCommand = new Command("Testing", "test", "correctRoom", (gameWorld) => { });
            manager.RegisterCommand(validCommand);


            // This command is not a valid command for this test, and the test should fail if it appears
            Command invalidCommand = new Command("INVALID", "INVALID", "incorrectRoom", (gameWorld) => { });
            manager.RegisterCommand(invalidCommand);

            Room room = new Room(game, "correctRoom", "testing");

            game.cmdManager = manager;
            game.currentRoom = room;

            List<string> identifiers = game.cmdManager.GetAllInputIdentifiers(room.name);
            
            foreach(var item in identifiers)
            {
                Assert.That(!identifiers.Contains("INVALID"));
            }

        }

    }
}
