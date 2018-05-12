using System.Collections.Generic;

namespace PlaneFinder.Service
{
    public class PlaneFinder
    {
        
        public string Data { get; set; }

        public Location GivenLocation {get; set;}

        public List<Plane> Planes { get; private set;}

        public IDistanceCalculator Calc { get; set; }
        public PlaneFinder(string data, Location loc, IDistanceCalculator calc)
        {
            Data = data;
            GivenLocation = loc;
            Planes = new List<Plane>();
            Calc = calc;
        }

        public Plane FindClosestPlane()
        {
            var osr = PlaneService.Deserialize(Data);

            double minDistance = double.MaxValue;
            Plane closest = null;

            for( int i = 0; i < osr.StateCount; i++)
            {
                Plane p = new Plane(osr.GetCallSign(i),
                                    osr.GetLongitude(i),
                                    osr.GetLatitude(i),
                                    osr.GetGeoAltitude(i),
                                    osr.GetOriginCountry(i),
                                    osr.GetIcao24(i)
                                    );
                p.Distance = Calc.Calculate(GivenLocation, p.PlaneLocation);
                if (p.Distance < minDistance)
                {
                    closest = p;
                    minDistance = p.Distance;
                }
            }
            
            return closest;

        }

    }
}