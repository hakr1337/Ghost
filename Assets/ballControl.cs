using UnityEngine;
using System.Collections;

public class ballControl : MonoBehaviour {
	public float speed = .05f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			//this.GetComponent<Rigidbody> ().AddForce (transform.forward * 2);
			this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z);
		} else if (Input.GetKey (KeyCode.S)) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x - speed,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z);
		}

		if (Input.GetKey (KeyCode.A)) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z + speed);
		} else if (Input.GetKey (KeyCode.D)) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z - speed);
		}
	}
}
