using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    class Solution : AdventSolution
    {
        private const string Player = "Player ";

        protected override void performWorkForProblem1(IList<string> inputData)
        {
            performWork(inputData,
                new Combat());
        }

        protected void performWork(
            IList<string> inputData,
            ICombatRules combatRules)
        {
            var decks = getPlayerDecks(inputData).ToArray();

            playGame(decks);

            while (combatRules.KeepPlayingGame(decks))
            {
                combatRules.EvaluateRound(decks);
            }

            var score = decks.Single(deck => deck.HasCards()).GetScore();

            Console.WriteLine($"The score is {score}");
        }

        private int playGame(
            PlayerDeck[] decks,
            ICombatRules combatRules)
        {
            while (combatRules.KeepPlayingGame(decks))
            {
                combatRules.EvaluateRound(decks);

                var cards = decks.Select(x => x.Draw()).ToArray();

                int bestIndex = 0;

                if (combatRules.ShouldPlaySubGame(decks, cards))
                {
                    var decksSlice = sliceDecks(decks, cards);

                    bestIndex = playGame(decksSlice, combatRules);
                }

                else
                {
                    for (int cardIndex = bestIndex + 1; cardIndex < cards.Length; cardIndex++)
                    {
                        if (cards[cardIndex] > cards[bestIndex])
                        {
                            bestIndex = cardIndex;
                        }
                    }
                }

                decks[bestIndex].Add(cards[bestIndex]);
                decks[bestIndex].Add(cards[(bestIndex + 1) % cards.Length]);
            }

            return combatRules.GetWinner(decks);
        }

        private PlayerDeck[] sliceDecks(PlayerDeck[] decks, int[] cards)
        {
            var slicedDecks = new PlayerDeck[decks.Length];

            for (int ii = 0; ii < decks.Length; ii++)
            {
                var slicedDeck = new PlayerDeck(ii);
                var cardsToAdd = decks[ii].Cards().Take(cards[ii]);
                foreach (var card in cardsToAdd)
                {
                    slicedDeck.Add(card);
                }

                slicedDecks[ii] = slicedDeck;
            }

            return slicedDecks;
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<PlayerDeck> getPlayerDecks(IEnumerable<string> inputData)
        {
            PlayerDeck playerDeck = null;

            foreach (var line in inputData)
            {
                if (line.StartsWith(Player))
                {
                    var playerNumberStr = line.Substring(Player.Length, 1);
                    playerDeck = new PlayerDeck(int.Parse(playerNumberStr));
                }

                else if (string.IsNullOrEmpty(line))
                {
                    yield return playerDeck;
                }

                else
                {
                    playerDeck.Add(int.Parse(line));
                }
            }

            yield return playerDeck;
        }
    }
}
