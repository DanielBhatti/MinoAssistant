namespace MinoAssistant.Board
{
    public record GameSettings(
        double Gravity = 1.0,
        double GravityIncrease = 0.0,
        double LockDelaySeconds = 0.3,
        int VisibleNextPieces = 5,
        bool IsShowingGhostPiece = true
        );
}
