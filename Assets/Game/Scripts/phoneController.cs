using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class phoneController : MonoBehaviour
    {
        [SerializeField] private Animator _phoneAnimator;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if ((Input.GetKeyDown(KeyCode.P)))
            {
                if(_phoneAnimator.GetBool("isClosed") == true){
                    _phoneAnimator.SetBool("isClosed", false);
                }else{
                    _phoneAnimator.SetBool("isClosed", true);
                }
            }
        }
    }
}
