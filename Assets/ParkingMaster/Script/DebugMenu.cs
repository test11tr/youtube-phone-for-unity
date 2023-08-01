using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace test11
{
    public class DebugMenu : MonoBehaviour
    {
        public DebugMenuOpenRitualChecker ritualChecker;
        public GameObject debugMenu;
        public TMPro.TMP_InputField increaseLevelInput;
        public Button goToLevelButton;
        bool IsMenuOpened;
        public string levelName;
        public Button mallButton;
        public LevelSelect _levelSelect;
        int levelNumber;

        private void Start() {
            IsMenuOpened = false;
            mallButton.onClick.AddListener(delegate {ChangeLevelName("01-Mall");});
        }

        private void Update() {
            if (ritualChecker.Check()) {
                Show();
            }

            if (Input.GetKeyDown (KeyCode.H)) {
			    PlayerPrefs.DeleteAll ();
			Debug.Log ("PlayerPrefs.DeleteAll ();");
            }

            if (Input.GetKeyDown (KeyCode.M)) {
			    IsMenuOpened = true;
                debugMenu.SetActive(true);
            }

            //Cheat
            if (Input.GetKeyDown (KeyCode.E))
                PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 1000);

            //Cheat
            if (Input.GetKeyDown (KeyCode.D))
                PlayerPrefs.SetInt ("Diamonds", PlayerPrefs.GetInt ("Diamonds") + 1000);
        }

        public void Show() {
            IsMenuOpened = true;
            debugMenu.SetActive(true);
        }

        public void Hide() {
            IsMenuOpened = false;
            debugMenu.SetActive(false);
        }

        void ChangeLevelName(string _levelName)
        {
            levelName = _levelName;
            print(levelName);
        }

        [System.Serializable]
        public struct DebugMenuOpenRitualChecker {
            public float screenCornerPercent;
            public float patternInputTime;
            int _patternIndex;
            float _patternInputStart;
            public bool Check() {
                if (Input.GetMouseButtonDown(0)) {

                    if (_patternIndex < 4 && Input.mousePosition.x < Screen.width * screenCornerPercent &&
                        Input.mousePosition.y > Screen.height - (Screen.width * screenCornerPercent)) {
                        _patternIndex++; 
                    }
                    else if (_patternIndex >= 4 && Input.mousePosition.x > Screen.width * (1 - screenCornerPercent) &&
                             Input.mousePosition.y > Screen.height - (Screen.width * screenCornerPercent)) {
                        if (Time.unscaledTime - _patternInputStart < patternInputTime) {
                            return true;
                        }
                        _patternIndex = 0;
                        _patternInputStart = Time.unscaledTime;
                    }
                    else {
                        _patternIndex = 0;
                        _patternInputStart = Time.unscaledTime;
                    }
                }
                return false;
            }
        }

        public void AddMoney(){
            PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 1000);
        }
        
        public void AddDiamond(){
            PlayerPrefs.SetInt ("Diamonds", PlayerPrefs.GetInt ("Diamonds") + 1000);
        }

        public void GoToLevel() {
            if (!Int32.TryParse(increaseLevelInput.text, out int levelNumber))
                return;
            levelNumber--;
            print(levelNumber);
            PlayerPrefs.SetInt(levelName + "LevelID", levelNumber);
            SceneManager.LoadScene (levelName);
        }

        public void DeleteAllData(){
            PlayerPrefs.DeleteAll();
        }

        public void QuitGame(){
            Application.Quit();
        }
    }
}
