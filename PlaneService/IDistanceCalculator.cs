namespace PlaneFinder.Service
{
    public interface IDistanceCalculator
    {
        double Calculate(Location a, Location b);
    }
}