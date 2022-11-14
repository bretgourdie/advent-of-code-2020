using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2020.Day22
{
    public class RecursiveCombat : ICombatRules
    {
        private readonly HashSet<HashSet<HashSet<int>>> hands;

        public RecursiveCombat()
        {
            hands = new HashSet<HashSet<HashSet<int>>>();
        }

        public bool KeepPlayingGame(PlayerDeck[] decks)
        {
            if (hands.Contains(getDecksHash(decks)))
            {
                return false;
            }

            return decks.All(deck => deck.HasCards());
        }

        public bool ShouldPlaySubGame(PlayerDeck[] decks, int[] playerCards)
        {
            bool shouldPlaySubGame = true;

            for (int ii = 0; ii < decks.Length; ii++)
            {
                shouldPlaySubGame &= decks[ii].Cards().Count() >= playerCards[ii];
            }

            return shouldPlaySubGame;
        }

        public int GetWinner(PlayerDeck[] decks)
        {
            throw new NotImplementedException();
        }

        public void EvaluateRound(PlayerDeck[] decks)
        {
            recordHand(decks);
        }

        private void recordHand(PlayerDeck[] decks)
        {
            hands.Add(getDecksHash(decks));
        }

        private HashSet<HashSet<int>> getDecksHash(PlayerDeck[] decks)
        {
            var masterHash = new HashSet<HashSet<int>>();

            foreach (var deck in decks)
            {
                masterHash.Add(
                    new HashSet<int>(deck.Cards()));
            }

            return masterHash;
        }
    }
}
