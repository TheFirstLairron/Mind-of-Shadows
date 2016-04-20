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

        [Test]
        public void GetRoomByNameTest()
        {
            World game = new World();
            Room room = new Room(game, "testing", "Valid Room");
            Room invalidRoom = new Room(game, "invalid", "invalid");
            List<Room> rooms = new List<Room>();
            rooms.Add(room);
            rooms.Add(invalidRoom);

            game.rooms = rooms;

            Assert.That(game.GetRoomByName("testing").Equals(room));
            
        }

        [Test]
        public void GetEnemyByNameTest()
        {
            World game = new World();

            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy("valid", 0, 0, 0, null);
            Enemy invalidEnemy = new Enemy("invalid", 0, 0, 0, null);

            enemies.Add(enemy);
            enemies.Add(invalidEnemy);

            game.enemies = enemies;

            Assert.That(game.GetEnemyByName("valid").Equals(enemy));
        }
    }
}
