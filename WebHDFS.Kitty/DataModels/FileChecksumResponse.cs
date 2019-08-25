using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public sealed class FileChecksumResponse
    {
        public FileChecksumResponse(FileChecksum checksum)
        {
            Checksum = checksum;
        }

        [JsonProperty("FileChecksum")]
        public FileChecksum Checksum { get; }
    }
}
