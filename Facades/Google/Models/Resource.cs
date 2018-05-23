using Newtonsoft.Json;

namespace server.Facades.Google.Models
{
    public abstract class Resource 
    {
        public string Kind { get; set; }

        public string Etag { get; set; }

        public string Id { get; set; }
    }
}