using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {
	GameObject b;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponentInChildren<Posessable> ().posessed == true) {
			GameObject.FindGameObjectWithTag("balltrigger").GetComponent<SphereCollider>().enabled = false;
			gameObject.GetComponentInParent<ballControl> ().enabled = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
		} else {
			gameObject.GetComponentInParent<ballControl> ().enabled = false;
			GameObject.FindGameObjectWithTag("balltrigger").GetComponent<SphereCollider>().enabled = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	public void deposess(){
		Transform t = GameObject.Find("Player").transform;

		t.position = this.transform.position;
	}
	
}
