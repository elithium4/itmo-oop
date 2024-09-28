namespace Lab1.Race
{

    public class NegativeRaceDistanceException : Exception
    {
        public NegativeRaceDistanceException() : base("Race distance must be positive number") { }
    }
}