using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class RecursiveCombat : ICombatRules
    {
        private readonly IDictionary<int, IList<IList<Deck>>> hashToDecks;

        public RecursiveCombat()
        {
            hashToDecks = new Dictionary<int, IList<IList<Deck>>>();
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
            var currentHash = decks.Sum(deck => deck.Cards().GetHashCode());

            if (hashToDecks.ContainsKey(currentHash))
            {
                var hands = hashToDecks[currentHash];

                foreach (var hand in hands)
                {
                    if (
                        handMatchesDeck(hand[0].Cards(), decks[0].Cards())
                        && handMatchesDeck(hand[1].Cards(), decks[1].Cards())
                       )
                    {
                        return true;
                    }
                }
            }



            return false;
        }

        private bool handMatchesDeck(
            IEnumerable<int> hand,
            IEnumerable<int> deck)
        {
            return hand.SequenceEqual(deck);
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
            var deckHashes = decks.Sum(deck => deck.Cards().GetHashCode());

            if (!hashToDecks.ContainsKey(deckHashes))
            {
                hashToDecks.Add(deckHashes, new List<IList<Deck>>());
            }

            hashToDecks[deckHashes].Add(
                new List<Deck>()
                {
                    decks[0],
                    decks[1]
                }
            );
        }
    }
}
