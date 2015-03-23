using UnityEngine;
using System.Collections;

public class KeepLifes : MonoBehaviour {

	public static KeepLifes instance = null;

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
            Destroy(gameObject);
    }
}
