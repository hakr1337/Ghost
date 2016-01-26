using UnityEngine;
using System.Collections;

public class Posessable : MonoBehaviour {
	public bool posessed = false;
	public bool shouldScare = false;
    public bool lit = false;
	public int color;

    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        //if object is posessed and activated by player is should scare (buggy)
		if( posessed && (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0||
		    Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 ||
		    Input.GetButtonDown("A"))){
			shouldScare = !shouldScare;
		}

        //exit posession
		if (Input.GetButtonDown("B") && posessed){

            //re-enable player
			SkinnedMeshRenderer[] skins = player.GetComponentsInChildren<SkinnedMeshRenderer>();//turn on mesh renderer
			foreach(SkinnedMeshRenderer s in skins){
				s.enabled = true;
			}
			//player.GetComponentInChildren<MeshRenderer>().enabled = true;
            player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;//turn on control
            player.GetComponent<Rigidbody>().isKinematic = false;//unfix
			player.GetComponent<player>().control = true;
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
            else if(gameObject.tag == "plane"){
                gameObject.GetComponentInParent<plane>().deposess();
            }
		
		}

	}


}
