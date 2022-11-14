using System.Linq;

namespace advent_of_code_2020.Day22
{
    class Combat : ICombatRules
    {

        public bool KeepPlayingGame(PlayerDeck[] decks) => decks.All(deck => deck.HasCards());
        public void EvaluateRound(PlayerDeck[] decks) { }

        public bool ShouldPlaySubGame(PlayerDeck[] decks, int[] playerCards) => false;
        public int GetWinner(PlayerDeck[] decks)
        {
            for (int ii = 0; ii < decks.Length; ii++)
            {
                if (decks[ii].HasCards()) return ii;
            }

            return -1;
        }
    }
}
