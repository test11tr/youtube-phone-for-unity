using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace test11
{
    public class LevelSelect : MonoBehaviour
    {
        public GameObject[] LevelSetPages;
        int currentIndex;

        public GameObject[] Level;
        public GameObject[] Locks;
        public GameObject[] normalBg;
        public GameObject[] completeBg;
        int temp;

        public GameObject[] star1Level, star2Level, star3Level;
        public GameObject selectDialog;
        public GameObject loading;

        public string levelName = "00-MainMenu";
        public int levelCount;

        void resetUI(){
            for(int a = 0; a < Level.Length; a++){
                normalBg[a].SetActive(false);
                completeBg[a].SetActive(false);
                Locks[a].SetActive(true);
            }
        }

        void UpdateStart(){
            LevelSetPages[0].SetActive(true);

            for(int a = 0; a < Level.Length; a++){
                normalBg[a].SetActive(false);
                completeBg[a].SetActive(false);
            }

            if(PlayerPrefs.GetInt(levelName + "LevelNum") > Level.Length){
                temp = Level.Length;
                for (int b = 0; b <= temp; b++){
                    if (temp > b){
                        Locks[b].SetActive(false);
                        normalBg[b].SetActive(true);
                    }
                }
            }else{
                temp = PlayerPrefs.GetInt(levelName + "LevelNum");
                for (int b = 0; b <= temp; b++){
                    if (temp > b){
                        Locks[b].SetActive(false);
                        normalBg[b].SetActive(true);
                    }
                }
            }
            
  
            for (int c = 0; c < Level.Length; c++){
                if (PlayerPrefs.GetInt (levelName+"Star" + c.ToString ()) == 3) 
                {
                    completeBg[c].SetActive(true);
                    star1Level [c].SetActive (true);
                    star2Level [c].SetActive (true);
                    star3Level [c].SetActive (true);
                }
                if (PlayerPrefs.GetInt (levelName+"Star" + c.ToString ()) == 2) 
                {
                    completeBg[c].SetActive(true);
                    star1Level [c].SetActive (true);
                    star2Level [c].SetActive (true);
                    star3Level [c].SetActive (false);
                }
                if (PlayerPrefs.GetInt ("Star" + c.ToString ()) == 1) 
                {
                    completeBg[c].SetActive(true);
                    star1Level [c].SetActive (true);
                    star2Level [c].SetActive (false);
                    star3Level [c].SetActive (false);
                }
                if (PlayerPrefs.GetInt (levelName+"Star" + c.ToString ()) == 0) 
                {
                    star1Level [c].SetActive (false);
                    star2Level [c].SetActive (false);
                    star3Level [c].SetActive (false);
                }
            }
        }

        public void UpdateLevelName(string _name){
            levelName = _name;
            resetUI();
            UpdateStart();
        }

        public void ChangeLevelPage(int _change){
            int previousIndex = currentIndex;
            currentIndex += _change;

            if(currentIndex < 0){
                currentIndex = LevelSetPages.Length - 1;
            }else if(currentIndex > LevelSetPages.Length -1){
                currentIndex = 0;
            }

            LevelSetPages[previousIndex].SetActive(false);
            LevelSetPages[currentIndex].SetActive(true);
        }

        public void SelectLevel (int id){
            if (id < temp) 
            {
                tempID = id;
                selectDialog.SetActive (true);
            }
        }

        int tempID;

        public void SelectLevelNow(){
            if(loading)
                loading.SetActive (true);

            PlayerPrefs.SetInt (levelName+"LevelID", tempID);
            SceneManager.LoadScene (levelName);
        }
    }
}
