namespace PlaneFinder.Service {
    public class Plane 
    {
        public Plane (string callSign, double? longitude, double? latitude, double? altitude, string countryOfOrigin, string iCao24) 
        {
            this.CallSign = callSign;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Altitude = altitude;
            this.CountryOfOrigin = countryOfOrigin;
            this.ICao24 = iCao24;

        }
        public Location PlaneLocation 
        {
            get
            {
                return new Location(){Longitude = this.Longitude, Latitude = this.Latitude};
            }   
        }

        public double Distance { get; set; }
        public string CallSign { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Altitude { get; set; }
        public string CountryOfOrigin { get; set; }
        public string ICao24 { get; set; }

        override public string ToString () {
            return $"Dist: {Distance}\nCS: {CallSign}\nLat,Long: {Latitude}, {Longitude}\nAlt: {Altitude}\nCtry: {CountryOfOrigin}\nICao24: {ICao24}";
        }

    }
}