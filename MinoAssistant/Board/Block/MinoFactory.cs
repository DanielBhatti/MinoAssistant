using System;

namespace MinoAssistant.Board.Block
{
    public static class MinoFactory
    {
        public static Mino GenerateMino(MinoType minoType, MinoColor minoColor, MinoColor ghostMinoColor)
        {
            switch (minoType)
            {
                case MinoType.Onemino:
                    return new Mino(new Position[] { (0, 0) }, minoColor, ghostMinoColor);
                case MinoType.Twomino:
                    return new Mino(new Position[] { (0, 0), (1, 0) }, minoColor, ghostMinoColor);
                case MinoType.ThreeminoI:
                    return new Mino(new Position[] { (-1, 0), (0, 0), (1, 0) }, minoColor, ghostMinoColor);
                case MinoType.ThreeminoL:
                    return new Mino(new Position[] { (0, 1), (0, 0), (0, 1) }, minoColor, ghostMinoColor);
                case MinoType.FourminoO:
                    return new Mino(new Position[] { (0, 0), (0, 1), (1, 0), (1, 1) }, minoColor, ghostMinoColor);
                case MinoType.FourminoL:
                    return new Mino(new Position[] { (-1, 0), (0, 0), (1, 0), (1, 1) }, minoColor, ghostMinoColor);
                case MinoType.FourminoJ:
                    return new Mino(new Position[] { (-1, 1), (-1, 0), (0, 0), (1, 0) }, minoColor, ghostMinoColor);
                case MinoType.FourminoS:
                    return new Mino(new Position[] { (-1, 0), (0, 0), (0, 1), (1, 1) }, minoColor, ghostMinoColor);
                case MinoType.FourminoZ:
                    return new Mino(new Position[] { (-1, 1), (0, 1), (0, 0), (1, 0) }, minoColor, ghostMinoColor);
                case MinoType.FourminoI:
                    return new Mino(new Position[] { (-1, 0), (0, 0), (1, 0), (2, 0) }, minoColor, ghostMinoColor);
                case MinoType.FourminoT:
                    return new Mino(new Position[] { (-1, 0), (0, 0), (1, 0), (0, 1) }, minoColor, ghostMinoColor);
                default:
                    throw new Exception($"{nameof(MinoFactory)} {nameof(GenerateBasicMino)} method does not support creation of {nameof(MinoType)} {minoType}.");
            }
        }

        public static Mino GenerateBasicMino(MinoType minoType)
        {
            switch (minoType)
            {
                case MinoType.Onemino:
                case MinoType.Twomino:
                    return GenerateMino(minoType, MinoColor.Black, MinoColor.Black);
                case MinoType.ThreeminoI:
                    return GenerateMino(minoType, MinoColor.Green, MinoColor.Orange);
                case MinoType.ThreeminoL:
                    return GenerateMino(minoType, MinoColor.Blue, MinoColor.Orange);
                case MinoType.FourminoO:
                    return GenerateMino(minoType, MinoColor.Yellow, MinoColor.YellowFaded);
                case MinoType.FourminoL:
                    return GenerateMino(minoType, MinoColor.Orange, MinoColor.OrangeFaded);
                case MinoType.FourminoJ:
                    return GenerateMino(minoType, MinoColor.Blue, MinoColor.BlueFaded);
                case MinoType.FourminoS:
                    return GenerateMino(minoType, MinoColor.Red, MinoColor.RedFaded);
                case MinoType.FourminoZ:
                    return GenerateMino(minoType, MinoColor.Green, MinoColor.GreenFaded);
                case MinoType.FourminoI:
                    return GenerateMino(minoType, MinoColor.Teal, MinoColor.TealFaded);
                case MinoType.FourminoT:
                    return GenerateMino(minoType, MinoColor.Purple, MinoColor.PurpleFaded);
                default:
                    throw new Exception($"{nameof(MinoFactory)} {nameof(GenerateBasicMino)} method does not support creation of {nameof(MinoType)} {minoType}.");
            }
        }

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
}
