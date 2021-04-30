using UnityEngine;
using UnityEngine.Networking;
using Google.Maps.Event;
using Google.Maps.Examples.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Google.Maps.Examples {
  /// <summary>
  /// Building names example, demonstrating how to get the name of a building created by the Maps
  /// SDK.
  /// </summary>
  [RequireComponent(typeof(MapLabeller))]
  public class BuildingLabelsCreator : MonoBehaviour {
    /// <summary>
    /// The Labeller used to create building labels.
    /// </summary>
    private MapLabeller Labeller;

    public MapsService mapsService;

    private Dictionary<string, string> namedPlaces = new Dictionary<string, string>(){
      { "ChIJ3w9Ub8EBskARp2zFSYUOjdk", "Corp AN" },
      { "ChIJte0v2cMBskARt9YM0wfr5Fc", "Rectorat" },
      {"ChIJY3fgn8EBskARwVrAnuRzyoU", "Corp BN" },
    };

    void Awake() {
      Labeller = GetComponent<MapLabeller>();
    }

    /// <summary>
    /// Add listeners for new building creations.
    /// </summary>
    void OnEnable() {
      Labeller.BaseMapLoader.MapsService.Events.ExtrudedStructureEvents.DidCreate.AddListener(
          OnExtrudedStructureCreated);
      Labeller.BaseMapLoader.MapsService.Events.ModeledStructureEvents.DidCreate.AddListener(
          OnModeledStructureCreated);
    }

    /// <summary>
    /// Remove listeners for new building creations.
    /// </summary>
    void OnDisable() {
      Labeller.BaseMapLoader.MapsService.Events.ExtrudedStructureEvents.DidCreate.RemoveListener(
          OnExtrudedStructureCreated);
      Labeller.BaseMapLoader.MapsService.Events.ModeledStructureEvents.DidCreate.RemoveListener(
          OnModeledStructureCreated);
    }

    void OnExtrudedStructureCreated(DidCreateExtrudedStructureArgs args) {
      CreateLabel(args.GameObject, args.MapFeature.Metadata.PlaceId, args.MapFeature.Metadata.Name);
    }

    void OnModeledStructureCreated(DidCreateModeledStructureArgs args) {
      CreateLabel(args.GameObject, args.MapFeature.Metadata.PlaceId, args.MapFeature.Metadata.Name);
    }

    /// <summary>
    /// Creates a label for a building.
    /// </summary>
    /// <param name="buildingGameObject">The GameObject of the building.</param>
    /// <param name="placeId">The place ID of the building.</param>
    /// <param name="displayName">The name to display on the label for the building.</param>
    void CreateLabel(GameObject buildingGameObject, string placeId, string displayName) {
      if (!Labeller.enabled)
        return;

      if (namedPlaces.ContainsKey(placeId))
      {
        displayName = namedPlaces[placeId];
      }

      // Ignore uninteresting names.
      if (displayName.Equals("ExtrudedStructure") || displayName.Equals("ModeledStructure"))
      {
        return;
      }


      Label label = Labeller.NameObject(buildingGameObject, placeId, displayName);
      if (label != null) {
        MapsGamingExamplesUtils.PlaceUIMarker(buildingGameObject, label.transform);
      }
    }

    // Fetch the name of a place via the Places API.
    IEnumerator GetPlaceName(GameObject buildingGameObject, string placeId)
    {
      string url = $"https://maps.googleapis.com/maps/api/place/details/json?placeid={placeId}&key={mapsService.ApiKey}";
      Debug.Log(url);
      UnityWebRequest www = UnityWebRequest.Get(url);
      yield return www.SendWebRequest();

      if (www.result != UnityWebRequest.Result.Success)
      {
        Debug.Log(www.error);
      }
      else
      {
        Debug.Log(www.downloadHandler.text);
        var attributes = JsonUtility.FromJson<Place>(www.downloadHandler.text);
        string displayName = attributes.result.name;

        Label label = Labeller.NameObject(buildingGameObject, placeId, displayName);
        if (label != null)
        {
          MapsGamingExamplesUtils.PlaceUIMarker(buildingGameObject, label.transform);
        }
      }
    }
  }
}
