using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TextAdventure.Test
{
    [TestFixture]
    public class WorldTest
    {
        [Test]
        public void IsValidInputTest()
        {
            World game = new World();
            CommandManager manager = new CommandManager(game);
            Room room = new Room(game, "test", "testing room");
            game.currentRoom = room;

            Command exit = new Command("Exit", "exit", "all", (gameWorld) => {});

            manager.RegisterCommand(exit);

            game.cmdManager = manager;

            Assert.That(game.IsValidInput("exit"), Is.EqualTo(true));

        }
    }
}
