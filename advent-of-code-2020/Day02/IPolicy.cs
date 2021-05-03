namespace advent_of_code_2020.Day02
{
    interface IPolicy
    {
        bool Passes(int firstNumber, int secondNumber, char letter, string password);
    }
}
