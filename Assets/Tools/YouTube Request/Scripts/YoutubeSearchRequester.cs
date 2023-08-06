using System.Collections.Generic;
using Proyecto26;
using RSG;
using UnityEngine;

namespace YoutubeRequestSystem
{
    [CreateAssetMenu(fileName = "Youtube Search Requester",
        menuName = "Request Related/Youtube Search Requester")]
    public class YoutubeSearchRequester : ScriptableObject
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private string apiKey;

        #endregion


        #region PUBLIC METHODS

        public IPromise<ResponseHelper> GetSearchResult(string searchKey, string maxResults)
        {
            var promise = new Promise<ResponseHelper>();

            RestClient.Request(new RequestHelper
            {
                Uri = "https://www.googleapis.com/youtube/v3/search",
                Method = "GET",
                Timeout = 10,
                Params = new Dictionary<string, string>
                {
                    { "key", apiKey },
                    { "q", searchKey },
                    { "part", "snippet" },
                    { "maxResults", maxResults },
                    { "type", "video" }
                },
            }).Then(response => { promise.Resolve(response); }).Catch(err =>
            {
                var error = err as RequestException;
                promise.Reject(err);
            });

            return promise;
        }

        #endregion
    }
}