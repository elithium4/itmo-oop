
namespace Lab1.Race
{
    public class BadRaceParticipantException : Exception
    {
        public BadRaceParticipantException() : base("Unacceptable participant type for this race type") { }
    }
}
