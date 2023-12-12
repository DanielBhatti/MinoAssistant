using MinoAssistant.Game;
using MinoAssistant.Game.Block;
using MinoAssistant.Game.Generator;
using MinoAssistant.Game.Motion;
using MinoAssistant.Game.Motion.Rotation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Test;

[TestFixture]
public class GameSystemTest
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

    private GameSystem TSystem { get; set; } = null!;
    private GameSystem ISystem { get; set; } = null!;
    private GameSystem OSystem { get; set; } = null!;
    private GameSystem JSystem { get; set; } = null!;
    private GameSystem LSystem { get; set; } = null!;
    private GameSystem SSystem { get; set; } = null!;
    private GameSystem ZSystem { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        DefaultSettings = GameSettings.DefaultSettings;
        ClassicRotationSystem = new ClassicRotationSystem();

        RandomizedBagMinoGenerator tGenerator = new(TMino);
        RandomizedBagMinoGenerator iGenerator = new(IMino);
        RandomizedBagMinoGenerator oGenerator = new(OMino);
        RandomizedBagMinoGenerator jGenerator = new(JMino);
        RandomizedBagMinoGenerator lGenerator = new(LMino);
        RandomizedBagMinoGenerator sGenerator = new(SMino);
        RandomizedBagMinoGenerator zGenerator = new(ZMino);

        TSystem = new GameSystem(DefaultSettings, tGenerator, ClassicRotationSystem);
        ISystem = new GameSystem(DefaultSettings, iGenerator, ClassicRotationSystem);
        OSystem = new GameSystem(DefaultSettings, oGenerator, ClassicRotationSystem);
        JSystem = new GameSystem(DefaultSettings, jGenerator, ClassicRotationSystem);
        LSystem = new GameSystem(DefaultSettings, lGenerator, ClassicRotationSystem);
        SSystem = new GameSystem(DefaultSettings, sGenerator, ClassicRotationSystem);
        ZSystem = new GameSystem(DefaultSettings, zGenerator, ClassicRotationSystem);
    }

    //[Test]
    //public void HardDropT()
    //{
    //    TSystem.HardDrop();
    //    var hardDroppedPositions = TMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(TSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropI()
    //{
    //    ISystem.HardDrop();
    //    var hardDroppedPositions = IMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = IMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(ISystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropO()
    //{
    //    OSystem.HardDrop();
    //    var hardDroppedPositions = OMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = OMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(OSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropJ()
    //{
    //    JSystem.HardDrop();
    //    var hardDroppedPositions = JMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = JMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(JSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropL()
    //{
    //    LSystem.HardDrop();
    //    var hardDroppedPositions = LMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = LMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(LSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropS()
    //{
    //    SSystem.HardDrop();
    //    var hardDroppedPositions = SMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = SMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(SSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void HardDropZ()
    //{
    //    ZSystem.HardDrop();
    //    var hardDroppedPositions = ZMino.GetAbsolutePositions(RotationAmount.R0, (DefaultSettings.OriginX, 0));
    //    var newMinoPositions = ZMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(ZSystem.Field, hardDroppedPositions.Union(newMinoPositions));
    //}

    //[Test]
    //public void StartGameT()
    //{
    //    var initialPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(TSystem.Field, initialPositions);
    //}

    //[Test]
    //public void MoveDownT()
    //{
    //    TSystem.MoveMino(MoveDirection.Down);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (0, -1));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveLeftT()
    //{
    //    TSystem.MoveMino(MoveDirection.Left);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (-1, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveTwoLeftT()
    //{
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (-2, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveThreeLeftT()
    //{
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (-3, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveFourLeftT()
    //{
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    TSystem.MoveMino(MoveDirection.Left);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (-3, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveRightT()
    //{
    //    TSystem.MoveMino(MoveDirection.Right);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (1, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveTwoRightT()
    //{
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (2, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveThreeRightT()
    //{
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (3, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveFourRightT()
    //{
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (4, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void MoveFiveRightT()
    //{
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    TSystem.MoveMino(MoveDirection.Right);
    //    var newMinoPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin + (4, 0));
    //    AssertOnlyCellsFilled(TSystem.Field, newMinoPositions);
    //}

    //[Test]
    //public void RotateT()
    //{
    //    TSystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (4, 22), (4, 21), (4, 20), (5, 21) };
    //    AssertOnlyCellsFilled(TSystem.Field, positions);
    //}

    //[Test]
    //public void RotateI()
    //{
    //    ISystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (3, 20), (3, 21), (3, 22), (3, 23) };
    //    AssertOnlyCellsFilled(ISystem.Field, positions);
    //}

    //[Test]
    //public void RotateO()
    //{
    //    ISystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (3, 20), (4, 20), (3, 21), (3, 22) };
    //    AssertOnlyCellsFilled(ISystem.Field, positions);
    //}

    //[Test]
    //public void RotateJ()
    //{
    //    JSystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (4, 20), (4, 21), (4, 22), (5, 22) };
    //    AssertOnlyCellsFilled(JSystem.Field, positions);
    //}

    //[Test]
    //public void RotateL()
    //{
    //    LSystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (4, 20), (4, 21), (4, 22), (3, 22) };
    //    AssertOnlyCellsFilled(LSystem.Field, positions);
    //}

    //[Test]
    //public void RotateS()
    //{
    //    SSystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (4, 20), (5, 20), (3, 21), (4, 21) };
    //    AssertOnlyCellsFilled(SSystem.Field, positions);
    //}

    //[Test]
    //public void RotateZ()
    //{
    //    ZSystem.Rotate(RotationDirection.Clockwise);
    //    HashSet<(int, int)> positions = new() { (3, 20), (4, 20), (4, 21), (5, 21) };
    //    AssertOnlyCellsFilled(ZSystem.Field, positions);
    //}

    //[Test]
    //public void SoftDropT()
    //{
    //    TSystem.MoveMino(MoveDirection.Down);
    //    _ = TSystem.SoftDrop();
    //    HashSet<(int, int)> positions = new() { (3, 0), (4, 0), (4, 1), (5, 0) };
    //    AssertOnlyCellsFilled(TSystem.Field, positions);
    //}

    //[Test]
    //public void HoldT()
    //{
    //    TSystem.Hold();
    //    var initialPositions = TMino.GetAbsolutePositions(RotationAmount.R0, DefaultSettings.Origin);
    //    AssertOnlyCellsFilled(TSystem.Field, initialPositions);
    //    Assert.AreEqual(TMino, TSystem.HeldMino!.Value);
    //}

    private static void AssertOnlyCellsFilled(MinoField field, IEnumerable<Position> positions) => AssertOnlyCellsFilled(field, positions.Select(p => (p.X, p.Y)));
    private static void AssertOnlyCellsFilled(MinoField field, IEnumerable<(int, int)> positions)
    {
        List<Position> outOfBoundPositions = new();
        List<Position> falseFilledPositions = new();
        List<Position> falseUnfilledPositions = new();
        for(var i = 0; i < field.Width; i++)
        {
            for(var j = 0; j < field.Height; j++)
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
@$"Positions out of bounds: {string.Join(",", outOfBoundPositions)}
Positions filled that shouldn't be: {string.Join(",", falseFilledPositions)}
Positions not filled that should be: {string.Join(",", falseUnfilledPositions)}");
        }
    }

    private static void AssertCellsFilled(MinoField field, IEnumerable<Position> positions) => AssertCellsFilled(field, positions.Select(p => (p.X, p.Y)));
    private static void AssertCellsFilled(MinoField field, IEnumerable<(int, int)> positions)
    {
        List<Position> outOfBoundPositions = new();
        List<Position> falseUnfilledPositions = new();
        for(var i = 0; i < field.Width; i++)
        {
            for(var j = 0; j < field.Height; j++)
            {
                (int, int) p = (i, j);
                if (!field.IsWithinBounds(p)) outOfBoundPositions.Add(p);
                if (!field.IsFilled(p) && positions.Contains(p)) falseUnfilledPositions.Add(p);
            }
        }
        if (outOfBoundPositions.Any() || falseUnfilledPositions.Any())
        {
            Assert.Fail(
@$"Positions out of bounds: {string.Join(",", outOfBoundPositions)}
Positions not filled that should be: {string.Join(",", falseUnfilledPositions)}");
        }
    }
}