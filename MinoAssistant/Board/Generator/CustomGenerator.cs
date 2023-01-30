using MinoAssistant.Board.Block;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board.Generator
{
    public class CustomGenerator : IMinoGenerator
    {
        public Mino NextPiece { get; set; } = MinoFactory.GenerateBasicMino(MinoType.FourminoT);

        public List<Mino> Peek(int n) => Enumerable.Range(0, n).Select(num => NextPiece).ToList();

        public Mino Pop() => NextPiece;

        public Mino Pop(Mino mino)
        {
            NextPiece = mino;
            return Pop();
        }
    }
}
