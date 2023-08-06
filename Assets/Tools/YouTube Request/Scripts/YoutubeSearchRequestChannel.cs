using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YoutubeRequestSystem
{
    [CreateAssetMenu(fileName = "Youtube Search Request Channel",
        menuName = "Request Related/Youtube Search Request Channel")]
    public class YoutubeSearchRequestChannel : ScriptableObject
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private YoutubeSearchRequester youtubeSearchRequester;
        [SerializeField] private string maxResults = "10";

        #endregion

        #region PUBLIC ACTIONS

        public Action OnRequestStarted;
        public Action<YoutubeSearchResult> OnRequestSuccessful;
        public Action<Exception> OnRequestFailed;
        #endregion
        


        #region PUBLIC METHODS

        [Button]
        public void GetSearch(string searchKey)
        {
            OnRequestStarted?.Invoke();
            youtubeSearchRequester.GetSearchResult(searchKey, maxResults).Then(response =>
            {
                YoutubeSearchResult result = JsonUtility.FromJson<YoutubeSearchResult>(response.Text);
                OnRequestSuccessful?.Invoke(result);
            }).Catch(err =>
            {
                Debug.Log(err.Message);
                OnRequestFailed?.Invoke(err);
            });
        }

        #endregion
    }
}