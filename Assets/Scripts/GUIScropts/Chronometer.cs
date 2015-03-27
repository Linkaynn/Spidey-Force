using UnityEngine;
using System.Collections;

public class Chronometer : MonoBehaviour {

    private float time;

    private float miliseconds = 0f;
    private int seconds = 0;
    private int minutes = 0;

    public static int[] variables = new int[2];

    private GameController gameController;

    public static Chronometer instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        variables[0] = seconds;
        variables[1] = minutes;

    }

	// Use this for initialization
	void Start () {
        time = 0f;
        miliseconds = 0;
        seconds = 0;
        minutes = 0;

        gameController = GameController.instance;

	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("End", 0) == 1)
            this.guiText.enabled = false;
        else
            this.guiText.enabled = true;

        if (gameController.nlifes > 0)
        {
            time += Time.deltaTime;

            miliseconds = Mathf.Floor((time - Mathf.Floor(time)) * 100f);

            if (miliseconds >= 90)
            {
                time = 0;
                miliseconds = 0f;
                seconds++;
            }

            if (seconds >= 59)
            {
                seconds = 0;
                minutes++;
            }

            gameObject.guiText.text = "" + minutes + ":" + seconds + ":" + miliseconds;
        }
	}

    public int[] getVariables()
    {
        int[] aux = new int[2];
        aux[0] = seconds;
        aux[1] = minutes;
        return aux;
    }

    public void setVariables(int minutes, int seconds)
    {
        this.minutes = minutes;
        this.seconds = seconds;
    }
}
