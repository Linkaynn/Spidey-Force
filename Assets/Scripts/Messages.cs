using UnityEngine;
using System.Collections;

public class Messages : MonoBehaviour {

	private GameController score; //Texto con la puntuación

	private ArrayList arrayOfMessages;
	private int nMessages = 0;

	public GameObject message;

	private int after, before;

	// Use this for initialization
	void Start () {
		score = GameController.instance;
		arrayOfMessages = new ArrayList ();
		message.guiText.fontSize = 10;
		after = 0;
		before = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		moveUp ();

		before = score.getScore();
		if (after - before < 0) {
			float random = Random.Range(0.01f,0.1f);
			message.guiText.text = "" + (before - after);
			GameObject aux = Instantiate(message, new Vector3(0.48f + random, 0.62f + random, 0f), this.transform.rotation) as GameObject;
			after = before;
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
