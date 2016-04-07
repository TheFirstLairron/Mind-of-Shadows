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
        public void TestCommandNameByIdentifier()
        {
            World game = new World();
            CommandManager manager = new CommandManager(game);

            Command command = new Command("Testing", "test", "all", (gameWorld) => { });
            manager.RegisterCommand(command);

            string name = manager.GetCommandNameByInputIdentifier(command.WrittenCommandText);

            Assert.That(command.CommandName, Is.EqualTo(name));
        }

    }
}
