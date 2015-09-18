using UnityEngine;
using System.Collections;

public class posess : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider c){
		if (c.GetComponent<Posessable>()) {
			Camera.main.transform.parent = c.transform;
			Camera.main.transform.localPosition = Vector3.zero;

			gameObject.GetComponent<CharacterController>().enabled = false;
			gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

			Camera.main.transform.localRotation = c.transform.localRotation;

			c.gameObject.GetComponent<CharacterController>().enabled = true;
			c.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;

			c.GetComponent<BoxCollider>().isTrigger = false;
			c.GetComponent<ParticleSystem>().enableEmission = false;
			Camera.main.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
		}
	}
}
