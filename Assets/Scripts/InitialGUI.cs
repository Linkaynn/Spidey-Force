using UnityEngine;
using System.Collections;

public class InitialGUI : MonoBehaviour {

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 3.7f - 100, 200, 200));

        if (GUILayout.Button("Play game"))
        {
            Application.LoadLevel(1);
        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Load checkpoint"))
        {
            PlayerPrefs.SetInt("isLoading", 1);
            Application.LoadLevel(1);
        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Instructions"))
        {
            Application.LoadLevel("Help");
        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Quit"))
        {
            Application.Quit();
        }
        GUILayout.EndArea();
    }
}
