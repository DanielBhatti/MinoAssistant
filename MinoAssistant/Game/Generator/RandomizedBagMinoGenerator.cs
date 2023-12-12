using MinoAssistant.Game.Block;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Game.Generator;

public class RandomizedBagMinoGenerator : MinoGenerator
{
    public virtual IEnumerable<Mino> Minos { get; }
    protected Queue<Mino> Queue { get; } = new();

    private Random Random { get; } = new();

    public RandomizedBagMinoGenerator(Mino mino) : this(new List<Mino>() { mino }) { }
    public RandomizedBagMinoGenerator(IEnumerable<Mino> minos) => Minos = minos;

    public IOrderedEnumerable<Mino> Peek(int n)
    {
        while(Queue.Count < n) AddToQueue();
        return Queue.Take(n).OrderBy(m => m);
    }

    public Mino Pop()
    {
        if(Queue.Count == 0) AddToQueue();
        return Queue.Dequeue();
    }

    protected virtual void AddToQueue()
    {
        var randomPermutation = Enumerable.Range(0, Minos.Count()).OrderBy(m => Random.NextInt64());
        foreach(var i in randomPermutation) Queue.Enqueue(Minos.ElementAt(i));
    }
}
