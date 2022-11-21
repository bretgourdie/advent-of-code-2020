using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class Game
    {
        public int Play(
            Deck[] playerDecks,
            ICombatRules combatRules)
        {
            var winningState = playGame(playerDecks, combatRules);

            var score = winningState.WinningDeck.GetScore();

            return score;
        }

        private WinningState playGame(
            Deck[] decks,
            ICombatRules combatRules)
        {
            while (combatRules.KeepPlayingGame(decks))
            {
                var cards = decks.Select(deck => deck.Draw()).ToList();

                int bestPlayer = 0;

                if (combatRules.ShouldPlaySubGame(decks, cards))
                {
                    var slicedDecks = sliceDecks(decks, cards);

                    var subgameResult = playGame(slicedDecks, combatRules.ForSubGame());

                    bestPlayer = subgameResult.WinningDeck.PlayerZeroIndex;
                }

                else
                {
                    bestPlayer = getHigherCard(cards);
                }

                giveCardToWinner(decks, cards, bestPlayer);
            }

            return combatRules.GetWinningState(decks);
        }

        private Deck[] sliceDecks(
            Deck[] decks,
            IList<int> cards)
        {
            var slicedDecks = new Deck[decks.Length];

            for (int ii = 0; ii < decks.Length; ii++)
            {
                var slicedDeckCards = decks[ii].Cards().Take(cards[ii]);

                var slicedDeck = new Deck(decks[ii].Player);

                foreach (var slicedDeckCard in slicedDeckCards)
                {
                    slicedDeck.Add(slicedDeckCard);
                }

                slicedDecks[ii] = slicedDeck;
            }

            return slicedDecks;
        }

        private void giveCardToWinner(
            Deck[] decks,
            IList<int> cards,
            int bestPlayer)
        {
            decks[bestPlayer].Add(cards[bestPlayer]);
            decks[bestPlayer].Add(cards[(bestPlayer + 1) % cards.Count]);
        }

        private int getHigherCard(IList<int> cards)
        {
            return cards.IndexOf(cards.Max());
        }
    }
}
