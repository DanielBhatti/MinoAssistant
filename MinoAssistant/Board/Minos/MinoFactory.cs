using System;

namespace MinoAssistant.Board.Minos
{
    public static class MinoFactory
    {
        public static Mino GenerateMino(MinoType minoType)
        {
            switch (minoType)
            {
                case MinoType.Onemino:
                    return new Mino(new Position[] { new Position(0, 0) });
                case MinoType.Twomino:
                    return new Mino(new Position[] { new Position(0, 0), new Position(1, 0) });
                case MinoType.ThreeminoI:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0) });
                case MinoType.ThreeminoL:
                    return new Mino(new Position[] { new Position(0, 1), new Position(0, 0), new Position(0, 1) });
                case MinoType.FourminoO:
                    return new Mino(new Position[] { new Position(0, 0), new Position(0, 1), new Position(1, 0), new Position(1, 1) });
                case MinoType.FourminoL:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(1, 1) });
                case MinoType.FourminoJ:
                    return new Mino(new Position[] { new Position(-1, 1), new Position(-1, 0), new Position(0, 0), new Position(1, 0) });
                case MinoType.FourminoS:
                    return new Mino(new Position[] { new Position(-1, -1), new Position(0, -1), new Position(0, 0), new Position(1, 0) });
                case MinoType.FourminoZ:
                    return new Mino(new Position[] { new Position(-1, 1), new Position(0, 1), new Position(0, 0), new Position(0, 1) });
                case MinoType.FourminoI:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(2, 0) });
                case MinoType.FourminoT:
                    return new Mino(new Position[] { new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(1, 0) });
                default:
                    throw new Exception("Should never reach this exception");
            }
        }

        public static Mino[] GenerateFourMinos() => new Mino[]
        {
            GenerateMino(MinoType.FourminoJ),
            GenerateMino(MinoType.FourminoL),
            GenerateMino(MinoType.FourminoO),
            GenerateMino(MinoType.FourminoI),
            GenerateMino(MinoType.FourminoS),
            GenerateMino(MinoType.FourminoZ),
            GenerateMino(MinoType.FourminoT)
        };

        public static Mino[] GenerateThreeMinos() => new Mino[]
        {
            GenerateMino(MinoType.ThreeminoI),
            GenerateMino(MinoType.ThreeminoL),
            GenerateMino(MinoType.FourminoO),
            GenerateMino(MinoType.FourminoI),
            GenerateMino(MinoType.FourminoS),
            GenerateMino(MinoType.FourminoZ),
            GenerateMino(MinoType.FourminoT)
        };
    }
}
