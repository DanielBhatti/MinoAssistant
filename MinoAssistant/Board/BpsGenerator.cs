using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board
{
    public class BpsGenerator : IGenerator
    {
        private Queue<Mino> Queue { get; } = new();
        private Random Random { get; } = new();

        public List<Mino> Peek(int n)
        {
            while (Queue.Count < n) FillQueue();
            List<Mino> minos = new();
            for (int i = 0; i < n; i++) minos.Add(Queue.ElementAt(i));
            return minos;
        }

        public Mino Pop()
        {
            if (Queue.Count == 0) FillQueue();
            return Queue.Dequeue();
        }

        private void FillQueue()
        {
            List<Mino> minos = MinoFactory.GenerateBasicFourMinos().ToList();
            while (minos.Count > 0)
            {
                int randomIndex = Random.Next(0, minos.Count);
                Mino mino = minos[randomIndex];
                minos.RemoveAt(randomIndex);
                Queue.Enqueue(mino);
            }
        }
    }
}
