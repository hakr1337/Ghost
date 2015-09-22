using UnityEngine;
using System.Collections;

public class lamp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponentInChildren<Posessable> ().posessed) {
			if(Input.GetKeyDown(KeyCode.Space)){
				this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
			}
		}
	}
}
