using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace test11.Managers
{
    public class UIManager : MonoBehaviour
    {
        public bool gameIsPaused = false;

        public void SetTrue (GameObject target)
        {
            target.SetActive (true);
        }

        public void SetFalse (GameObject target)
        {
            target.SetActive (false);
        }

        public void ReloadScene(){
            string currentScene = SceneManager.GetActiveScene ().name; 
            SceneManager.LoadScene(currentScene);
            Time.timeScale = 1f;
        }

        public void LoadMainMenuScene(){
            SceneManager.LoadScene("00-MainMenu");
            Time.timeScale = 1f;
        }
        
        public void PauseGame ()
        {
            print("here");
            if(gameIsPaused)
            {
                gameIsPaused = false;
                Time.timeScale = 1f;
            }
            else 
            {
                gameIsPaused = true;
                Time.timeScale = 0f;
            }
        }
    }
}
