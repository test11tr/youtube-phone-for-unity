using UnityEngine;
using UnityEngine.Video;

namespace YoutubePlayerEditor
{
    public class SimpleYoutubeVideo : MonoBehaviour
    {
        public string videoUrl;

        async void Start()
        {
            Debug.Log("Loading video...");
            var videoPlayer = GetComponent<VideoPlayer>();
            await videoPlayer.PlayYoutubeVideoAsync(videoUrl);
        }
    }
}
