using UnityEngine;
using System.Collections;

public class Posessable : MonoBehaviour {
	public bool posessed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape) && posessed){
			GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
			GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
			this.GetComponent<Collider>().isTrigger = true;
			posessed = false;

			if(gameObject.tag == "balltrigger"){
				gameObject.GetComponentInParent<ball>().deposess();
			}
		
		}

	}
}
