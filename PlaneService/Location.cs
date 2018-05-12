namespace PlaneFinder.Service
{
    public class Location
    {
        public double? Longitude { get; set;}
        public double? Latitude { get; set; }  

        public double Long { get { return Longitude ?? double.MaxValue; } }
        public double Lat { get { return Latitude ?? double.MaxValue; } }

        public bool IsValid { get { return (Longitude != null && Latitude != null); } }


    }
}