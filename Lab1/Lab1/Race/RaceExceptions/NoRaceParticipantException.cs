namespace Lab1.Race
{
    public class NoRaceParticipantsException : Exception
    {
        public NoRaceParticipantsException() : base("Race must have at least 2 participants") { }
    }
}
