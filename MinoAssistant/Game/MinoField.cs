using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion;
using MinoAssistant.Game.Motion.Rotation;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Game;

public class MinoField : Field
{
    public bool IsShowingGhostPiece { get; }

    private readonly HashSet<(Position Position, MinoColor MinoColor)> _placedBlocks = new();
    public IReadOnlyCollection<(Position Position, MinoColor MinoColor)> PlacedBlocks => _placedBlocks;
    public MinoContext? MinoContext { get; private set; }

    public MinoField(int width, int height, MinoColor defaultColor, bool isShowingGhostPiece = false) : base(width, height, defaultColor) => IsShowingGhostPiece = isShowingGhostPiece;

    public bool AddMino(MinoContext minoContext)
    {
        if(MinoContext.HasValue) return false;
        MinoContext = minoContext;
        return true;
    }

    public void LockMino()
    {
        if(!MinoContext.HasValue) return;
        foreach(var p in MinoContext.Value.AbsolutePositions) _ = _placedBlocks.Add((p, MinoContext.Value.Mino.MinoColor));
        MinoContext = null;
    }

    public bool MoveMino(Position relativeShift) => MoveAndRotateMino(relativeShift, RotationAmount.R0);
    public bool RotateMino(RotationAmount relativeRotation) => MoveAndRotateMino(Position.Zero, relativeRotation);
    public bool MoveAndRotateMino(Position relativeShift, RotationAmount relativeRotation)
    {
        if(!MinoContext.HasValue) return false;
        var newMinoContext = new MinoContext() { Mino = MinoContext.Value.Mino, CenterPosition = MinoContext.Value.CenterPosition + relativeShift, RotationAmount = MinoContext.Value.RotationAmount + relativeRotation};
        if(IsAnyFilled(newMinoContext.AbsolutePositions)) return false;
        MinoContext = newMinoContext;
        Refresh();
        return true;
    }

    private void RefreshPiece()
    {
        if(!MinoContext.HasValue) return;
        if(IsShowingGhostPiece) Reset(GetGhostPiecePositions());
        base.Reset(MinoContext.Value.AbsolutePositions);
        base.Set(MinoContext.Value.AbsolutePositions, MinoContext.Value.Mino.MinoColor);
        if(IsShowingGhostPiece) base.Set(GetGhostPiecePositions(), MinoContext.Value.Mino.GhostMinoColor);
    }

    protected IEnumerable<Position> GetGhostPiecePositions()
    {
        if(!MinoContext.HasValue) return new Position[0];
        var ghostPositions = MinoContext.Value.AbsolutePositions;
        while(ghostPositions.Select(p => p + (0, -1)).All(IsFilled)) ghostPositions = ghostPositions.Select(p => p + (0, -1));
        return ghostPositions;
    }

    protected void Refresh()
    {
        ResetField();
        foreach(var block in PlacedBlocks) Set(block.Position, block.MinoColor);
        RefreshPiece();
    }
}