using UnityEngine;
using System.Collections;

public class Posessable : MonoBehaviour {
	public bool posessed = false;
	public bool shouldScare = false;
    public bool lit = false;

    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        //if object is posessed and activated by player is should scare (buggy)
		if((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W)||
		    Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)||
		    Input.GetKeyDown(KeyCode.D))&& posessed){
			shouldScare = !shouldScare;
		}

        //exit posession
		if (Input.GetKey(KeyCode.Escape) && posessed){

            //re-enable player
			player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;//turn on mesh renderer
            player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;//turn on control
            player.GetComponent<Rigidbody>().isKinematic = false;//unfix
            player.GetComponent<CapsuleCollider>().enabled = true;//turn on collider
            player.GetComponent<posess>().one = false;//enable posession of another object

            //disable object
            this.GetComponent<Collider>().isTrigger = true;//turn trigger back on 
            this.lit = false;//mark unlit
			posessed = false;

            //if ball call deposess function in ball
			if(gameObject.tag == "balltrigger"){
				gameObject.GetComponentInParent<ball>().deposess();
			}
		
		}

	}
}
