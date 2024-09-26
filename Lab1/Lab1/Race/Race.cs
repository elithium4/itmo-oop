using Lab1.Vehicles;

namespace Lab1.Race
{
    public class Race
    {
        public RaceType Type { get; set; }

        public double Distance { get; set; }
        private List<Vehicle> Vehicles = [];
        public Race(RaceType type, double distance)
        {
            Type = type;
            if (distance <= 0) {
                throw new NegativeRaceDistanceException();
            }
            Distance = distance;
        }

        public void RegisterVehicle(Vehicle t)
        {
            if (Type == RaceType.Mixed || Type == RaceType.Air && t is AirVehicle || Type == RaceType.Ground && t is GroundVehicle)
            {
                Vehicles.Add(t);
            }
            else
            {
                throw new BadRaceParticipantException();
            }
        }

        public void StartRace()
        {
            if (Vehicles.Count < 2) {
                throw new NoRaceParticipantsException();
            }
            Console.WriteLine($"Race has started!");
            double t = 0;
            List<Vehicle> finishedVehicles = [];

            while (Vehicles.Count > 0)
            {
                for (int i = 0; i < Vehicles.Count; i++)
                {
                    Vehicles[i].Move(t);
                    if (Vehicles[i].DistanceFromStart >= Distance) {
                        var vehicle = Vehicles[i];
                        Console.WriteLine($"Finished  #{finishedVehicles.Count+1} with time {t}: {vehicle.GetType().Name}");
                        finishedVehicles.Add(vehicle);
                        Vehicles.Remove(vehicle);
                    }
                }
                t += 0.5;
            }
        }

    }
}
