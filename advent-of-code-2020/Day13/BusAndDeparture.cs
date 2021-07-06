namespace advent_of_code_2020.Day13
{
    struct BusAndDeparture
    {
        public readonly int Id;
        public readonly int FirstDeparture;

        public BusAndDeparture(
            int id,
            int firstDeparture)
        {
            Id = id;
            FirstDeparture = firstDeparture;
        }
    }
}
