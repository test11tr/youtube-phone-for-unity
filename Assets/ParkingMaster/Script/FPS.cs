using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace test11
{
    public class FPS : MonoBehaviour
    {
        private float count;
        public bool isFPSCounterEnabled;
        public int _targetFPS;
        
        private IEnumerator Start()
        {
            GUI.depth = 2;
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        private void OnGUI()
        {
            if(isFPSCounterEnabled){
                Rect location = new Rect(5, 5, 350, 40);
                string text = $"FPS: {Mathf.Round(count)}";
                Texture black = Texture2D.linearGrayTexture;
                GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
                GUI.color = Color.black;
                GUI.skin.label.fontSize = 35;
                GUI.Label(location, text);
            }
        }

        private void Awake() {
            Application.targetFrameRate = _targetFPS;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(gameObject);
        }
    }
}
