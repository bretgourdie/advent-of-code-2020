using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class PlayerDeck
    {
        private readonly int player;
        private readonly Queue<int> deck;

        public PlayerDeck(
            int player)
        {
            this.player = player;
            this.deck = new Queue<int>();
        }

        public void Add(int card)
        {
            deck.Enqueue(card);
        }

        public int Draw()
        {
            return deck.Dequeue();
        }

        public IEnumerable<int> Cards()
        {
            return deck.AsEnumerable();
        }

        public bool HasCards() => deck.Any();

        public int GetScore()
        {
            var deckList = deck.Reverse().ToList();

            int total = 0;
            for (int ii = 0; ii < deckList.Count; ii++)
            {
                total += deckList[ii] * (ii + 1);
            }

            return total;
        }

        public override string ToString()
        {
            return $"Player {player}'s deck: " + String.Join(", ", deck);
        }
    }
}
