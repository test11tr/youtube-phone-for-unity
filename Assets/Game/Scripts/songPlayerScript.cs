using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using YoutubePlayerEditor;
using Unity.VisualScripting;

namespace test11
{
    public class songPlayerScript : MonoBehaviour
    {
        public Image songIcon;
        public TMP_Text songName;
        public string songID; 
        public string imageURL;
        [SerializeField] private phoneController _phoneController;
        [SerializeField] private YoutubePlayer _ytPlayer; 

        void Start() {
            if (_phoneController == null)
            {
                _phoneController = GameObject.FindGameObjectWithTag("phoneController").GetComponent<phoneController>();
            }

            if(imageURL != null){
                StartCoroutine(DownloadImage(imageURL));
                StopCoroutine(RefreshURLCoroutine());
                _ytPlayer.youtubeUrl = songID;
                PrepareVideo();
            }else{
                RefreshURL();
            }
        }

        public async void PrepareVideo()
        {
            _phoneController.songLoadingScreen.SetActive(true);
            Debug.Log("Loading video...");
            await _ytPlayer.PrepareVideoAsync();
            Debug.Log("Video ready");
            _phoneController.songLoadingScreen.SetActive(false);
            _phoneController.setCurrentPage(3);
        }

        [Obsolete]
        IEnumerator DownloadImage(string MediaUrl)
        {   
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError) 
                Debug.Log(request.error);
            else{
                Texture2D tex = ((DownloadHandlerTexture) request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                songIcon.overrideSprite = sprite;
                StopAllCoroutines();
            }   
        } 

        public void RefreshURL(){
            StartCoroutine(RefreshURLCoroutine());
        }

        IEnumerator RefreshURLCoroutine()
        {   
            yield return new WaitForSeconds(1);
        } 
    }
}
