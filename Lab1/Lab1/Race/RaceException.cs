namespace Lab1.Race
{
    public class BadRaceTypeException : Exception
    {
        public BadRaceTypeException() : base("Unacceptable race type") { }
    }

    public class BadRaceParticipantException : Exception
    {
        public BadRaceParticipantException() : base("Unacceptable participant type for this race type") { }
    }

    public class NoRaceParticipantsException : Exception
    {
        public NoRaceParticipantsException() : base("Race must have at least 2 participants") { }
    }

    public class BadRaceDistanceException : Exception
    {
        public BadRaceDistanceException() : base("Race distance must be positive number") { }
    }
}