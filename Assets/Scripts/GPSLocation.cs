using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());
    }


    IEnumerator GPSLoc()
    {
        // check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // start service before querying location
        Input.location.Start();

        // wait until service initialize
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // service didn't init in 20 seconds
        if (maxWait < 1)
        {
            yield break;
        }

        // connection failed 
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        else
        {
            //Acces granted
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Access granted to GPS values and it has been init

            float x = latToX(Input.location.lastData.latitude);
            float z = lonToZ(Input.location.lastData.longitude);
            player.transform.position = new Vector3(x, 0.8f, z);
        }
        else
        {
            // Service is stopped
        }
    }

    float latToX(double lat)
    {
        lat = (lat - 44.44170) * 100000 * (0.02897436);
        double x = lat;
        return (float)x;
    }
    //44.44178 26.0516 - campus
    float lonToZ(double lon)
    {
        lon = (lon - 26.05160) * 100000 * (-0.02);
        double z = lon;
        return (float)z;
    }
}
