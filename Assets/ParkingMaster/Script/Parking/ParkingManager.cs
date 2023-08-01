using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using test11.Managers;
using UniqueVehicleController;
using TMPro;
using UnityEngine.SceneManagement;

namespace test11.Managers
{
    public class ParkingManager : MonoBehaviour
    {
        [Header("Important References - Don't Assign")]
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private UVCUniqueVehicleController _carController;
        [SerializeField] private ParkingManager _parkingManager;
        [SerializeField] private GameObject _hud;
        [SerializeField] private GameObject parkingNotify;

        [HideInInspector]
        public bool fl,fr,rl,rr,front,rear;
        [Header("Menu References")]
        public GameObject FinishMenu;
        public GameObject FailedMenu;
        public GameObject TimeFailedMenu;

        [Header("Score Settings")]
        public int CollisionScore0 = 0;
        public int CollisionScore1 = 0;
        public int CollisionScore2 = 0;
        public int CollisionScore3 = 0;
        public int CollisionScoreSoftfail = 0;
        public int collisionLimit = 3;
        public GameObject youSuckText;
        public GameObject visualstar1, visualstar2, visualstar3;
        private bool isFinish, Finished, Score;

        [Header("UI Text References")]
        public TMP_Text CollisionCountText;
        public TMP_Text FinishScoreText;
        [HideInInspector] public int CollisionCount;
        public TMP_Text levelHud;
        
        [Header("Visual Stuff")]
        public MeshRenderer ParkingArea;
        public MeshRenderer ParkingAreaEmission;
        public GameObject star0, star1, star2, star3;
        public ParticleSystem confettiEffect;
        public float finishMenuDelay;

        [Header("Is Time Limited?")]
        public bool timeLimit;
        public GameObject TimeDownMenu;
        
        [Header("Is Bonus Rewarded?")]
        public bool isBonusRewarded;
        public GameObject bonusInfoText;
        public int bonusDiamondAmount;
        public GameObject bonusReward;
        public TMP_Text bonusText;

        [Header("Audio Related")]
        public AudioSource AlarmSound;
        public AudioClip clipSuccess, clipLost;
        private AudioSource _audioSource;

        private string  levelName;

        void Start(){
            levelName = SceneManager.GetActiveScene().name;

            if (_levelManager == null)
            {
                _levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
            }
            if (_carController == null)
            {
                _carController = _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>();
            }
            if (_parkingManager == null)
            {
                _parkingManager = this;
            }
            if (_hud == null)
            {
                _hud = GameObject.FindGameObjectWithTag("HUD");
            }
            
            if(timeLimit)
                TimeDownMenu.SetActive(true);
            else{
                TimeDownMenu.SetActive(false);
            }

            levelHud.text = "LEVEL " + PlayerPrefs.GetInt(levelName + "LevelNum");

            // Audiosource
            //_audioSource = gameObject.AddComponent<AudioSource>();
            //_audioSource.spatialBlend = 0;
            //_audioSource.playOnAwake = false;
            //_audioSource.loop = false;
        }

        void Update(){
            if(!Finished){
                if(fl && fr && rl && rr && front && rear && _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().speedOnKmh < 5){
                        ParkingArea.material.color = Color.green;
                        ParkingAreaEmission.gameObject.SetActive(true);
                        parkingNotify.SetActive(true);
                        if(_levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().isparking == true){
                            parkingNotify.SetActive(false);
                            //checking when timer is reaching to 0
                            CheckTimeToFinished();
                            isFinish = true;
                        }
                }else if(fl || fr || rl || rr || front || rear){
                        ParkingArea.material.color = Color.yellow;
                }
                else{
                    //Not parked correctly
                    //StopCoroutine(CheckTimeToFinished());
                    parkingNotify.SetActive(false);
                    isFinish = false;
                    ParkingArea.material.color = Color.white;
                    ParkingAreaEmission.gameObject.SetActive(false);
                }

                if(timeLimit)
                    TimeDown();
            }
        }

        void CheckTimeToFinished(){
            //yield return new WaitForSeconds(4f);
            if(isFinish){
                if(!Score){
                    Score = true;
                    Finished = true;
                    _hud.SetActive(false);
                    confettiEffect.Play();
                    StartCoroutine(CalculateFinish());
                }
            }
        }

        IEnumerator CalculateFinish(){
            yield return new WaitForSeconds(finishMenuDelay);
            if(CollisionCount == 0){
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CollisionScore0);
                FinishScoreText.text = CollisionScore0.ToString();
                star3.SetActive(true);
                PlayerPrefs.SetInt(levelName+"Star" + PlayerPrefs.GetInt(levelName+"LevelID").ToString(), 3);
                //_audioSource.clip = clipSuccess;
                //_audioSource.Play();

                PlayerPrefs.SetInt("LevelXP", PlayerPrefs.GetInt("LevelXP") + CollisionScore0);
                _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;

                if(isBonusRewarded){
                    PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + bonusDiamondAmount);
                    bonusReward.SetActive(true);
                    bonusText.text = bonusDiamondAmount.ToString();
                }
            }

            if(CollisionCount == 1){
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CollisionScore1);
                FinishScoreText.text = CollisionScore1.ToString();
                star2.SetActive(true);
                PlayerPrefs.SetInt(levelName+"Star" + PlayerPrefs.GetInt(levelName+"LevelID").ToString(), 2);
                //_audioSource.clip = clipSuccess;
                //_audioSource.Play();

