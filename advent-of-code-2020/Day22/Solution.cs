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

            while (combatRules.KeepPlayingGame(decks))
            {
                combatRules.EvaluateRound(decks);
            }

            var score = decks.Single(deck => deck.HasCards()).GetScore();

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
