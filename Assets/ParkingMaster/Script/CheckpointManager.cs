using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class CheckpointManager : MonoBehaviour
    {
        public int currentCheckpoint;
        public GameObject[] checkpoints;

        IEnumerator Start () {

            for (int a = 0; a < checkpoints.Length; a++) {
                checkpoints [a].SetActive (false);
            }
            checkpoints [0].SetActive (true);
            yield return new WaitForEndOfFrame ();
        }

        public void NextCheckpoint()
        {
            currentCheckpoint++;
            for (int a = 0; a < checkpoints.Length; a++) {
                checkpoints [a].SetActive (false);
            }
            if(checkpoints.Length  >   currentCheckpoint)
                checkpoints [currentCheckpoint].SetActive (true);
        }
    }
}
