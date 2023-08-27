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
        private int currentIndex;

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
                    if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
                    {
                        Search();
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                if (_phoneController.currentPage == 0)
                {
                    _phoneController.ClosePhone();
                }
                else if (_phoneController.currentPage == 1)
                {
                    _searchResultPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
                else if (_phoneController.currentPage == 2)
                {
                    _loadingPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
                else if (_phoneController.currentPage == 3)
                {
                    _playSongPageBackButton.GetComponent<Button>().onClick.Invoke();
                }
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (_searchField.gameObject.activeSelf)
                {
                    _searchField.ActivateInputField();
                }
                if (_phoneController.currentPage == 1)
                {
                    _phoneController.searchResultGroupParent.transform.GetChild(1).gameObject.GetComponent<songCardScript>().onClickPlayButton();
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (_searchField.gameObject.activeSelf)
                {
                    _searchField.ActivateInputField();
                }
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (_phoneController.currentPage == 1)
                {
                    if (_phoneController.searchResultGroupParent.transform.childCount > 0)
                    {
                        GameObject songCard;
                        //ClearAllSelections
                        for (int i = 0; i < _phoneController.searchResultGroupParent.transform.childCount; i++)
                        {
                            
                            songCard = _phoneController.searchResultGroupParent.transform.GetChild(i).gameObject;
                            songCard.GetComponent<songCardScript>().cardselection.SetActive(false);
                        }
                        //HandleChildCount
                        currentIndex ++;
                        if (currentIndex > _phoneController.searchResultGroupParent.transform.childCount - 1)
                        {
                            currentIndex = 0;
                        }
                        //MakeSelection
                        songCard = _phoneController.searchResultGroupParent.transform.GetChild(currentIndex).gameObject;
                        songCard.GetComponent<songCardScript>().cardselection.SetActive(true);
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
