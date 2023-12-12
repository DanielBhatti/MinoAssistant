using MinoAssistant.Game.Block;
using System.Linq;

namespace MinoAssistant.Game.Generator;

public interface MinoGenerator
{
    IOrderedEnumerable<Mino> Peek(int n);

    Mino Pop();
}
