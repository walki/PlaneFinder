using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PlaneFinder.Service
{
    public class OpenSkyResponse
    {

        /*
         0 - string icao24
         1 - string callsign
         2 - string origin_country
         3 - int time_position
         4 - int last_contact
         5 - float longitude
         6 - float latitude
         7 - float geo_altitude
         8 - bool on_ground 
         9 - float velocity
        10 - float heading 
        11 - float vertical_rate 
        12 - int[] sensors
        13 - float baro_altitude
        14 - string squawk
        15 - bool spi
        16 - int position_source
         */

        public int Time
        { 
            get
            {
                return (int)wholeThing["time"];
            }
        }

        private JObject wholeThing;

        public OpenSkyResponse(string json)
        {
            wholeThing = JObject.Parse(json);
        }

        private int stateCount = -1;

        public int StateCount
        {
            get
            {
                if (stateCount == -1)
                {
                    var arr = JsonConvert.DeserializeObject<JArray>(wholeThing["states"].ToString());
                    stateCount = arr.Count;
                }
                return stateCount;
            }
        }

        public string GetState(int index, int prop)
        {
            JArray state = JArray.Parse(wholeThing["states"][index].ToString());
            return state[prop].ToString().Trim();
        }        

        public int? GetStateInt(int index, int prop)
        {
            string state = GetState(index, prop);
            return String.IsNullOrWhiteSpace(state) ? null : (int?)Convert.ToInt32(state);
       }

        public double? GetStateDouble(int index, int prop)
        {
            string state = GetState(index, prop);
            return String.IsNullOrWhiteSpace(state) ? null : (double?)Convert.ToDouble(state);
        }

        public bool GetStateBool(int index, int prop)
        {
            return Convert.ToBoolean(GetState(index, prop));
        }


        public string GetIcao24(int index) => GetState(index, 0);
        public string GetCallSign(int index) => GetState(index, 1);
        public string GetOriginCountry(int index) => GetState(index, 2);
        public int? GetTimePosition(int index) => GetStateInt(index, 3);
        public int? GetLastContact(int index) => GetStateInt(index, 4);
        public double? GetLongitude(int index) => GetStateDouble(index, 5);
        public double? GetLatitude(int index) => GetStateDouble(index, 6);
        public double? GetGeoAltitude(int index) => GetStateDouble(index, 7);
        public bool GetOnGround(int index) => GetStateBool(index, 8);
        public double? GetVelocity(int index) => GetStateDouble(index, 9);
        public double? GetHeading(int index) => GetStateDouble(index, 10);
        public double? GetVerticalRate(int index) => GetStateDouble(index, 11);

        //public int[] GetSensors(int index) => GetStateInt(index, 12);
        public double? GetBaroAltitude(int index) => GetStateDouble(index, 13);
        public string GetSquawk(int index) => GetState(index, 14);
        public bool GetSpi(int index) => GetStateBool(index, 15);
        public int? GetPositionSource(int index) => GetStateInt(index, 16);

    }
}