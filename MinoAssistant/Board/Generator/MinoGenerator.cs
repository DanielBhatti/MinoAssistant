using MinoAssistant.Board.Block;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board.Generator;
public class MinoGenerator : IMinoGenerator
{
    public virtual IEnumerable<Mino> Minos { get; }
    protected Queue<Mino> Queue { get; } = new();

    private Random Random { get; } = new();

    public MinoGenerator(Mino mino) : this(new List<Mino>() { mino }) { }
    public MinoGenerator(IEnumerable<Mino> minos) => Minos = minos;

    public List<Mino> Peek(int n)
    {
        while (Queue.Count < n) AddToQueue();
        List<Mino> nextMinos = new();
        for (int i = 0; i < n; i++) nextMinos.Add(Queue.ElementAt(i));
        return nextMinos;
    }

    public Mino Pop()
    {
        if (Queue.Count == 0) AddToQueue();
        return Queue.Dequeue();
    }

    protected virtual void AddToQueue()
    {
        var randomPermutation = Enumerable.Range(0, Minos.Count()).OrderBy(m => Random.NextInt64());
        foreach (int i in randomPermutation) Queue.Enqueue(Minos.ElementAt(i));
    }
}
