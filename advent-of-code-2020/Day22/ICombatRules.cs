using System;
using System.Collections.Generic;

namespace advent_of_code_2020.Day22
{
    public interface ICombatRules
    {
        bool KeepPlayingGame(Deck[] decks);
        int GetBestPlayer(Deck[] decks, IList<int> playerCards, Func<IList<int>, int> getHigherCardFunction);
        WinningState GetWinningState(Deck[] decks);
    }
}
