using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace test11
{
    public class WorldDisplay : MonoBehaviour
    {
        [Header("World Info")]
        [SerializeField] private string worldIndex;
        [SerializeField] private int worldNumberIndex;
        [SerializeField] private TMP_Text worldName;
        [SerializeField] private TMP_Text worldPriceAsDiamond;
        [SerializeField] private int worldPriceAsDiamondInt;
        [SerializeField] private TMP_Text worldDescription;

        [Header("World References")]
        [SerializeField] private Image worldImage;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private LevelSelect _levelSelect;

        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject unlockButton;
        [SerializeField] private GameObject comingSoonButton;
        [SerializeField] private GameObject buyWorldDialog;
        [SerializeField] private GameObject worldBought;
        [SerializeField] private GameObject notEnoughMoney;


        private string worldSceneName;

        public void DisplayWorld(World _world){
            worldIndex = _world.worldIndex;
            worldNumberIndex = _world.worldNumberIndex;
            worldName.text = _world.worldName;
            worldPriceAsDiamond.text = _world.worldPriceAsDiamond.ToString();
            worldPriceAsDiamondInt = _world.worldPriceAsDiamond;
            worldSceneName = _world.worldSceneName;
            worldDescription.text = _world.worldDescription;
            worldImage.sprite = _world.worldImage;

            if(PlayerPrefs.GetInt(worldIndex) == 1){
                playButton.SetActive(true);
                unlockButton.SetActive(false);
                lockIcon.SetActive(false);
                worldImage.color = Color.white;
                comingSoonButton.SetActive(false);
            }else if(PlayerPrefs.GetInt(worldIndex) == 0){
                if(worldPriceAsDiamondInt == 0){
                    comingSoonButton.SetActive(true);
                    unlockButton.SetActive(false);
                    lockIcon.SetActive(true);
                    worldImage.color = Color.gray;
                }else{
                    comingSoonButton.SetActive(false);
                    playButton.SetActive(false);
                    unlockButton.SetActive(true);
                    worldPriceAsDiamond.text = "x " + _world.worldPriceAsDiamond.ToString();
                    lockIcon.SetActive(true);
                    worldImage.color = Color.gray;
                }
                
            }
        }

        public void BuyWorld(){
            if(PlayerPrefs.GetInt("Diamonds") >= worldPriceAsDiamondInt){
                PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - worldPriceAsDiamondInt);
                PlayerPrefs.SetInt(worldIndex, 1); 
                PlayerPrefs.SetInt(worldSceneName + "LevelNum", 1);
                playButton.SetActive(true);
                unlockButton.SetActive(false);
                worldBought.SetActive(true);
                lockIcon.SetActive(false);
                worldImage.color = Color.white;
            }else{
                notEnoughMoney.SetActive(true);
            }
        }

        public void SetLevelSet(){
            PlayerPrefs.SetInt("CurrentWorld", worldNumberIndex);
            _levelSelect.UpdateLevelName(worldSceneName);
        }
    }
}
