using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class SettingsButtonHandler : MonoBehaviour
    {
        public GameObject arrowOnButton;
        public GameObject wheelOnButton;
        
        void Start()
        {
            if(PlayerPrefs.GetInt("ControllerType") == 0){
                wheelOnButton.SetActive(true);
                arrowOnButton.SetActive(false);
            }else{
                wheelOnButton.SetActive(false);
                arrowOnButton.SetActive(true);
            }
        }
    }
}
