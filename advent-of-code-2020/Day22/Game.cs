using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class Game
    {
        public WinningState Play(
            Deck[] decks,
            ICombatRules combatRules)
        {
            while (combatRules.KeepPlayingGame(decks))
            {
                var cards = decks.Select(deck => deck.Draw()).ToList();

                int bestPlayer = combatRules.GetBestPlayer(decks, cards, getHigherCard);

                giveCardToWinner(decks, cards, bestPlayer);
            }

            return combatRules.GetWinningState(decks);
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
