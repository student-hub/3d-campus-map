using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomSelection: MonoBehaviour
{
    public int nr;

    private void OnMouseDown()
    {
        FindObjectOfType<SceneManager>().StartRoute(nr);
    }
   /* private void OnMouseOver()
    {
        if (gameObject.transform.position.y < 0.5f)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime / 2);
    }

    private void OnMouseExit()
    {
        while (gameObject.transform.position.y > 0f)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y / 10 - Time.deltaTime);
    }*/
}
