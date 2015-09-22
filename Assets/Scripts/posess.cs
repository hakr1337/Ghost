using UnityEngine;
using System.Collections;

public class posess : MonoBehaviour {

	GameObject[] posessables = new GameObject[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKey (KeyCode.Q)) {

			
		//}
	}

	/*void OnTriggerEnter(Collider c){






		//if (Input.GetKey (KeyCode.Q)) {
			if (c.GetComponent<Posessable> ()) {
				//this.gameObject.SetActive(false);
				for(int i = 0; i < 5; i++){
				 	if(posessables[i] == null){
						posessables[i] = c;
					}
				}
			}
		//}
	}*/

	void OnTriggerStay(Collider c){
		//print (c.name);
		if (Input.GetKeyDown (KeyCode.Q)) {
			c.GetComponent<Collider>().isTrigger = false;
			c.GetComponent<Posessable>().posessed = true;
			this.gameObject.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
			this.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
			this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
