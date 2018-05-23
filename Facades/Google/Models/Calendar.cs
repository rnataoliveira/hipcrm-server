using Newtonsoft.Json;

namespace server.Facades.Google.Models
{
  public class Calendar : Resource
  {
    public string Summary { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public string TimeZone { get; set; }
  }
}