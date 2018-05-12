using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PlaneFinder.Service
{
    static public class PlaneService
    {
        static public string GetData()
        {
            WebRequest req = WebRequest.Create("https://opensky-network.org/api/states/all");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            return new StreamReader(resp.GetResponseStream()).ReadToEnd();
        }

        static public OpenSkyResponse Deserialize(string json)
        {
            return new OpenSkyResponse(json);
        }
    }
}
