using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRoomScript : MonoBehaviour
{
    public Image image;
    public Text text;
    public GameObject group;

    public Sprite sageataSus;
    public Sprite sageataDreapta;
    public Sprite sageataStanga;
    public Sprite Flag;

    public void RoomNumber(int val)
    {
        switch(val)
        {
            case 0: FirstRoom(); break;
            case 1: SecondRoom();  break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break;
            case 8: break;
            default: break;
        }
    }
    void FirstRoom()
    {
        Sprite[] imagini = { sageataSus, sageataDreapta, sageataStanga, Flag };
        string[] textt = { "Mergeti la inainte.", "Mergeti la dreapta.", "Mergeti la stanga.", "Ati ajuns la destinatie." };

        CreateObjects(imagini, textt);
    }

    void SecondRoom()
    {
        Sprite[] imagini = { sageataDreapta, sageataStanga, sageataSus, sageataDreapta, sageataStanga, sageataDreapta, sageataDreapta, sageataDreapta, sageataStanga, sageataSus, sageataDreapta, sageataStanga, sageataDreapta, sageataDreapta, Flag };
        string[] textt = { "Mergeti la dreapta si acum la dreapta la dreapta la dreapta la stanga.", "Mergeti la stanga.", "Mergeti inainte.", "Mergeti la dreapta.", "Mergeti la stanga.", "Mergeti la dreapta.", "Mergeti la dreapta.", "Mergeti la dreapta si acum la dreapta la dreapta la dreapta la stanga.", "Mergeti la stanga.", "Mergeti inainte.", "Mergeti la dreapta.", "Mergeti la stanga.", "Mergeti la dreapta.", "Mergeti la dreapta.", "Ati ajuns la destinatie." };
        CreateObjects(imagini, textt);
    }

    private void CreateObjects(Sprite[] imagini, string[] textt)
    {
        for (int i = 0; i < imagini.Length; i++)
        {
            image.sprite = imagini[i];
            text.text = textt[i];
            GameObject group1 = Instantiate<GameObject>(group) as GameObject;
            group1.transform.SetParent(transform);
            group1.transform.localScale = new Vector3(1f, 1f, 1f);
            group1.SetActive(true);

        }
    }
    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
