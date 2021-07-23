using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookToCamera : MonoBehaviour
{
    public Camera my_camera;
 
    void Update()
    {
        transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.forward, my_camera.transform.rotation * Vector3.up);
        
        /*if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("M-ai apasat");
        }    */
    }

  
}
