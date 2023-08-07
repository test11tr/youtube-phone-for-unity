using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace test11
{
    public class songPlayCardScript : MonoBehaviour
    {
        public Image songIcon;
        public TMP_Text songName;
        public Button playButton;
        public string songID; 
        public string imageURL;

        void Start()
        {
            if(imageURL != null){
                StartCoroutine(DownloadImage(imageURL));
                StopCoroutine(RefreshURLCoroutine());
            }else{
                RefreshURL();
            }
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
