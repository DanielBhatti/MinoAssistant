namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        public Cell[,] Cells { get; }

        public Field(int width, int height) => Cells = new Cell[width, height];
    }
}
