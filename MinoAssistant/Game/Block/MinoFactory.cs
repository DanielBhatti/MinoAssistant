using System;

namespace MinoAssistant.Game.Block;

public static class MinoFactory
{
    public static Mino GenerateMino(MinoType minoType, MinoColor minoColor, MinoColor ghostMinoColor) =>
        minoType switch
        {
            MinoType.Onemino => new Mino() { RelativePositionsDefinition = new Position[] { (0, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.Twomino => new Mino() { RelativePositionsDefinition = new Position[] { (0, 0), (1, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.ThreeminoI => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 0), (0, 0), (1, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.ThreeminoL => new Mino() { RelativePositionsDefinition = new Position[] { (0, 1), (0, 0), (0, 1) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoO => new Mino() { RelativePositionsDefinition = new Position[] { (0, 0), (0, 1), (1, 0), (1, 1) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoL => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 0), (0, 0), (1, 0), (1, 1) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoJ => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 1), (-1, 0), (0, 0), (1, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoS => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 0), (0, 0), (0, 1), (1, 1) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoZ => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 1), (0, 1), (0, 0), (1, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoI => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 0), (0, 0), (1, 0), (2, 0) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            MinoType.FourminoT => new Mino() { RelativePositionsDefinition = new Position[] { (-1, 0), (0, 0), (1, 0), (0, 1) }, MinoColor = minoColor, GhostMinoColor = ghostMinoColor },
            _ => throw new Exception($"{nameof(MinoFactory)} {nameof(GenerateBasicMino)} method does not support creation of {nameof(MinoType)} {minoType}."),
        };

    public static Mino GenerateBasicMino(MinoType minoType) => minoType switch
    {
        MinoType.Onemino or MinoType.Twomino => GenerateMino(minoType, MinoColor.Black, MinoColor.Black),
        MinoType.ThreeminoI => GenerateMino(minoType, MinoColor.Green, MinoColor.Orange),
        MinoType.ThreeminoL => GenerateMino(minoType, MinoColor.Blue, MinoColor.Orange),
        MinoType.FourminoO => GenerateMino(minoType, MinoColor.Yellow, MinoColor.YellowFaded),
        MinoType.FourminoL => GenerateMino(minoType, MinoColor.Orange, MinoColor.OrangeFaded),
        MinoType.FourminoJ => GenerateMino(minoType, MinoColor.Blue, MinoColor.BlueFaded),
        MinoType.FourminoS => GenerateMino(minoType, MinoColor.Red, MinoColor.RedFaded),
        MinoType.FourminoZ => GenerateMino(minoType, MinoColor.Green, MinoColor.GreenFaded),
        MinoType.FourminoI => GenerateMino(minoType, MinoColor.Teal, MinoColor.TealFaded),
        MinoType.FourminoT => GenerateMino(minoType, MinoColor.Purple, MinoColor.PurpleFaded),
        _ => throw new Exception($"{nameof(MinoFactory)} {nameof(GenerateBasicMino)} method does not support creation of {nameof(MinoType)} {minoType}."),
    };

    // the quantity of four minos vs all the minos of size four
    public static Mino[] GenerateBasicFourMinos() => new Mino[]
    {
        GenerateBasicMino(MinoType.FourminoJ),
        GenerateBasicMino(MinoType.FourminoL),
        GenerateBasicMino(MinoType.FourminoO),
        GenerateBasicMino(MinoType.FourminoI),
        GenerateBasicMino(MinoType.FourminoS),
        GenerateBasicMino(MinoType.FourminoZ),
        GenerateBasicMino(MinoType.FourminoT)
    };

    public static Mino[] GenerateBasicThreeMinos() => new Mino[]
    {
        GenerateBasicMino(MinoType.ThreeminoI),
        GenerateBasicMino(MinoType.ThreeminoL),
    };
}
