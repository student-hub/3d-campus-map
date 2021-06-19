using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlsTouch : MonoBehaviour
{
  private Vector2 worldStartPoint;

  void Update()
  {
    // only work with one touch
    if (Input.touchCount == 1)
    {
      Touch currentTouch = Input.GetTouch(0);

      if (currentTouch.phase == TouchPhase.Began)
      {
        worldStartPoint = getWorldPoint(currentTouch.position);
      }

      if (currentTouch.phase == TouchPhase.Moved)
      {
        Vector2 worldDelta = getWorldPoint(currentTouch.position) - worldStartPoint;
        Debug.Log("current touch: " + getWorldPoint(currentTouch.position) + " worldStartPoint: " + worldStartPoint);

        Camera.main.transform.Translate(
            -worldDelta.x,
            -worldDelta.y,
            0
        );
      }
    }
  }

  // convert screen point to world point
  private Vector2 getWorldPoint(Vector2 screenPoint)
  {
    RaycastHit hit;
    Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
    return hit.point;
  }
}
