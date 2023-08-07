using Newtonsoft.Json;

namespace YoutubePlayerEditor
{
    public class YoutubePlaylistFlatMetadata
    {
        public YoutubePlaylistFlatMetadata()
        {
        }

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("entries")]
        public YoutubePlaylistFlatEntry[] Entries;
    }
}
