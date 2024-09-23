namespace Lab1.Race
{
    enum RaceType
    {
        AIR,
        GROUND,
        MIXED
    }
    internal class Race
    {
        public RaceType Type { get; set; }

        public double Distance { get; set; }
        private List<Vehicle.Vehicle> Vehicles = [];
        public Race(RaceType type, double distance)
        {
            Type = type;
            Distance = distance;
        }

        public void RegisterVehicle(Vehicle.Vehicle t)
        {
            Console.WriteLine(t is Vehicle.AirVehicle);
            Console.WriteLine(Type);
            Console.WriteLine(t is Vehicle.GroundVehicle);
            if (Type == RaceType.MIXED || Type == RaceType.AIR && t is Vehicle.AirVehicle || Type == RaceType.GROUND && t is Vehicle.GroundVehicle)
            {
                Console.WriteLine("add");
                Vehicles.Add(t);
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Oops");
                throw new Exception("Unsupported Vehicle for this race type");
            }
        }

        public void StartRace()
        {
            Console.WriteLine($"Starting race, distance: {Distance}");
            double t = 0;
            List<Vehicle.Vehicle> finishedVehicles = [];

            while (Vehicles.Count > 0)
            {
                for (int i = 0; i < Vehicles.Count; i++)
                {
                    Vehicles[i].Move(t);
                    if (Vehicles[i].DistanceFromStart >= Distance) {
                        var vehicle = Vehicles[i];
                        Console.WriteLine($"Finished as {finishedVehicles.Count+1} with time {t}: {vehicle.GetType().Name}");
                        finishedVehicles.Add(vehicle);
                        Vehicles.Remove(vehicle);
                    }
                }
                t += 0.1;
            }
        }

    }
}
