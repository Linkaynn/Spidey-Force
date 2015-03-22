using UnityEngine;
using System.Collections;

public class HelpGUI : MonoBehaviour {

    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 3.5f - 100, 200, 200));

        GUILayout.Space(140);

        if (GUILayout.Button("Back"))
        {
            Application.LoadLevel(0) ;
        }
        GUILayout.EndArea();
    }
}
