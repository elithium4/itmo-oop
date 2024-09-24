
using Lab1.Vehicles;
using System.Linq;

namespace Lab1.Race
{
    public class RaceSetuper()
    {
        public Race SetupRace()
        {
            RaceType type = GetRaceType();
            double distance = GetRaceDistance();
            Race race = new Race(type, distance);
            List<Vehicle> vehicles = SelectVehicles();
            foreach (var item in vehicles)
            {
                race.RegisterVehicle(item);
            }
            return race;
        }

        private RaceType GetRaceType()
        {
            Console.WriteLine("Select race type: AIR, GROUND or MIXED");
            while (true)
            {
                RaceType selectedRaceType;
                if (Enum.TryParse(Console.ReadLine(), out selectedRaceType))
                {
                    return selectedRaceType;
                }
                else
                {
                    throw new BadRaceTypeException();
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
                    if (distance > 0)
                    {
                        return distance;
                    } else
                    {
                        throw new BadRaceDistanceException();
                    }
                }
            }
        }

        private List<Vehicle> SelectVehicles()
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

            Console.WriteLine($"Select {(raceParticipants.Count == 0 ? "vehicle" : "more vehicles")} to register. Enter -1 to finish.");
            for (int i = 0; i < availableTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {availableTypes[i].GetType().Name} ({(availableTypes[i] is AirVehicle ? "Air type" : "Ground type")})");
            }

            int possibleParticipants = availableTypes.Count;

            while (possibleParticipants > 0)
            {
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < -1 || choice > availableTypes.Count)
                {
                    continue;
                }
                if (choice == -1)
                {
                    break;
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
                        raceParticipants.Add(vehicle);
                        possibleParticipants--;
                        Console.WriteLine($"New participant: {vehicle.GetType().Name}");
                    }
                }
            }
            return raceParticipants;
        }
    }
}