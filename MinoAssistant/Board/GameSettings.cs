using MinoAssistant.Board.Block;

namespace MinoAssistant.Board
{
    public struct GameSettings
    {
        public MinoColor UnfilledCellValue;
        public MinoColor GhostPieceValue;
        public int FieldWidth;
        public int FieldHeight;
        public int OriginX;
        public int OriginY;
        public double Gravity;
        public double GravityIncrease;
        public double LockDelaySeconds;
        public int VisibleNextPieces;
        public bool IsShowingGhostPiece;

        public GameSettings(MinoColor unfilledCellValue, MinoColor ghostPieceValue, int fieldWidth = 10, int fieldHeight = 25, int originX = 5, int originY = 20, double gravity = 1000.0, double gravityIncrease = 0.0, double lockDelaySeconds = 0.3, int visibleNextPieces = 5, bool isShowingGhostPiece = true)
        {
            UnfilledCellValue = unfilledCellValue;
            GhostPieceValue = ghostPieceValue;
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
            OriginX = originX;
            OriginY = originY;
            Gravity = gravity;
            GravityIncrease = gravityIncrease;
            LockDelaySeconds = lockDelaySeconds;
            VisibleNextPieces = visibleNextPieces;
            IsShowingGhostPiece = isShowingGhostPiece;
        }
    }
}
