using System.Collections.Generic;

namespace MinoAssistant.Board
{
    public interface IGenerator
    {
        List<Mino> Peek(int n);

        Mino Pop();
    }
}
