using System;

namespace PlaneFinder.Service
{
    public class EuclideanDistanceCalculator : IDistanceCalculator
    {
        public double Calculate(Location a, Location b)
        {
            
            if (!(a.IsValid && b.IsValid))
                return double.MaxValue;

            double dist = Math.Sqrt( Math.Pow((b.Long - a.Long), 2.0) 
                                     + Math.Pow(b.Lat - a.Lat, 2.0));

            return dist;
        }   
    }
}