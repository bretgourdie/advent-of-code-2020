namespace advent_of_code_2020.Day02
{
    class TobogganPolicy : IPolicy
    {
        public bool Passes(
            int a,
            int b,
            char letter,
            string password)
        {
            return password[a - 1] == letter ^ password[b - 1] == letter;
        }
    }
}
