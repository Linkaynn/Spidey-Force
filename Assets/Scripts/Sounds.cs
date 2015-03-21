using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public static Sounds instance = null;

	public AudioSource mainAudio; //AudioSource player of the base music

	public AudioClip[] baseClips = new AudioClip[2]; //Vector of base clips

	public new AudioSource[] audio = new AudioSource[4]; //Vector of AudioSource player of the short clips
	
	public AudioClip[] clips = new AudioClip[5]; //Vector of short clips
	/* Clips
	 * 0: Coin (coin)
	 * 1: Enemy die (enemiedie)
	 * 2: Player jump (jump)
	 * 3: Enemy hit player (hit)
	 * 4: Take Life (life)
	 * 5: Throw knife (knife)
	 * 6: Take sword (sword)
     * 7: Sword hit (swordh)
     * 8: Sword no hit (swordnh)
	 */

	// Use this for initialization
	void Start () {

		if (audio == null) {
			Debug.Log("ERROR: Expect AudioSource in Sounds.cs script.");
		}

		instance = this;

		mainAudio = gameObject.GetComponent<AudioSource> ();
	}

	/**SHORT CLIPS**/
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

	private void playSource(string clip, AudioSource audioSource){
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
		case "knife":
			audioSource.clip = clips[5];
			audioSource.Play();
			break;
		case "sword":
			audioSource.clip = clips[6];
			audioSource.Play();
			break;
        case "swordh":
            audioSource.clip = clips[7];
            audioSource.Play();
            break;
        case "swordnh":
            audioSource.clip = clips[8];
            audioSource.Play();
            break;
		default:
			Debug.Log ("No se encuentra sonido para \"" + clip + "\"");
			break;
		}
	}
	/*************/

	/**BASE MUSIC**/

	public void changeBaseClip(string clip){
		switch (clip){
		case "Base_1":
			mainAudio.clip = baseClips[0];
			mainAudio.Play();
			break;
		case "Boss":
			mainAudio.clip = baseClips[1];
			mainAudio.Play();
			break;
		default:
			Debug.Log ("No se encuentra sonido para \"" + clip + "\"");
			break;
		}
	}

	/**************/
}
