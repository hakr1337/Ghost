using UnityEngine;
using System.Collections;

public class cameraStuff : MonoBehaviour {
	GameObject p;
	GameObject[] x;
	// Use this for initialization
	void Start () {
		p = GameObject.Find("Player");
		x = GameObject.FindGameObjectsWithTag("Particles");

		foreach(GameObject o in x){
			o.GetComponentInChildren<ParticleSystem>().enableEmission = false;	
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;

			//p.SetActive(!p.activeSelf);

			foreach(GameObject o in x){
				o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
				o.GetComponentInChildren<ParticleSystem>().Clear();
			}
		}
	}
}
