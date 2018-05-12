namespace PlaneFinder.Service
{
    public class OpenSkyState
    {
        string icao24 { get; set; }
        string callsign { get; set; }
        string origin_country {get; set;}
        int time_position { get; set;}
        int last_contact { get; set;}
        float longitude { get; set;}
        float latitude { get; set;}
        float geo_altitude { get; set;}
        bool on_ground  { get; set;}
        float velocity { get; set;}
        float heading  { get; set;}
        float vertical_rate  { get; set;}
        int[] sensors { get; set;}
        float baro_altitude { get; set;}
        string squawk { get; set;}
        bool spi { get; set;}
        int position_source { get; set;}

    }
}