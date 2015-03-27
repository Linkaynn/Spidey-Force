using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour {

    public GameObject AllGUI;

    private Sounds sound = null;

    private bool inDialog = false;

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
        checkPlay();
    }

    private void checkPlay(){
        if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 1)
        {
            inDialog = true;
            AllGUI.transform.position += new Vector3(20, 20, 20);
        }
        else
        {
            PlayerPrefs.SetInt("End", 0);
            Application.LoadLevel(1);
        }
    }

    void OnGUI()
    {
        if (inDialog)
        {
            GUI.BeginGroup(new Rect((Screen.width - 400) / 2, (Screen.height - 200) / 2, 400, 400));
            GUI.Box(new Rect(0, 0, 400, 200), "");
            GUI.Label(new Rect((400 - 230) / 2, (100 - 30) / 2, 230, 90),
                "Are you sure you want start a new game having one? " +
                    "\nIf you restart and you reach a checkpoint your load file will be rewritten.");
            if (GUI.Button(new Rect((400 - 230) / 2, (200 - 30) / 2 + 40, 100, 30), "Yes"))
            {
                PlayerPrefs.SetInt("End", 0);
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect((400 - 230) / 2 + 130, (200 - 30) / 2 + 40, 100, 30), "No"))
            {
                inDialog = false;
                AllGUI.transform.position -= new Vector3(20, 20, 20);
            }
            GUI.EndGroup();
        }
    }
}
