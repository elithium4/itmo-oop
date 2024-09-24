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
            if (Type == RaceType.MIXED || Type == RaceType.AIR && t is Vehicle.AirVehicle || Type == RaceType.GROUND && t is Vehicle.GroundVehicle)
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
