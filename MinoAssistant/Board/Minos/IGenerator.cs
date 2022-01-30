using System.Collections.Generic;

namespace MinoAssistant.Board.Minos
{
    public interface IGenerator
    {
        List<Mino> Peek(int n);

        Mino Pop();
    }
}
