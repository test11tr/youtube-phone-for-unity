using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace test11
{
    public class inputHandler : MonoBehaviour
    {

        [SerializeField] private EventSystem system;
        [SerializeField] private phoneController _phoneController;
        [SerializeField] private GameObject _mainPage;
        [SerializeField] private GameObject _searchButton;
        bool allow = true;

       void OnEnable()
        {
            system = EventSystem.current;
        }

        void Start() {
            if (_phoneController == null)
            {
                _phoneController = GameObject.FindGameObjectWithTag("phoneController").GetComponent<phoneController>();
            }
        }
        
        void Update()
        {
            if (system == null)
            {
                system = EventSystem.current;
                if (system == null) return;
            }

            GameObject currentObject = system.currentSelectedGameObject;
            if (currentObject != null )
            {
                TMP_InputField tmpInput = currentObject.GetComponent<TMP_InputField>();
                if (tmpInput != null)
                {
                    if (Input.GetKeyUp(KeyCode.Return))
                    {
                        Search();
                    }
                }
            }
        }

        public void Search(){
            _searchButton.GetComponent<Button>().onClick.Invoke(); 
            allow = true;      
        }
    }
}
