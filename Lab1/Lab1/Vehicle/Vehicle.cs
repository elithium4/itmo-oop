

namespace Lab1.Vehicles
{
    public abstract class Vehicle
    {
        public abstract void Move(double time);
        public double DistanceFromStart { set; get; } = 0;
        public abstract double Speed { set; get; }

    }

}
