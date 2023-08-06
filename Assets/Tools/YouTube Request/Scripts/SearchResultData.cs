using System.Collections.Generic;

namespace YoutubeRequestSystem
{
    [System.Serializable]
    public class Thumbnails
    {
        public Thumbnail @default;
        public Thumbnail medium;
        public Thumbnail high;
    }

    [System.Serializable]
    public class Thumbnail
    {
        public string url;
        public int width;
        public int height;
    }

    [System.Serializable]
    public class VideoId
    {
        public string kind;
        public string videoId;
    }

    [System.Serializable]
    public class Snippet
    {
        public string publishedAt;
        public string channelId;
        public string title;
        public string description;
        public Thumbnails thumbnails;
        public string channelTitle;
        public string liveBroadcastContent;
        public string publishTime;
    }

    [System.Serializable]
    public class YoutubeSearchResult
    {
        public string kind;
        public string etag;
        public string nextPageToken;
        public string regionCode;
        public PageInfo pageInfo;
        public List<YoutubeItem> items;
    }

    [System.Serializable]
    public class PageInfo
    {
        public int totalResults;
        public int resultsPerPage;
    }

    [System.Serializable]
    public class YoutubeItem
    {
        public string kind;
        public string etag;
        public VideoId id;
        public Snippet snippet;
    }
}