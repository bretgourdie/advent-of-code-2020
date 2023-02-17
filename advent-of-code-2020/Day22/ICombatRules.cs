using System.Collections.Generic;

namespace advent_of_code_2020.Day22
{
    public interface ICombatRules
    {
        bool KeepPlayingGame(Deck[] decks);
        bool ShouldPlaySubGame(Deck[] decks, IList<int> playerCards);
        WinningState GetWinningState(Deck[] decks);
        ICombatRules ForSubGame();
    }
}
