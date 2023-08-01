using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using test11.Managers;

namespace test11
{
    public class openingBarrier : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

       void OnTriggerStay(Collider other) {
            if(other.tag == "Player"){
                _animator.SetBool("isOpened", false);
            }
       }

       void OnTriggerExit(Collider other){
            if(other.tag == "Player"){
                _animator.SetBool("isOpened", true);
            }
       } 
    }
}
