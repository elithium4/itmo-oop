
using Lab1.Vehicles;

namespace Lab1.Race
{
    public static class RaceSetuper
    {
        public static Race SetupRace()
        {
            RaceType type = GetRaceType();
            double distance = GetRaceDistance();
            Race race = new Race(type, distance);
            RegisterVehicles(race);
            return race;
        }

        private static RaceType GetRaceType()
        {
            Console.WriteLine("Select race type: Air, Ground or Mixed");
            while (true)
            {
                RaceType selectedRaceType;
                if (Enum.TryParse(Console.ReadLine(), true, out selectedRaceType))
                {
                    return selectedRaceType;
                }
                else
                {
                    Console.WriteLine("Couldn't recognize race type, try again");
                }
            }
        }

        private static double GetRaceDistance()
        {
            while (true)
            {
                double distance;
                Console.WriteLine("Enter race distance: ");
                if (double.TryParse(Console.ReadLine(), out distance))
                {
                    if (distance > 0)
                    {
                        return distance;
                    }
                    else
                    {
                        Console.WriteLine("Race distance must be positive");
                    }
                }
            }
        }

        private static void RegisterVehicles(Race race)
        {
            List<Vehicle> availableTypes = new List<Vehicle> {
                new Broom(),
                new BabaYagaMortar(),
                new FlyingShip(),
                new MagicCarpet(),
                new Centaur(),
                new ChickenLegsHut(),
                new MagicBoots(),
                new PumpkinCarriage()
            };
            List<Vehicle> raceParticipants = [];

            Console.WriteLine("Select vehicles to register. Enter -1 to finish.");
            for (int i = 0; i < availableTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {availableTypes[i].GetType().Name} ({(availableTypes[i] is AirVehicle ? "Air type" : "Ground type")})");
            }

            int possibleParticipants = availableTypes.Count;
            int registeredParticipants = 0;

            while (possibleParticipants > 0)
            {
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < -1 || choice > availableTypes.Count)
                {
                    continue;
                }
                if (choice == -1)
                {
                    if (registeredParticipants < 2)
                    {
                        Console.WriteLine("You must register at least 2 participants");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Vehicle vehicle = availableTypes[choice - 1];
                    if (raceParticipants.Contains(vehicle))
                    {
                        Console.WriteLine($"{vehicle.GetType().Name} already selected");
                    }
                    else
                    {
                        try
                        {
                            race.RegisterVehicle(vehicle);
                            Console.WriteLine($"New participant registered: {vehicle.GetType().Name}");
                            possibleParticipants--;
                            registeredParticipants++;
                        }
                        catch (BadRaceParticipantException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}