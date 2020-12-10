using Newtonsoft.Json;

namespace SeaWar.API.Infrastructure.Models
{
    public class SocketMessage<T>
    {
        public string MessageType { get; set; }

        public T Payload { get; set; }

        public string ToJson() 
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
