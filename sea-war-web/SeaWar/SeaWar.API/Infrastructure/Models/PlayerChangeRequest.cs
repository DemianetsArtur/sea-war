using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeaWar.API.Infrastructure.Models
{
    public class PlayerChangeRequest
    {
        public string Name { get; set; }

        public IList<Coordinate> Coordinates { get; set; }

        public PlayerChangeRequest()
        {
            this.Coordinates = new List<Coordinate>();
        }

        public static PlayerChangeRequest FromJson(string json)
        {
            return JsonConvert.DeserializeObject<PlayerChangeRequest>(json);
        }
    }
}
