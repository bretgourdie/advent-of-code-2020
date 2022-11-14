using System.Linq;

namespace advent_of_code_2020.Day22
{
    class Combat : ICombatRules
    {

        public bool KeepPlayingGame(PlayerDeck[] decks) => decks.All(deck => deck.HasCards());

        public void EvaluateRound(PlayerDeck[] decks)
        {
            var cards = decks.Select(x => x.Draw()).ToList();

            int bestIndex = 0;
            for (int cardIndex = bestIndex + 1; cardIndex < cards.Count; cardIndex++)
            {
                if (cards[cardIndex] > cards[bestIndex])
                {
                    bestIndex = cardIndex;
                }
            }

            decks[bestIndex].Add(cards[bestIndex]);
            decks[bestIndex].Add(cards[(bestIndex + 1) % cards.Count]);
        }
    }
}
