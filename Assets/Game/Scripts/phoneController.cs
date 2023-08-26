using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using YoutubeRequestSystem;
using UnityEngine.UI;
using TMPro;

namespace test11
{
    public class phoneController : MonoBehaviour
    {
        [SerializeField] private Animator _phoneAnimator;
        [SerializeField] private YoutubeRequestReceiver _ytRequestReciever;
        [SerializeField] private YoutubeSearchRequestChannel _ytSearchRequestChannel;
        [SerializeField] private TMPro.TMP_InputField _searchField;
        public GameObject songPlayerParent;
        public GameObject songPlayerPage;
        public GameObject songLoadingScreen;

        void Start()
        {
            if(_phoneAnimator == null){
                _phoneAnimator = GetComponent<Animator>();
            }
            if(_ytRequestReciever == null){
                _ytRequestReciever = GetComponentInChildren<YoutubeRequestReceiver>();
            }
            if(_searchField == null){
                _searchField = GetComponentInChildren<TMPro.TMP_InputField>();
            }
        } 

        // Update is called once per frame
        void Update()
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if(_phoneAnimator.GetBool("isClosed") == true){
                    _phoneAnimator.SetBool("isClosed", false);
                }else{
                    _phoneAnimator.SetBool("isClosed", true);
                }
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

        public void searchButtonPressed(){
            _ytSearchRequestChannel.GetSearch(_searchField.text);
        }

        public void clearInstantiatedSongCards(GameObject target){
            if(target.transform.childCount > 0){
                for (int i = 0; i < target.transform.childCount; i++)
                {
                    Destroy(target.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
