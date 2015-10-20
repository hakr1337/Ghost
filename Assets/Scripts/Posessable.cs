using UnityEngine;
using System.Collections;

public class Posessable : MonoBehaviour {
	public bool posessed = false;
	public bool shouldScare = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W)||
		    Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)||
		    Input.GetKeyDown(KeyCode.D))&& posessed){
			shouldScare = !shouldScare;
		}

		if (Input.GetKey(KeyCode.Escape) && posessed){
			GameObject.Find("Player").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
			GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
			GameObject.Find("Player").GetComponent<CapsuleCollider>().enabled = true;
			this.GetComponent<Collider>().isTrigger = true;
			posessed = false;


			if(gameObject.tag == "balltrigger"){
				gameObject.GetComponentInParent<ball>().deposess();
			}
		
		}

	}
}
