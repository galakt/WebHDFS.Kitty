using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public sealed class FileChecksum
    {
        public FileChecksum(string algorithm, string hex, int length)
        {
            Algorithm = algorithm;
            Hex = hex;
            Length = length;
        }

        public string Algorithm { get; }

        [JsonProperty("bytes")]
        public string Hex { get; }

        public int Length { get; }
    }
}
