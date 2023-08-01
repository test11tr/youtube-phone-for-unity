using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class Checkpoint : MonoBehaviour
    {
        CheckpointManager _checkpointManager;
        public GameObject[] objectsCheckpointActivates;
        public GameObject[] objectsCheckpointDeactivates;

        void Awake()
        {
            _checkpointManager = GetComponentInParent<CheckpointManager> ();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag ("Player")) {
                _checkpointManager.NextCheckpoint ();
                gameObject.SetActive (false);
                for (int a = 0; a < objectsCheckpointActivates.Length; a++)
                    objectsCheckpointActivates [a].SetActive (true);
                for (int a = 0; a < objectsCheckpointDeactivates.Length; a++)
                    objectsCheckpointDeactivates [a].SetActive (false);
            }
        }
    }
}
