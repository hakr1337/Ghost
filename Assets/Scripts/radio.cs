using UnityEngine;
using System.Collections;

public class radio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponentInChildren<Posessable> ().posessed) {
			if(Input.GetKeyDown(KeyCode.Space)){
				if(this.GetComponent<AudioSource>().isPlaying){
					this.GetComponent<AudioSource>().Stop();
				}else{
					this.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
