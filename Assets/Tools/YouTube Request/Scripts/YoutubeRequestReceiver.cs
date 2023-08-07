using System;
using Sirenix.OdinInspector;
using UnityEngine;
using test11;

namespace YoutubeRequestSystem
{
    public class YoutubeRequestReceiver : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private YoutubeSearchRequestChannel youtubeSearchRequestChannel;

        //
        public GameObject songCardPrefab;
        public GameObject songLayoutGroup;

        #endregion

        #region UNITY METHODS

        private void Start() {
        }

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

                GameObject songCard = Instantiate(songCardPrefab, songLayoutGroup.transform.position, Quaternion.identity, songLayoutGroup.transform);
                songCard.GetComponent<songCardScript>().songName.text = item.snippet.title;
                songCard.GetComponent<songCardScript>().songID = item.id.videoId;
                songCard.GetComponent<songCardScript>().imageURL = item.snippet.thumbnails.@default.url;
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