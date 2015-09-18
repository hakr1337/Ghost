using UnityEngine;
using System.Collections;

public class Posessable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){
			GameObject player = GameObject.Find("Player");
			Camera.main.transform.parent = player.transform;
			Camera.main.transform.localPosition = Vector3.zero;
			Camera.main.transform.localRotation = player.transform.localRotation;

			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			player.GetComponent<CharacterController>().enabled = true;


			this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
			this.GetComponent<CharacterController>().enabled = false;
			this.GetComponent<BoxCollider>().isTrigger = true;
			this.GetComponent<ParticleSystem>().enableEmission = true;
			Camera.main.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		}
	}
}
