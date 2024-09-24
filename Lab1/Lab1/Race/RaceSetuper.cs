public class RaceSetuper()
{
    public Race SetupRace()
    {
        RaceType type = GetRaceType();
        double distance = GetRaceDistance();
        Race race = new Race(type, distance);
        return race;
    }

    private RaceType GetRaceType()
    {
        Console.WriteLine("Select race type: AIR, GROUND or MIXED");
        while (true)
        {
            RaceType selectedRaceType;
            string selection = Console.ReadLine();
            if (Enum.TryParse(selection, out selectedRaceType))
            {
                return selectedRaceType;
            }
            else
            {
                Console.WriteLine("Try Entering again");
            }
        }
    }

    private double GetRaceDistance()
    {
        while (true)
        {
            double distance;
            Console.WriteLine("Enter race distance: ");
            if (double.TryParse(Console.ReadLine(), out distance))
            {
                if (distance > 0) {
                    return distance
                }
            }
        }
    }

    private List<Vehicle> AddVehicles(Race race) {

    }
}