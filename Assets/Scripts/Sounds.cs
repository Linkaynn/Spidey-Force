using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public static Sounds instance = null;

	public new AudioSource[] audio = new AudioSource[4];
	
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

		for (int i = 0; i < audio.Length; i++) {
			if (!audio[i].isPlaying){
				playSource (clip, audio [i]);
				break;
			}
			if (i == audio.Length - 1 && (!audio[i].isPlaying || (audio[i].clip != clips [0] && clip == "coin"))){
				playSource (clip, audio [audio.Length - 1]);
			}
		}
	}

	public void playSource(string clip, AudioSource audioSource){
		switch (clip){
		case "coin":
			audioSource.clip = clips[0];
			audioSource.Play();
			break;
		case "enemiedie":
			audioSource.clip = clips[1];
			audioSource.Play();
			break;
		case "jump":
			audioSource.clip = clips[2];
			audioSource.Play();
			break;
		case "hit":
			audioSource.clip = clips[3];
			audioSource.Play();
			break;
		case "life":
			audioSource.clip = clips[4];
			audioSource.Play();
			break;
		default:
			Debug.Log ("No se encuentra sonido para \"" + clip + "\"");
			break;
		}
	}
}
