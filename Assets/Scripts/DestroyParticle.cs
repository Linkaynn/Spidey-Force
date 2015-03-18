using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("destroy", 2f);
	}
	
	void destroy(){
		Destroy (this.gameObject);
	}
}
