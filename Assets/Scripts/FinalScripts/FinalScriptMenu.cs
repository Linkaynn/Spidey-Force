using UnityEngine;
using System.Collections;

public class FinalScriptMenu : MonoBehaviour {

    void OnMouseOver()
    {
        guiText.color = Color.green;
        guiText.fontSize = 32;
    }


    void OnMouseExit()
    {
        guiText.color = Color.black;
        guiText.fontSize = 30;
    }

    void OnMouseDown()
    {
        Destroy(GameObject.FindWithTag("MainCamera").gameObject);
        Destroy(GameObject.FindWithTag("GuiSword").gameObject);
        Destroy(GameObject.FindWithTag("GuiLife").gameObject);
        Destroy(GameObject.FindWithTag("Chronometer").gameObject);
        Application.LoadLevel(0);
    }
}
