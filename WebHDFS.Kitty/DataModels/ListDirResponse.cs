using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public class ListDirResponse
    {
        [JsonProperty(propertyName: "FileStatuses")]
        public FileStatusCollection FileStatusCollection { get; set; }
    }
}
