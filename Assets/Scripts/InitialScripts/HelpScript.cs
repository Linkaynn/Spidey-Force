using UnityEngine;
using System.Collections;

public class HelpScript : MonoBehaviour {

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
        Application.LoadLevel("Help");
    }
}
