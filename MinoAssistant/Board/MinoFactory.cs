using System;

namespace MinoAssistant.Board
{
    public static class MinoFactory
    {
        public static Mino GenerateMino(MinoType minoType, MinoColor minoColor)
        {
            switch (minoType)
            {
                case MinoType.Onemino:
                    return new Mino(new Position[] { new Position(0, 0) }, minoColor);
                case MinoType.Twomino:
                    return new Mino(new Position[] { new Position(0, 0), new Position(1, 0) }, minoColor);
                case MinoType.ThreeminoI:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0) }, minoColor);
                case MinoType.ThreeminoL:
                    return new Mino(new Position[] { new Position(0, 1), new Position(0, 0), new Position(0, 1) }, minoColor);
                case MinoType.FourminoO:
                    return new Mino(new Position[] { new Position(0, 0), new Position(0, 1), new Position(1, 0), new Position(1, 1) }, minoColor);
                case MinoType.FourminoL:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(1, 1) }, minoColor);
                case MinoType.FourminoJ:
                    return new Mino(new Position[] { new Position(-1, 1), new Position(-1, 0), new Position(0, 0), new Position(1, 0) }, minoColor);
                case MinoType.FourminoS:
                    return new Mino(new Position[] { new Position(-1, -1), new Position(0, -1), new Position(0, 0), new Position(1, 0) }, minoColor);
                case MinoType.FourminoZ:
                    return new Mino(new Position[] { new Position(-1, 1), new Position(0, 1), new Position(0, 0), new Position(0, -1) }, minoColor);
                case MinoType.FourminoI:
                    return new Mino(new Position[] { new Position(-2, 0), new Position(-1, 0), new Position(0, 0), new Position(1, 0) }, minoColor);
                case MinoType.FourminoT:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(0, 1) }, minoColor);
                default:
                    throw new Exception($"{nameof(MinoFactory)} {nameof(GenerateBasicMino)} method does not support creation of {nameof(MinoType)} {minoType}.");
            }
        }

        public static Mino GenerateBasicMino(MinoType minoType)
        {
            switch (minoType)
            {
                case MinoType.Onemino:
                    return GenerateMino(minoType, MinoColor.Black);
                case MinoType.Twomino:
                    return GenerateMino(minoType, MinoColor.Black);
                case MinoType.ThreeminoI:
                    return GenerateMino(minoType, MinoColor.Black);
                case MinoType.ThreeminoL:
                    return GenerateMino(minoType, MinoColor.Black);
                case MinoType.FourminoO:
                    return GenerateMino(minoType, MinoColor.Yellow);
                case MinoType.FourminoL:
                    return GenerateMino(minoType, MinoColor.Orange);
                case MinoType.FourminoJ:
                    return GenerateMino(minoType, MinoColor.Blue);
                case MinoType.FourminoS:
                    return GenerateMino(minoType, MinoColor.Red);
                case MinoType.FourminoZ:
                    return GenerateMino(minoType, MinoColor.Green);
                case MinoType.FourminoI:
                    return GenerateMino(minoType, MinoColor.Teal);
                case MinoType.FourminoT:
                    return GenerateMino(minoType, MinoColor.Purple);
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
