using MinoAssistant.Board;
using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion.Rotation;
using NUnit.Framework;
using System.Collections.Generic;

namespace MinoAssistant.Test
{
    public class GameControllerTest
    {
        private GameSettings GameSettings { get; set; }
        private SrsRotationSystem SrsRotationSystem { get; set; } = null!;
        private BpsGenerator BpsGenerator { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            GameSettings = new GameSettings() { };
            SrsRotationSystem = new SrsRotationSystem();
            BpsGenerator = new BpsGenerator();
        }

        [Test]
        public void HardDropT()
        {
            MinoGenerator generator = new MinoGenerator(MinoFactory.GenerateBasicMino(MinoType.FourminoT));
            GameController gameController = new GameController(GameSettings, generator, SrsRotationSystem);
            gameController.HardDrop();
            HashSet<(int, int)> positions = new() { (3, 0), (4, 0), (4, 1), (5, 0) };
            AssertOnlyCellsFilled(gameController.Field, positions);
        }

        [Test]
        public void HardDropI()
        {
            MinoGenerator generator = new MinoGenerator(MinoFactory.GenerateBasicMino(MinoType.FourminoI));
            GameController gameController = new GameController(GameSettings, generator, SrsRotationSystem);
            gameController.HardDrop();
            HashSet<(int, int)> positions = new() { (3, 10), (4, 0), (5, 0), (6, 0) };
            AssertOnlyCellsFilled(gameController.Field, positions);
        }

        [Test]
        public void MoveLeft()
        {
            Assert.Pass();
        }

        private void AssertOnlyCellsFilled(Field field, ICollection<(int, int)> positions)
        {
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    (int, int) p = (i, j);
                    if (field.IsFilled(p) && !positions.Contains(p)) Assert.Fail("Cell is filled when it shouldn't be.", p);
                    if (!field.IsFilled(p) && positions.Contains(p)) Assert.Fail("Cell isn't filled when it should be.", p);
                }
            }
        }
    }
}