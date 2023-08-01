using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class DragCamera : MonoBehaviour
    {
       public OrbitCamera _camera;
        IEnumerator Start () {

            yield return new WaitForEndOfFrame ();

            if(!_camera)
                _camera = GameObject.Find("OrbitCamera").GetComponent<OrbitCamera> ();

            if(!_camera)
                this.enabled = false;
            
        }

        public void EnableDrag(bool state)
        {
            if (state)
                _camera.canDrag = true;
            else
                _camera.canDrag = false ;

        }
    }
}
