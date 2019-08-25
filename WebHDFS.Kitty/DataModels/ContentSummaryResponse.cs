using Newtonsoft.Json;

namespace WebHDFS.Kitty.DataModels
{
    public sealed class ContentSummaryResponse
    {
        public ContentSummaryResponse(ContentSummary summary)
        {
            Summary = summary;
        }

        [JsonProperty("ContentSummary")]
        public ContentSummary Summary { get; }
    }
}
