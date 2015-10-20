using UnityEngine;
using System.Collections;

public class posess : MonoBehaviour {

	GameObject[] posessables = new GameObject[15];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKey (KeyCode.Q)) {

			
		//}
	}
	

	void OnTriggerStay(Collider c){
		//print (c.name);
		if (Input.GetKeyDown (KeyCode.Q) && c.GetComponent<Posessable>() != null) {
			if(Camera.main.GetComponent<cameraStuff>().visionOn){
				Camera.main.GetComponent<cameraStuff>().turnOff();
			}

			c.GetComponent<Collider>().isTrigger = false;
			c.GetComponent<Posessable>().posessed = true;

			this.gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
			this.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
			this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
		}
	}
}
