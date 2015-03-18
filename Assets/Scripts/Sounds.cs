using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public static Sounds instance = null;

	public new AudioSource audio;
	
	public AudioClip[] clips = new AudioClip[5];
	/* Clips
	 * 0: Coin (coin)
	 * 1: Enemy die (enemiedie)
	 * 2: Player jump (jump)
	 * 3: Enemy hit player (hit)
	 * 4: Take Life (life)
	 */

	// Use this for initialization
	void Start () {
		if (audio == null) {
			Debug.Log("ERROR: Expect AudioSource in Sounds.cs script.");
		}
		instance = this;
	}


	// I decide do a switch to do more clean the code of Player.cs script because is a but more larger that this
	public void playSound(string clip){
		if (!audio.isPlaying || (audio.clip != clips[0] && clip == "coin")){
			switch (clip){
			case "coin":
				audio.clip = clips[0];
				audio.Play();
				break;
			case "enemiedie":
				audio.clip = clips[1];
				audio.Play();
				break;
			case "jump":
				audio.clip = clips[2];
				audio.Play();
				break;
			case "hit":
				audio.clip = clips[3];
				audio.Play();
				break;
			case "life":
				audio.clip = clips[4];
				audio.Play();
				break;
			default:
				Debug.Log ("No se encuentra sonido para \"" + clip + "\"");
				break;
			}
		}
	}
}
