using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        string buildingName = transform.GetComponentInChildren<Text>().text;
        FindObjectOfType<SceneManager>().EnterInteriorFromSearch(buildingName);
    }
}
