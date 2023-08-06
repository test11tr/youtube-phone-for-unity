using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YoutubeRequestSystem
{
    public class YoutubeRequestReceiver : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private YoutubeSearchRequestChannel youtubeSearchRequestChannel;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            youtubeSearchRequestChannel.OnRequestStarted += RequestStarted;
            youtubeSearchRequestChannel.OnRequestSuccessful += RequestSuccessful;
            youtubeSearchRequestChannel.OnRequestFailed += RequestFailed;
        }

        private void OnDisable()
        {
            youtubeSearchRequestChannel.OnRequestStarted -= RequestStarted;
            youtubeSearchRequestChannel.OnRequestSuccessful -= RequestSuccessful;
            youtubeSearchRequestChannel.OnRequestFailed -= RequestFailed;
        }

        #endregion

        #region PRIVATE METHODS

        private void RequestStarted()
        {
            Debug.Log("Request Started ");
        }

        private void RequestSuccessful(YoutubeSearchResult result)
        {
            Debug.Log("Request Successful");
            foreach (var item in result.items)
            {
                Debug.Log("Video Title: " + item.snippet.title);
                Debug.Log("Video ID: " + item.id.videoId);
                Debug.Log("Published At: " + item.snippet.publishedAt);
                Debug.Log("Description: " + item.snippet.description);
                Debug.Log("Thumbnail URL: " + item.snippet.thumbnails.@default.url);
            }
        }

        private void RequestFailed(Exception exception)
        {
            Debug.Log("Request failed. Error: " + exception.Message);
        }

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void GetSearch(string searchKey)
        {
            youtubeSearchRequestChannel.GetSearch(searchKey);
        }

        #endregion
    }
}