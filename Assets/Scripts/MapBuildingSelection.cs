using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuildingSelection : MonoBehaviour
{
    public string name = "";
    float lastClick = 0f;
    float clickInterval = 0.1f;
    private void OnMouseDown()
    {
        lastClick = Time.time;
    }

    private void OnMouseUp()
    {
        if ((lastClick + clickInterval) < Time.time)
        {//Single click
            //FindObjectOfType<SceneManagerScript>().IntraInCladire();
            FindObjectOfType<SceneManager>().EnterInteriorFromMap(name);
        }
    }
}
