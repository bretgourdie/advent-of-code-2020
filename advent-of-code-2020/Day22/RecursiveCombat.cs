using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class RecursiveCombat : ICombatRules
    {
        private readonly IDictionary<string, Deck[]> hashToDecks;

        public RecursiveCombat()
        {
            hashToDecks = new Dictionary<string, Deck[]>();
        }

        public bool KeepPlayingGame(Deck[] decks)
        {
            var handHasBeenPlayed = handsHaveBeenPlayed(decks);

            if (!handHasBeenPlayed)
            {
                recordHand(decks);
            }

            return !handHasBeenPlayed && decks.All(deck => deck.HasCards());
        }

        private bool handsHaveBeenPlayed(Deck[] decks)
        {
            var currentHash = getDeckHash(decks);

            var decksInHash = hashToDecks.ContainsKey(currentHash);

            return decksInHash;
        }

        public bool ShouldPlaySubGame(Deck[] decks, IList<int> playerCards)
        {
            for (int ii = 0; ii < decks.Length; ii++)
            {
                var cardCount = decks[ii].CardCount();
                var playingCard = playerCards[ii];

                if (playingCard > cardCount)
                {
                    return false;
                }
            }

            return true;
        }

        public WinningState GetWinningState(Deck[] decks)
        {
            for (int ii = 0; ii < decks.Length; ii++)
            {
                if (!decks[ii].HasCards())
                {
                    return new WinningState(
                        decks[(ii + 1) % decks.Length],
                        decks[ii]);
                }
            }

            // We still have cards, so "seen game" must have triggered
            // Player 1 wins in this case
            return new WinningState(
                decks[0],
                decks[1]);
        }

        public ICombatRules ForSubGame()
        {
            return new RecursiveCombat();
        }

        private void recordHand(Deck[] decks)
        {
            var deckHash = getDeckHash(decks);

            if (!hashToDecks.ContainsKey(deckHash))
            {
                hashToDecks.Add(deckHash, decks);
            }
        }

        private string getDeckHash(Deck[] decks)
        {
            return
                String.Join(";", (object[]) decks);
        }
    }
}
