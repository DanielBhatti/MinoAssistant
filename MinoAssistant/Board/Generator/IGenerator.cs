using MinoAssistant.Board.Block;
using System.Collections.Generic;

namespace MinoAssistant.Board.Generator
{
    public interface IGenerator
    {
        List<Mino> Peek(int n);

        Mino Pop();
    }
}
