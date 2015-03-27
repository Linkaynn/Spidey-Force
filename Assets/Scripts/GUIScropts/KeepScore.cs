using UnityEngine;
using System.Collections;

public class KeepScore : MonoBehaviour {

    public static KeepScore instance = null;

    public Vector3 transformReal;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("End", 0) == 1)
            this.transform.position = new Vector3(20, 20, 20);
        else
            this.transform.position = transformReal;
    }
}
