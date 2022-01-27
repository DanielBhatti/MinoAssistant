namespace MinoAssistant.Board
{
    public class Cell
    {
        public object? Value { get; set; }
        public bool IsFilled { get => Value != null; }

        public void Clear() => Value = null;
    }
}
