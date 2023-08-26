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
        [SerializeField] private GameObject _searchResultPageBackButton;
        [SerializeField] private GameObject _playSongPageBackButton;
        [SerializeField] private GameObject _loadingPageBackButton;
        [SerializeField] private GameObject _searchButton;
        [SerializeField] private TMP_InputField _searchField;
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

            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                if (_searchResultPageBackButton.activeSelf)
                {
                    _searchResultPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
                else if (_playSongPageBackButton.activeSelf)
                {
                   _playSongPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
                else if (_loadingPageBackButton.activeSelf)
                {
                    _loadingPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    return;
                }
            }
            
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (_searchField.gameObject.activeSelf)
                {
                    _searchField.ActivateInputField();
                }
            }

        }

        public void Search(){
            _searchButton.GetComponent<Button>().onClick.Invoke(); 
            allow = true;      
        }
    }
}
