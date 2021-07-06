namespace advent_of_code_2020.Day13
{
    class BusDeparture
    {
        public readonly int Id;

        protected BusDeparture() { }

        public BusDeparture(int id)
        {
            Id = id;
        }

        public virtual bool TimeStampWorks(long baseTimeStamp, int indexOffset)
        {
            return (baseTimeStamp + indexOffset) % Id == 0;
        }
    }
}
