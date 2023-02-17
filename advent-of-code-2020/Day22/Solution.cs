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
            var decks = getPlayerDecks(inputData).ToArray();

            for (int round = 1; decks.All(deck => deck.HasCards()); round++)
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

            var score = decks.Where(deck => deck.HasCards()).Single().GetScore();

            Console.WriteLine($"The score is {score}");
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
