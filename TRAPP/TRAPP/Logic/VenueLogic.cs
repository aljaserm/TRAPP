using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRAPP.Model;

namespace TRAPP.Logic
{
   public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenues(double latitude,double longitude)
        {
            List<Venue> v = new List<Venue>();
            var url = VenueRoot.GenerateURL(latitude, longitude);
            using (HttpClient client = new HttpClient())
            {
                var response=await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                v=venueRoot.response.venues as List<Venue>;
            }
            return v;
        }
    }
}
