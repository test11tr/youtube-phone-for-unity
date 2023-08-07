using Newtonsoft.Json;

namespace YoutubePlayerEditor
{
    public class YoutubePlaylistFlatEntry
    {
        public YoutubePlaylistFlatEntry()
        {
        }

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("url")]
        public string Url;
    }
}
