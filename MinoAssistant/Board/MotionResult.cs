namespace MinoAssistant.Board
{
    public class MotionResult
    {
        public Mino Mino { get; }
        public MotionType MotionType { get; }
        public Position PreviousCenterPosition { get; }
        public Position NewCenterPosition { get; }

        public MotionResult(Mino mino, MotionType motionType, Position previousCenterPosition, Position newCenterPosition)
        {
            Mino = mino;
            MotionType = motionType;
            PreviousCenterPosition = previousCenterPosition;
            NewCenterPosition = newCenterPosition;
        }
    }
}
