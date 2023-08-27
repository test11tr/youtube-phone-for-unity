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
    public class songCardScript : MonoBehaviour
    {
        public Image songIcon;
        public TMP_Text songName;
        public Button playButton;
        public string songID; 
        public string imageURL;
        public GameObject songPlayCardPrefab;
        [SerializeField] private phoneController _phoneController;
        public GameObject cardselection;

        void Start()
        {
            if (_phoneController == null)
            {
                _phoneController = GameObject.FindGameObjectWithTag("phoneController").GetComponent<phoneController>();
            }

            if(imageURL != null){
                StartCoroutine(DownloadImage(imageURL));
                StopCoroutine(RefreshURLCoroutine());
            }else{
                RefreshURL();
            }
        }

        //UI
        public void SetTrue (GameObject target)
        {
            target.SetActive (true);
        }

        public void SetFalse (GameObject target)
        {
            target.SetActive (false);
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

        public void onClickPlayButton(){
            transform.parent.parent.parent.SetActive(false);
            GameObject songPlayCard = Instantiate(songPlayCardPrefab, _phoneController.songPlayerParent.transform.position, Quaternion.identity, _phoneController.songPlayerParent.transform);
            songPlayCard.GetComponent<songPlayerScript>().imageURL = imageURL;
            songPlayCard.GetComponent<songPlayerScript>().songName.text = songName.text;
            songPlayCard.GetComponent<songPlayerScript>().songID = songID;
            _phoneController.songPlayerPage.SetActive(true);
            _phoneController.setCurrentPage(2);
        }
    }
}
