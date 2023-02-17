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
            var decks = getDecks(inputData).ToArray();

            var score = new Game().Play(decks, combatRules);

            Console.WriteLine($"The score is {score}");
        }

        protected override void performWorkForProblem2(IList<string> inputData)
        {
            performWork(inputData,
                new RecursiveCombat());
        }

        private IEnumerable<Deck> getDecks(IEnumerable<string> inputData)
        {
            Deck deck = null;

            foreach (var line in inputData)
            {
                if (line.StartsWith(Player))
                {
                    var playerNumberStr = line.Substring(Player.Length, 1);
                    deck = new Deck(int.Parse(playerNumberStr));
                }

                else if (string.IsNullOrEmpty(line))
                {
                    yield return deck;
                }

                else
                {
                    deck.Add(int.Parse(line));
                }
            }

            yield return deck;
        }
    }
}
