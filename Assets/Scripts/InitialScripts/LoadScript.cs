using UnityEngine;
using System.Collections;

public class LoadScript : MonoBehaviour {

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

    void Update()
    {
        if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 0)
        {
            guiText.text = "¡CAN'T";
        }
    }

    void OnMouseDown()
    {
        load();
    }

    private void load()
    {
        PlayerPrefs.SetInt("End", 0);
        PlayerPrefs.SetInt("isLoading", 1);
        Application.LoadLevel(1);
    }
}
