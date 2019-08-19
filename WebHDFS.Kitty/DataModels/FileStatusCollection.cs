using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public class FileStatusCollection
    {
        [JsonProperty(propertyName: "FileStatus")]
        public FileStatus[] FileStatuses { get; set; }
    }
}
