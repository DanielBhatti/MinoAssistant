using MinoAssistant.Game.Block;

namespace MinoAssistant.Game.Generator;

public class BpsGenerator : RandomizedBagMinoGenerator
{
    public BpsGenerator() : base(MinoFactory.GenerateBasicFourMinos()) { }
}
