using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    // Cand apesi pe o cladire din MapPanel sau FindPanel, avand fiecare cladire un nr, se activeaza butoanele pentru interior 
    // conform cladirii, la fel si interiorul

    public GameObject searchPanel, favoritesPanel, navigationPanel, mainPagePanel, routePanel;
    public Button mapButton, searchButton, favoritesButton;
    public Image searchButtonImage, favoritesButtonImage;
    public Text searchButtonText, favoritesButtonText;

    public Camera interiorCamera, mapCamera, userCamera;

    public Button backButton, routeButton, changeViewButton, changeCameraButton, resetButton;
    public Text changeViewText;
    public GameObject mapButtonsPanel;

    public GameObject BuildingsButtonParent;
    public Button buildingsButtonPrefab;
    public Text buildingsTextPrefab;
    public string[] buildingsText;

    public GameObject TwoFloorPanel;
    public Button F1, F2;

    string lastPanelShowed, currentPanel;
    int roomNumber = -1;
    string buildingName;
    bool userCameraOn = false;
    Transform buildingInterior;

    // Start is called before the first frame update
    void Start()
    {
        InitializeApp();
    }

    private void Update()
    {
        CheckIfCameraMoved();
    }

    private void CheckIfCameraMoved()
    {
        if (userCamera.transform.localPosition != new Vector3(5.06f, 2.49f, 0f) && userCamera.gameObject.activeSelf)
        {
            resetButton.gameObject.SetActive(true);
        }
        else if (mapCamera.transform.position != new Vector3(10f, 12f, 0f) && mapCamera.gameObject.activeSelf)
        {
            resetButton.gameObject.SetActive(true);
        }
        else
        {
            resetButton.gameObject.SetActive(false);
        }
    }

    void InitializeApp()
    {
        InitializeButtons();
        lastPanelShowed = "SearchPanel";
        currentPanel = lastPanelShowed;
        ShowPanel("SearchPanel");
    }

    void InitializeButtons()
    {
        mapButton.onClick.AddListener(mapButtonClicked);
        searchButton.onClick.AddListener(searchButtonClicked);
        favoritesButton.onClick.AddListener(favoritesButtonClicked);
        backButton.onClick.AddListener(backButtonClicked);
        routeButton.onClick.AddListener(routeButtonClicked);
        changeViewButton.onClick.AddListener(changeViewButtonClicked);
        changeCameraButton.onClick.AddListener(changeCameraButtonClicked);
        resetButton.onClick.AddListener(resetButtonClicked);

        for(int i=0; i<buildingsText.Length; i++)
        {
            buildingsTextPrefab.text = buildingsText[i];
            Button button = Instantiate<Button>(buildingsButtonPrefab) as Button;
            button.transform.SetParent(BuildingsButtonParent.transform);
            button.transform.localScale = new Vector3(1f, 1f, 1f);
            button.gameObject.SetActive(true);
        }

        F1.onClick.AddListener(F1ButtonClicked);
        F2.onClick.AddListener(F2ButtonClicked);
    }

    void F1ButtonClicked()
    {
        for(int i=0;i<buildingInterior.childCount;i++)
        {
            Transform b = buildingInterior.transform.GetChild(i);
            if (b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(false);
                break;
            }
        }
        Transform a = buildingInterior.transform.GetChild(0);
        a.gameObject.SetActive(true);
        F1.interactable = false;
        F2.interactable = true;
    }
    void F2ButtonClicked()
    {
        for (int i = 0; i < buildingInterior.childCount; i++)
        {
            Transform b = buildingInterior.transform.GetChild(i);
            if (b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(false);
                break;
            }
        }
        Transform a = buildingInterior.transform.GetChild(1);
        a.gameObject.SetActive(true);
        F2.interactable = false;
        F1.interactable = true;
    }
    void mapButtonClicked()
    {
        currentPanel = "MapPanel";
        DisableLastPanel();
        ShowPanel(currentPanel);
    }
    void searchButtonClicked()
    {
        currentPanel = "SearchPanel";
        DisableLastPanel();
        ShowPanel(currentPanel);
    }
    void favoritesButtonClicked()
    {
        currentPanel = "FavoritesPanel";
        DisableLastPanel();
        ShowPanel(currentPanel);
    }
    void backButtonClicked()
    {
        switch(currentPanel)
        {
            case "MapPanel":
                searchButtonClicked();
                break;
            case "RoutePanel":
                FindObjectOfType<SelectRoomScript>().DestroyChildren();
                mapButtonClicked();
                break;
            case "InteriorFromMap":
                interiorCamera.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                DisableFloorPanel();
                mapButtonClicked();
                break;
            case "InteriorFromSearch":
                interiorCamera.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                DisableFloorPanel();
                searchButtonClicked();
                break;
        }
    }
    void routeButtonClicked()
    {
        currentPanel = "RoutePanel";
        DisableLastPanel();
        ShowPanel(currentPanel);
        FindObjectOfType<SelectRoomScript>().RoomNumber(roomNumber);
    }
    void resetButtonClicked()
    {
        if (mapCamera.gameObject.activeSelf)
        {
            mapCamera.transform.position = new Vector3(10f, 12f, 0f);
        }
        else
        {
            userCamera.transform.localPosition = new Vector3(5.06f, 2.49f, 0f);
        }

    }
    void changeViewButtonClicked()
    {
        if (changeViewText.text == "3D")
        {
            changeViewText.text = "2D";
            mapCamera.transform.rotation = Quaternion.Euler(90, -90, 0);
        }
        else
        {
            changeViewText.text = "3D";
            mapCamera.transform.rotation = Quaternion.Euler(45, -90, 0);
        }

    }
    void changeCameraButtonClicked()
    {
        if (userCamera.gameObject.activeSelf)
        {
            userCamera.gameObject.SetActive(false);
            mapCamera.gameObject.SetActive(true);
            changeViewButton.interactable = true;
            userCameraOn = false;
        }
        else
        {
            userCamera.gameObject.SetActive(true);
            mapCamera.gameObject.SetActive(false);
            changeViewButton.interactable = false;
            userCameraOn = true;
        }
    }


    void ShowPanel(string name)
    {
        switch(name)
        {
            case "SearchPanel":
                mainPagePanel.SetActive(true);
                navigationPanel.SetActive(true);
                searchPanel.SetActive(true);
                ActivateNavigateButton(currentPanel);
                break;
            case "FavoritesPanel":
                mainPagePanel.SetActive(true);
                favoritesPanel.SetActive(true);
                navigationPanel.SetActive(true);
                ActivateNavigateButton(currentPanel);
                break;
            case "MapPanel":
                mapButtonsPanel.SetActive(true);
                backButton.gameObject.SetActive(true);
                routeButton.gameObject.SetActive(true);
                if (userCameraOn)
                    userCamera.gameObject.SetActive(true);
                else
                    mapCamera.gameObject.SetActive(true);
                break;
            case "RoutePanel":
                routePanel.SetActive(true);
                backButton.gameObject.SetActive(true);
                break;
            case "Interior":
                interiorCamera.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                break;
        }
    }

    void DisableLastPanel()
    {
        if(currentPanel != lastPanelShowed)
            switch (lastPanelShowed)
            {
                case "SearchPanel":
                    mainPagePanel.SetActive(false);
                    navigationPanel.SetActive(false);
                    searchPanel.SetActive(false);
                    break;
                case "FavoritesPanel":
                    mainPagePanel.SetActive(false);
                    favoritesPanel.SetActive(false);
                    navigationPanel.SetActive(false);
                    break;
                case "MapPanel":
                    mapButtonsPanel.SetActive(false);
                    backButton.gameObject.SetActive(false);
                    routeButton.gameObject.SetActive(false);
                    if(userCameraOn)
                        userCamera.gameObject.SetActive(false);
                    else
                        mapCamera.gameObject.SetActive(false);
                    break;
                case "RoutePanel":
                    routePanel.SetActive(false);
                    backButton.gameObject.SetActive(false);
                    break;
            }
        lastPanelShowed = currentPanel;
    }
    
    void ActivateNavigateButton(string panelName)
    {
        Color col1, col2;
        switch (panelName)
        {
            case "SearchPanel":
                searchButtonImage.GetComponent<Image>().color = Color.blue;
                searchButtonText.GetComponent<Text>().color = Color.blue;
                col1 = searchButton.GetComponent<Image>().color;
                col1.a = 0.4f;
                searchButton.GetComponent<Image>().color = col1;

                favoritesButtonImage.GetComponent<Image>().color = Color.black;
                favoritesButtonText.GetComponent<Text>().color = Color.black;
                col2 = favoritesButton.GetComponent<Image>().color;
                col2.a = 0f;
                favoritesButton.GetComponent<Image>().color = col2;
                break;
            case "FavoritesPanel":
                searchButtonImage.GetComponent<Image>().color = Color.black;
                searchButtonText.GetComponent<Text>().color = Color.black;
                col1 = searchButton.GetComponent<Image>().color;
                col1.a = 0f;
                searchButton.GetComponent<Image>().color = col1;

                favoritesButtonImage.GetComponent<Image>().color = Color.blue;
                favoritesButtonText.GetComponent<Text>().color = Color.blue;
                col2 = favoritesButton.GetComponent<Image>().color;
                col2.a = 0.4f;
                favoritesButton.GetComponent<Image>().color = col2;
                break;
        }
    }

    public void EnterInteriorFromMap(string name)
    {
        currentPanel = "InteriorFromMap";
        DisableLastPanel();
        ShowPanel("Interior");
        ShowInterior(name);
    }
    public void EnterInteriorFromSearch(string name)
    {
        currentPanel = "InteriorFromSearch";
        DisableLastPanel();
        ShowPanel("Interior");
        ShowInterior(name);
    }


    void ShowInterior(string name)
    {
        buildingName = name;
        GameObject interiors = GameObject.Find("Interiors");
        switch (name)
        {
            case "Corp EC":
                buildingInterior = interiors.transform.GetChild(0);
                buildingInterior.gameObject.SetActive(true);
                ActivateFloorPanel();
                FindObjectOfType<NavScript>().SetDestination(new Vector3(-16.864f, 1.8f, 8.568f));
                break;
        }
    }

    public void StartRoute(int val)
    {
        roomNumber = val;
        currentPanel = "MapPanel";
        DisableLastPanel();
        ShowPanel(currentPanel);
        mapCamera.gameObject.SetActive(false);
        userCamera.gameObject.SetActive(true);
        userCameraOn = true;
        changeViewButton.interactable = false;
        DisableFloorPanel();
    }

    void ActivateFloorPanel()
    {
        switch(buildingName)
        {
            case "Corp EC":
                TwoFloorPanel.SetActive(true);
                F1.interactable = false;
                break;
        }
    }

    void DisableFloorPanel()
    {
        switch (buildingName)
        {
            case "Corp EC":
                TwoFloorPanel.SetActive(false);
                break;
        }
    }
}
