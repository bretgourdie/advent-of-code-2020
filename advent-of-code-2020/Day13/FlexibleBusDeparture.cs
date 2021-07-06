namespace advent_of_code_2020.Day13
{
    class FlexibleBusDeparture : BusDeparture
    {
        public FlexibleBusDeparture() : base() { }

        public override bool TimeStampWorks(long baseTimeStamp, int indexOffset)
        {
            return true;
        }
    }
}
