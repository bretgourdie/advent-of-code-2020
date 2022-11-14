namespace advent_of_code_2020.Day22
{
    public interface ICombatRules
    {
        bool KeepPlayingGame(PlayerDeck[] decks);
        void EvaluateRound(PlayerDeck[] decks);
        bool ShouldPlaySubGame(PlayerDeck[] decks, int[] playerCards);
        int GetWinner(PlayerDeck[] decks);
    }
}
