using UnityEngine;
using System.Collections;

public class cameraStuff : MonoBehaviour {
	GameObject p;
	GameObject[] x;

	public bool visionOn = false;

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
			visionOn = !visionOn;
			//p.SetActive(!p.activeSelf);
			//GameObject.Find("visable vision").GetComponent<MeshRenderer>().enabled = !GameObject.Find("visable vision").GetComponent<MeshRenderer>().enabled;
			GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;

			foreach(GameObject o in x){
				o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
				o.GetComponentInChildren<ParticleSystem>().Clear();
			}
		}
	}

	public void turnOff(){

		this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
		visionOn = !visionOn;
		//p.SetActive(!p.activeSelf);
		//GameObject.Find("visable vision").GetComponent<MeshRenderer>().enabled = !GameObject.Find("visable vision").GetComponent<MeshRenderer>().enabled;
		GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;
		
		foreach(GameObject o in x){
			o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
			o.GetComponentInChildren<ParticleSystem>().Clear();
		}
	}
}
