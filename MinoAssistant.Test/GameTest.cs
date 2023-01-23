using MinoAssistant.Board;
using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion.Rotation;
using NUnit.Framework;
using System.Collections.Generic;

namespace MinoAssistant.Test
{
    public class GameTest
    {
        private GameSettings GameSettings { get; } = new GameSettings();
        private CustomGenerator Generator { get; } = new CustomGenerator();
        private SrsRotationSystem RotationSystem { get; } = new SrsRotationSystem();

        private Game Game { get; set; }

        [SetUp]
        public void Setup()
        {
            Game = new Game(GameSettings, Generator, RotationSystem);
        }

        [Test]
        public void HardDropT()
        {
            Generator.NextPiece = MinoFactory.GenerateBasicMino(MinoType.FourminoT);
            Game.Start();
            Game.HardDrop();
            HashSet<(int, int)> positions = new() { (3, 0), (4, 0), (4, 1), (5, 0) };
            AssertOnlyCellsFilled(positions);
        }

        [Test]
        public void HardDropI()
        {
            Generator.NextPiece = MinoFactory.GenerateBasicMino(MinoType.FourminoI);
            Game.Start();
            Game.HardDrop();
            HashSet<(int, int)> positions = new() { (3, 10), (4, 0), (5, 0), (6, 0) };
            AssertOnlyCellsFilled(positions);
        }

        [Test]
        public void MoveLeft()
        {
            Assert.Pass();
        }

        private void AssertOnlyCellsFilled(ICollection<(int, int)> positions)
        {
            for (int i = 0; i < Game.Field.Width; i++)
            {
                for (int j = 0; j < Game.Field.Height; j++)
                {
                    (int, int) p = (i, j);
                    if (Game.Field.IsFilledCell(p) && !positions.Contains(p)) Assert.Fail("Cell is filled when it shouldn't be.", p);
                    if (!Game.Field.IsFilledCell(p) && positions.Contains(p)) Assert.Fail("Cell isn't filled when it should be.", p);
                }
            }
        }
    }
}