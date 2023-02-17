using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class Deck
    {
        public readonly int Player;
        public int PlayerZeroIndex => Player - 1;
        private readonly Queue<int> cards;

        public Deck(
            int player)
        {
            this.Player = player;
            this.cards = new Queue<int>();
        }

        public void Add(int card)
        {
            cards.Enqueue(card);
        }

        public int Draw()
        {
            return cards.Dequeue();
        }

        public IEnumerable<int> Cards()
        {
            return cards.AsEnumerable();
        }

        public int CardCount() => cards.Count;

        public bool HasCards() => cards.Any();

        public int GetScore()
        {
            var deckList = cards.Reverse().ToList();

            int total = 0;
            for (int ii = 0; ii < deckList.Count; ii++)
            {
                total += deckList[ii] * (ii + 1);
            }

            return total;
        }

        public override string ToString()
        {
            return $"Player {Player}'s deck: " + String.Join(", ", cards);
        }
    }
}
