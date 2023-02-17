namespace advent_of_code_2020.Day22
{
    public class WinningState
    {
        public readonly Deck WinningDeck;
        public readonly Deck LosingDeck;

        public WinningState(
            Deck winningDeck,
            Deck losingDeck)
        {
            WinningDeck = winningDeck;
            LosingDeck = losingDeck;
        }
    }
}