                PlayerPrefs.SetInt("LevelXP", PlayerPrefs.GetInt("LevelXP") + CollisionScore1);
                _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;

                if(isBonusRewarded){
                    bonusInfoText.SetActive(true);
                }
            }

            if(CollisionCount == 2){
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CollisionScore2);
                FinishScoreText.text = CollisionScore2.ToString();
                star1.SetActive(true);
                PlayerPrefs.SetInt(levelName+"Star" + PlayerPrefs.GetInt(levelName+"LevelID").ToString(), 1);
                //_audioSource.clip = clipSuccess;
                //_audioSource.Play();

                PlayerPrefs.SetInt("LevelXP", PlayerPrefs.GetInt("LevelXP") + CollisionScore2);
                _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;

                if(isBonusRewarded){
                    bonusInfoText.SetActive(true);
                }
            }

            if(CollisionCount == 3){
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CollisionScore3);
                FinishScoreText.text = CollisionScore3.ToString();
                star0.SetActive(true);
                PlayerPrefs.SetInt(levelName+"Star" + PlayerPrefs.GetInt(levelName+"LevelID").ToString(), 0);
                //_audioSource.clip = clipSuccess;
                //_audioSource.Play();

                PlayerPrefs.SetInt("LevelXP", PlayerPrefs.GetInt("LevelXP") + CollisionScore3);
                _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;

                if(isBonusRewarded){
                    bonusInfoText.SetActive(true);
                }
            }

            if(CollisionCount > 3){
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CollisionScoreSoftfail);
                FinishScoreText.text = CollisionScoreSoftfail.ToString();
                star0.SetActive(true);
                PlayerPrefs.SetInt(levelName+"Star" + PlayerPrefs.GetInt(levelName+"LevelID").ToString(), 0);
                //_audioSource.clip = clipSuccess;
                //_audioSource.Play();

                PlayerPrefs.SetInt("LevelXP", PlayerPrefs.GetInt("LevelXP") + CollisionScoreSoftfail);
                _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;

                youSuckText.SetActive(true);
            }


            PlayerPrefs.SetInt(levelName + "TotalPassed", PlayerPrefs.GetInt(levelName + "TotalPassed") + 1);
            if (PlayerPrefs.GetInt (levelName+"LevelID") + 1 == PlayerPrefs.GetInt (levelName+"LevelNum")) {
                PlayerPrefs.SetInt (levelName+"LevelNum", PlayerPrefs.GetInt (levelName+"LevelNum") + 1);
                print("levelNum: " + PlayerPrefs.GetInt(levelName+"LevelNum"));
                PlayerPrefs.SetInt (levelName+"PassedLevels", PlayerPrefs.GetInt (levelName+"PassedLevels") + 1);
            }
            FinishMenu.SetActive (true);
            _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().engineIsStarted = false;
        }

        public void updateVisualStar(){
            if (CollisionCount == 0){
                visualstar1.SetActive(true);
                visualstar2.SetActive(true);
                visualstar3.SetActive(true);
            }else if (CollisionCount == 1){
                visualstar1.SetActive(true);
                visualstar2.SetActive(true);
                visualstar3.SetActive(false);
            }else if (CollisionCount == 2){
                visualstar1.SetActive(true);
                visualstar2.SetActive(false);
                visualstar3.SetActive(false);
            }else if (CollisionCount >= 3){
                visualstar1.SetActive(false);
                visualstar2.SetActive(false);
                visualstar3.SetActive(false);
            }
        }

        public void TimeFailed(){
            //_audioSource.clip = clipLost;
            //_audioSource.Play();
            TimeFailedMenu.SetActive(true);
            PlayerPrefs.SetInt("TotalFailed", PlayerPrefs.GetInt("TotalFailed") + 1);
             _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().engineIsStarted = false;
             _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<ParkingManager>().enabled = false;
            _hud.SetActive(false);
            _text.text = "00:00";
        }

        public TMP_Text _text;
        public float seconds = 59;
        public float minutes = 0;

        public void TimeDown(){
            if(_levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().engineIsStarted){
                if (seconds <= 0) {
                    seconds = 59;

                    if (minutes >= 1) {
                        minutes --;
                    } else {
                        minutes = 0;
                        seconds = 0;
                        _text.text  = minutes.ToString ("f0") + ":0" + seconds.ToString ("f0");
                    }
                } else 
                {
                    seconds -= Time.deltaTime;
                    string min;
                    string sec;

                    if (minutes < 10)
                        min = "0" + minutes.ToString ();
                    else
                        min = minutes.ToString ();

                    if (seconds < 10)
                        sec = "0" +( Mathf.FloorToInt(seconds)).ToString ();
                    else
                        sec = (Mathf.FloorToInt(seconds)).ToString ();


                    _text.text = min + ":"+ sec;
                }

                if (minutes <= 0 && seconds <= 0)
                    TimeFailed ();
            }else{
                string min;
                string sec;

                if (minutes < 10)
                    min = "0" + minutes.ToString ();
                else
                    min = minutes.ToString ();

                if (seconds < 10)
                    sec = "0" +( Mathf.FloorToInt(seconds)).ToString ();
                else
                    sec = (Mathf.FloorToInt(seconds)).ToString ();


                _text.text = min + ":"+ sec;
            }
        }
    }
}
