using MinoAssistant.Board.Block;
using System.Collections.Generic;

namespace MinoAssistant.Board.Generator
{
    public interface IMinoGenerator
    {
        IEnumerable<Mino> Minos { get; }

        List<Mino> Peek(int n);

        Mino Pop();
    }
}
