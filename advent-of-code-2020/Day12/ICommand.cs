namespace advent_of_code_2020.Day12
{
    public interface ICommand
    {
        Transform Resolve(Transform transform);
    }
}
