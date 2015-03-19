using UnityEngine;
using System.Collections;

public class Messages : MonoBehaviour {

	private GameController gameController; //Texto con la puntuación

	private ArrayList arrayOfMessages;
	private int nMessages = 0;

	public GameObject message;

	private int sAfter, sBefore;
	private bool scoreUp;

	private int lAfter, lBefore;
	private bool life;

	// Use this for initialization
	void Start () {
		gameController = GameController.instance;
		arrayOfMessages = new ArrayList ();
		message.guiText.fontSize = 10;
		sAfter = 0;
		sBefore = 0;
		lAfter = 3;
		lBefore = 3;
	
	}
	
	// Update is called once per frame
	void Update () {

		moveUp ();

		sBefore = gameController.getScore();

		if (sAfter != sBefore)
			scoreUp = true;

		lBefore = gameController.nlifes;

		if (lAfter != lBefore)
			life = true;

		if (sAfter != sBefore || lAfter != lBefore) {
			float random = Random.Range(0.01f,0.1f);

			if (scoreUp){
				message.guiText.text = "" + (sBefore - sAfter);
				message.guiText.color = Color.black;
				sAfter = sBefore;
				scoreUp = false;
			}

			if (life){
				if (lBefore - lAfter < 0)
					message.guiText.text = "" + (lBefore - lAfter);
				else
					message.guiText.text = "+" + (lBefore - lAfter);
				message.guiText.color = Color.red;
				lAfter = lBefore;
				life = false;
			}

			GameObject aux = Instantiate(message, new Vector3(0.48f + random, 0.62f + random, 0f), this.transform.rotation) as GameObject;

			arrayOfMessages.Add(aux);
			nMessages++;
		}

		if (nMessages > 0) {
			Invoke ("removeLast", 1f);
			nMessages--;
		}

	}

	void removeLast(){
		GameObject aux = arrayOfMessages[arrayOfMessages.Count -1] as GameObject;
		Destroy (aux);
		arrayOfMessages.RemoveAt(arrayOfMessages.Count - 1);
	}

	void moveUp(){
		float smoothValor = 0.2f;
		for (int i = 0; i < arrayOfMessages.Count; i++) {
			GameObject aux = arrayOfMessages[i] as GameObject;
			aux.transform.Translate(0, smoothValor * Time.deltaTime, 0);
		}
	}
}
