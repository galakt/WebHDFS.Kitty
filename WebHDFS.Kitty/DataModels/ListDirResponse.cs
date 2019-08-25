using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public class ListStatusResponse
    {
        [JsonProperty(propertyName: "FileStatuses")]
        public FileStatusCollection FileStatusCollection { get; set; }
    }
}
