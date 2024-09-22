

namespace Lab1.Vehicle
{
    internal abstract class Vehicle
    {
        public abstract void Move(double time);
        public double DistanceFromStart { set; get; } = 0;
        public abstract double Speed { set; get; }

    }

}
