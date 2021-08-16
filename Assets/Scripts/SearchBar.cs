using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBar : MonoBehaviour
{
    public GameObject imagine;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<UnityEngine.UI.Text>().isActiveAndEnabled == false)
            imagine.SetActive(false);
        else
            imagine.SetActive(true);

    }
}
