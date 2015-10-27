using UnityEngine;
using System.Collections;

public class posess : MonoBehaviour {

	//used to make sure only one object glows and that is the possesable object
    public bool one = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}



	void OnTriggerStay(Collider c){
        
        //Possesable interactions on trigger
        if (c.GetComponent<Posessable>() != null) { //make sure item has posseable component

            if (one == false || c.GetComponent<Posessable>().lit == true) { //make sure only possesing one object that is glowing

                c.GetComponentInChildren<ParticleSystem>().enableEmission = true; //turn on particle emission
                one = true; 
                c.GetComponent<Posessable>().lit = true;//mark object as lit


                if (Input.GetKeyDown(KeyCode.Q) && c.GetComponent<Posessable>() != null) { //detect posses button (Q)

                    if (Camera.main.GetComponent<cameraStuff>().visionOn) {//if vision on turn off
                        Camera.main.GetComponent<cameraStuff>().turnOff();
                    }

                    //start posession change to object being posessed
                    c.GetComponent<Collider>().isTrigger = false;//turn off object being posessed's trigger
                    c.GetComponent<Posessable>().posessed = true;//mark object as posessed
                    c.GetComponentInChildren<ParticleSystem>().enableEmission = false;//turn off and clear particle system
                    c.GetComponentInChildren<ParticleSystem>().Clear();

                    //start posession change to player
                    this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;//turn off mesh renderer
                    this.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;//turn off player control
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;//fix player position
                    this.gameObject.GetComponent<CapsuleCollider>().enabled = false;//turn off players collider
                }
            }
        }
	}


    //called when exiting a trigger
    void OnTriggerExit(Collider c) {

        //for exiting posessible triggers
        if (c.GetComponent<Posessable>() != null && c.GetComponentInChildren<ParticleSystem>() != null) {//if object is posessable and has particle system
            one = false;//allow possesion of another object
            c.GetComponent<Posessable>().lit = false; //mark this object as unlit
            c.GetComponentInChildren<ParticleSystem>().enableEmission = false; //turn off and clear particle system
            c.GetComponentInChildren<ParticleSystem>().Clear();
        }
    }
}
