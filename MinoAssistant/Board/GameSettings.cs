using HarfBuzzSharp;
using MinoAssistant.Board.Block;

namespace MinoAssistant.Board
{
    public record struct GameSettings
    {
        public required MinoColor UnfilledCellValue { get; init; }
        public required int FieldWidth { get; init; }
        public required int FieldHeight { get; init; }
        public required int OriginX { get; init; }
        public required int OriginY { get; init; }
        public required double Gravity { get; init; }
        public required double GravityIncrease { get; init; }
        public required double LockDelaySeconds { get; init; }
        public required int VisibleNextPieces { get; init; }
        public required bool IsShowingGhostPiece { get; init; }
        public required bool IsEditMode { get; init; }

        public Position Origin { get => (OriginX, OriginY); }

        public static GameSettings DefaultSettings => new GameSettings()
        {
            UnfilledCellValue = MinoColor.White,
            FieldWidth = 10,
            FieldHeight = 25,
            OriginX = 4,
            OriginY = 20,
            Gravity = 1000.0,
            GravityIncrease = 0.0,
            LockDelaySeconds = 0.3,
            VisibleNextPieces = 5,
            IsShowingGhostPiece = true,
            IsEditMode = false
        };
    }
}
