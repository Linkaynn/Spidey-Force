using UnityEngine;
using UnityEditor;
using System.Collections;

public class InitialGUI : MonoBehaviour {

    void OnGUI()
    {

        PlayerPrefs.SetInt("End", 0);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 3.7f - 100, 200, 200));

        if (GUILayout.Button("Play game"))
        {
            if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 1)
            {
                if (EditorUtility.DisplayDialog("Restart with load file",
                "Are you sure you want start a new game having one? " +
                "If you restart your load file will be rewritten.",
                "Yes, I'm sure",
                "No, please"))
                {
                    Application.LoadLevel(1);
                }
            }
            else
            {
                Application.LoadLevel(1);
            }
        }

        GUILayout.Space(20f);

        if (PlayerPrefs.GetInt("haveCheckpoint", 0) == 1)
        {
            if (GUILayout.Button("Load checkpoint"))
            {
                PlayerPrefs.SetInt("isLoading", 1);
                Application.LoadLevel(1);
            }
        }
        else
        {
            GUILayout.Button("Can't find load file");
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
