using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020.Day22
{
    public class Combat : ICombatRules
    {

        public bool KeepPlayingGame(Deck[] decks) => decks.All(deck => deck.HasCards());

        public int GetBestPlayer(
            Deck[] decks,
            IList<int> playerCards,
            Func<IList<int>, int> getHigherCardFunction)
        {
            return getHigherCardFunction(playerCards);
        }

        public WinningState GetWinningState(Deck[] decks)
        {
            for (int ii = 0; ii < decks.Length; ii++)
            {
                if (decks[ii].HasCards())
                {
                    return new WinningState(
                        decks[ii],
                        decks[(ii + 1) % decks.Length]);
                }
            }

            throw new ArgumentException("Both decks have cards");
        }
    }
}
