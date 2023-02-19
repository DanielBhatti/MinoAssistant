using MinoAssistant.Board;
using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion;
using MinoAssistant.Board.Motion.Rotation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Test
{
    public class GameControllerTest
    {
        private GameSettings DefaultSettings { get; set; }
        private ClassicRotationSystem ClassicRotationSystem { get; set; } = null!;

        private Mino TMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoT);
        private Mino IMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoI);
        private Mino OMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoO);
        private Mino SMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoS);
        private Mino ZMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoZ);
        private Mino JMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoJ);
        private Mino LMino { get; } = MinoFactory.GenerateBasicMino(MinoType.FourminoL);

        private GameController TController { get; set; } = null!;
        private GameController IController { get; set; } = null!;
        private GameController OController { get; set; } = null!;
        private GameController JController { get; set; } = null!;
        private GameController LController { get; set; } = null!;
        private GameController SController { get; set; } = null!;
        private GameController ZController { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            DefaultSettings = GameSettings.DefaultSettings;
            ClassicRotationSystem = new ClassicRotationSystem();

            MinoGenerator tGenerator = new(TMino);
            MinoGenerator iGenerator = new(IMino);
            MinoGenerator oGenerator = new(OMino);
            MinoGenerator jGenerator = new(JMino);
            MinoGenerator lGenerator = new(LMino);
            MinoGenerator sGenerator = new(SMino);
            MinoGenerator zGenerator = new(ZMino);

            TController = new GameController(DefaultSettings, tGenerator, ClassicRotationSystem);
            IController = new GameController(DefaultSettings, iGenerator, ClassicRotationSystem);
            OController = new GameController(DefaultSettings, oGenerator, ClassicRotationSystem);
            JController = new GameController(DefaultSettings, jGenerator, ClassicRotationSystem);
            LController = new GameController(DefaultSettings, lGenerator, ClassicRotationSystem);
            SController = new GameController(DefaultSettings, sGenerator, ClassicRotationSystem);
            ZController = new GameController(DefaultSettings, zGenerator, ClassicRotationSystem);
        }

        [Test]
        public void HardDropT()
        {
            TController.HardDrop();
            var hardDroppedPositions = TMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(TController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropI()
        {
            IController.HardDrop();
            var hardDroppedPositions = IMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = IMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(IController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropO()
        {
            OController.HardDrop();
            var hardDroppedPositions = OMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = OMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(OController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropJ()
        {
            JController.HardDrop();
            var hardDroppedPositions = JMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = JMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(JController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropL()
        {
            LController.HardDrop();
            var hardDroppedPositions = LMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = LMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(LController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropS()
        {
            SController.HardDrop();
            var hardDroppedPositions = SMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = SMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(SController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void HardDropZ()
        {
            ZController.HardDrop();
            var hardDroppedPositions = ZMino.GetAbsolutePositions(RotationState.R0, (DefaultSettings.OriginX, 0));
            var newMinoPositions = ZMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(ZController.Field, hardDroppedPositions.Union(newMinoPositions));
        }

        [Test]
        public void StartGameT()
        {
            var initialPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(TController.Field, initialPositions);
        }

        [Test]
        public void MoveDownT()
        {
            TController.MoveMino(MoveDirection.Down);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (0, -1));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveLeftT()
        {
            TController.MoveMino(MoveDirection.Left);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (-1, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveTwoLeftT()
        {
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (-2, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveThreeLeftT()
        {
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (-3, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveFourLeftT()
        {
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            TController.MoveMino(MoveDirection.Left);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (-3, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveRightT()
        {
            TController.MoveMino(MoveDirection.Right);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (1, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveTwoRightT()
        {
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (2, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveThreeRightT()
        {
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (3, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveFourRightT()
        {
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (4, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void MoveFiveRightT()
        {
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            TController.MoveMino(MoveDirection.Right);
            var newMinoPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin + (4, 0));
            AssertOnlyCellsFilled(TController.Field, newMinoPositions);
        }

        [Test]
        public void RotateT()
        {
            TController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (4, 22), (4, 21), (4, 20), (5, 21) };
            AssertOnlyCellsFilled(TController.Field, positions);
        }

        [Test]
        public void RotateI()
        {
            IController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (3, 20), (3, 21), (3, 22), (3, 23) };
            AssertOnlyCellsFilled(IController.Field, positions);
        }

        [Test]
        public void RotateO()
        {
            IController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (3, 20), (4, 20), (3, 21), (3, 22) };
            AssertOnlyCellsFilled(IController.Field, positions);
        }

        [Test]
        public void RotateJ()
        {
            JController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (4, 20), (4, 21), (4, 22), (5, 22) };
            AssertOnlyCellsFilled(JController.Field, positions);
        }

        [Test]
        public void RotateL()
        {
            LController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (4, 20), (4, 21), (4, 22), (3, 22) };
            AssertOnlyCellsFilled(LController.Field, positions);
        }

        [Test]
        public void RotateS()
        {
            SController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (4, 20), (5, 20), (3, 21), (4, 21) };
            AssertOnlyCellsFilled(SController.Field, positions);
        }

        [Test]
        public void RotateZ()
        {
            ZController.Rotate(RotationDirection.Clockwise);
            HashSet<(int, int)> positions = new() { (3, 20), (4, 20), (4, 21), (5, 21) };
            AssertOnlyCellsFilled(ZController.Field, positions);
        }

        [Test]
        public void SoftDropT()
        {
            TController.MoveMino(MoveDirection.Down);
            TController.SoftDrop();
            HashSet<(int, int)> positions = new() { (3, 0), (4, 0), (4, 1), (5, 0) };
            AssertOnlyCellsFilled(TController.Field, positions);
        }

        [Test]
        public void HoldT()
        {
            TController.Hold();
            var initialPositions = TMino.GetAbsolutePositions(RotationState.R0, DefaultSettings.Origin);
            AssertOnlyCellsFilled(TController.Field, initialPositions);
            Assert.AreEqual(TMino, TController.HeldMino!.Value);
        }

        private static void AssertOnlyCellsFilled(Field field, IEnumerable<Position> positions) => AssertOnlyCellsFilled(field, positions.Select(p => (p.X, p.Y)));
        private static void AssertOnlyCellsFilled(Field field, IEnumerable<(int, int)> positions)
        {
            List<Position> outOfBoundPositions = new();
            List<Position> falseFilledPositions = new();
            List<Position> falseUnfilledPositions = new();
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    (int, int) p = (i, j);
                    if (!field.IsWithinBounds(p)) outOfBoundPositions.Add(p);
                    if (field.IsFilled(p) && !positions.Contains(p)) falseFilledPositions.Add(p);
                    if (!field.IsFilled(p) && positions.Contains(p)) falseUnfilledPositions.Add(p);
                }
            }
            if (outOfBoundPositions.Any() || falseFilledPositions.Any() || falseUnfilledPositions.Any())
            {
                Assert.Fail(
@$"Positions out of bounds: {String.Join(",", outOfBoundPositions)}
Positions filled that shouldn't be: {String.Join(",", falseFilledPositions)}
Positions not filled that should be: {String.Join(",", falseUnfilledPositions)}");
            }
        }


        private static void AssertCellsFilled(Field field, IEnumerable<Position> positions) => AssertCellsFilled(field, positions.Select(p => (p.X, p.Y)));
        private static void AssertCellsFilled(Field field, IEnumerable<(int, int)> positions)
        {
            List<Position> outOfBoundPositions = new();
            List<Position> falseUnfilledPositions = new();
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    (int, int) p = (i, j);
                    if (!field.IsWithinBounds(p)) outOfBoundPositions.Add(p);
                    if (!field.IsFilled(p) && positions.Contains(p)) falseUnfilledPositions.Add(p);
                }
            }
            if (outOfBoundPositions.Any() || falseUnfilledPositions.Any())
            {
                Assert.Fail(
@$"Positions out of bounds: {String.Join(",", outOfBoundPositions)}
Positions not filled that should be: {String.Join(",", falseUnfilledPositions)}");
            }
        }
    }
}