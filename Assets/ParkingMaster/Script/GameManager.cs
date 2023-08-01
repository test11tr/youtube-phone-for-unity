using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private UIManager _uiManager;
        private LevelManager _levelManager;
        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
        }
    }
}
