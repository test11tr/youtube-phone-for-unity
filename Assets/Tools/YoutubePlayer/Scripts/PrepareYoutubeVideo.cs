using UnityEngine;

namespace YoutubePlayerEditor
{
    public class PrepareYoutubeVideo : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;

        public async void Prepare()
        {
            Debug.Log("Loading video...");
            await youtubePlayer.PrepareVideoAsync();
            Debug.Log("Video ready");
        }
    }
}
