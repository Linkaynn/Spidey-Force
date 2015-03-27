using UnityEngine;
using System.Collections;

public class BackScript : MonoBehaviour {

    private Sounds sound = null;

    void Start()
    {
        sound = Sounds.instance;
    }

    void OnMouseOver()
    {
        guiText.color = Color.yellow;
    }


    void OnMouseExit()
    {
        guiText.color = Color.black;
    }

    void OnMouseDown()
    {
        Application.LoadLevel(0);
    }
}
