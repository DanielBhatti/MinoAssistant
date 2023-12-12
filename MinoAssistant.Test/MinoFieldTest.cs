using NUnit.Framework;
using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion;
using MinoAssistant.Game;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Test;

[TestFixture]
public class MinoFieldTest
{
    private Field Field { get; set; } = null!;
    private int Width => 10;
    private int Height => 20;
    private MinoColor DefaultColor => MinoColor.None;

    [SetUp]
    public void Setup() => Field = new MinoField(Width, Height, DefaultColor, false);

    [Test]
    public void ConstructsProperly()
    {
        Assert.AreEqual(Width, Field.Width);
        Assert.AreEqual(Height, Field.Height);
        Assert.AreEqual(DefaultColor, Field.EmptyCellColor);
    }

    [Test]
    public void WithinBounds2()
    {
        Assert.IsTrue(Field.IsWithinBounds(new Position(0, 0)));
        Assert.IsFalse(Field.IsWithinBounds(new Position(10, 20)));
    }

    [Test]
    public void SetOutofBounds()
    {
        Field.Set(new Position(10, 20), MinoColor.Blue);
        Assert.Pass();
    }

    [Test]
    public void ResetToDefaultColor()
    {
        Field.Set(new Position(5, 5), MinoColor.Blue);
        Field.ResetField();
        Assert.AreEqual(DefaultColor, Field[5, 5].Value);
    }

    [Test]
    public void DefaultCellsAreInitializedCorrectly()
    {
        for(int i = 0; i < Width; i++)
            for(int j = 0; j < Height; j++)
                Assert.AreEqual(DefaultColor, Field[i, j].Value);
    }

    [Test]
    public void SetMultiplePositions()
    {
        var positions = new List<Position> { new(1, 1), new(2, 2) };
        Field.Set(positions, MinoColor.Blue);
        foreach(var position in positions) Assert.AreEqual(MinoColor.Blue, Field[position.X, position.Y].Value);
    }

    [Test]
    public void IsFilledCorrectlyIdentifiesFilledCells()
    {
        Field.Set(new Position(3, 3), MinoColor.Blue);
        Assert.IsTrue(Field.IsFilled(new Position(3, 3)));
    }

    [Test]
    public void IsFilledIdentifiesEmptyCell()
    {
        Field.Set(new Position(3, 3), DefaultColor);
        Assert.IsFalse(Field.IsFilled(new Position(3, 3)));
    }

    [Test]
    public void BoundaryConditions()
    {
        Assert.IsFalse(Field.IsWithinBounds(new Position(-1, 0)));
        Assert.IsFalse(Field.IsWithinBounds(new Position(0, -1)));
        Assert.IsTrue(Field.IsWithinBounds(new Position(Width - 1, Height - 1)));
    }

    [Test]
    public void IsRowFilledWithFullyFilledRow()
    {
        for(int i = 0; i < Width; i++) Field.Set(new Position(i, 0), MinoColor.Blue);
        Assert.IsTrue(Field.IsRowFilled(0));
    }

    [Test]
    public void ResetFieldResetsAllCells()
    {
        Field.Set(new Position(1, 1), MinoColor.Blue);
        Field.ResetField();
        Assert.IsTrue(Enumerable.Range(0, Width).All(x => Enumerable.Range(0, Height).All(y => Field[x, y].Value == DefaultColor)));
    }

    [Test]
    public void IsAllFilledWithAllFilledCells()
    {
        var positions = new List<Position> { new(1, 1), new(2, 2) };
        Field.Set(positions, MinoColor.Blue);
        Assert.IsTrue(Field.IsAllFilled(positions));
    }

    [Test]
    public void IsRowFilledWithEmptyRow() => Assert.IsFalse(Field.IsRowFilled(0));

    [Test]
    public void IsRowFilledWithPartiallyFilledRow()
    {
        Field.Set(new Position(0, 0), MinoColor.Blue);
        Assert.IsFalse(Field.IsRowFilled(0));
    }

    [Test]
    public void IsAnyFilledWithNoFilledCells()
    {
        var positions = new List<Position> { new Position(1, 1), new Position(2, 2) };
        Assert.IsFalse(Field.IsAnyFilled(positions));
    }

    [Test]
    public void IsAnyFilledWithSomeFilledCells()
    {
        Field.Set(new Position(1, 1), MinoColor.Blue);
        var positions = new List<Position> { new Position(1, 1), new Position(2, 2) };
        Assert.IsTrue(Field.IsAnyFilled(positions));
    }

    [Test]
    public void IsAllFilledWithPartiallyFilledCells()
    {
        Field.Set(new Position(1, 1), MinoColor.Blue);
        var positions = new List<Position> { new Position(1, 1), new Position(2, 2) };
        Assert.IsFalse(Field.IsAllFilled(positions));
    }
}
