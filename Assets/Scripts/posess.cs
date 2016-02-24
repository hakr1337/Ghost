using UnityEngine;
using System.Collections;

public class posess : MonoBehaviour {

	//used to make sure only one object glows and that is the possesable object
    public bool one = false;
	Camera cam;
    Animator anim;


    // Use this for initialization
    void Start () {
		cam = Camera.main;
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) {
            GameObject.Find("newPause").GetComponent<PauseScreen>().PausePress();
        }
	}



	IEnumerator OnTriggerStay(Collider c){
        
        //Possesable interactions on trigger
        if (c.GetComponent<Posessable>() != null) { //make sure item has posseable component


            if (one == false || c.GetComponent<Posessable>().lit == true) { //make sure only possesing one object that is glowing

				if(!cam.GetComponent<Cam>().visionOn){
	                //c.GetComponentInChildren<ParticleSystem>().enableEmission = true; //turn on particle emission
	                one = true; 
	                c.GetComponent<Posessable>().lit = true;//mark object as lit
                    c.GetComponentInParent<shaderGlow>().lightOn();
				}

				if ((Input.GetButtonDown("A") || Input.GetMouseButtonDown(0)) && c.GetComponent<Posessable>() != null) { //detect posses button (Q)

                    if (Camera.main.GetComponent<Cam>().visionOn) {//if vision on turn off
                        Camera.main.GetComponent<Cam>().turnOff();
                    }

                 
                    //start posession change to player
					SkinnedMeshRenderer[] skins = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();//turn off mesh renderer
					//this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
					foreach(SkinnedMeshRenderer s in skins){
						s.enabled = false;
					}

					gameObject.GetComponentInChildren<ParticleSystem> ().Pause ();
					gameObject.GetComponentInChildren<ParticleSystem> ().Clear();

					this.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;//turn off player control
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;//fix player position
					this.gameObject.GetComponent<player>().control = false;
                    this.gameObject.GetComponent<CapsuleCollider>().enabled = false;//turn off players collider

					yield return new WaitForSeconds(.2f);
					c.GetComponent<Posessable>().posessed = true;
				}
            }
        }
	}


    //called when exiting a trigger
    void OnTriggerExit(Collider c) {

        //for exiting posessible triggers
		if (c.GetComponent<Posessable>() != null  && !cam.GetComponent<Cam>().visionOn) {//if object is posessable and has particle system
            one = false;//allow possesion of another object
            c.GetComponent<Posessable>().lit = false; //mark this object as unlit
            //c.GetComponentInChildren<ParticleSystem>().enableEmission = false; //turn off and clear particle system
            //c.GetComponentInChildren<ParticleSystem>().Clear();
            shaderGlow sg = c.GetComponentInParent<shaderGlow>();
            if (sg != null)
                sg.lightOff();
        }
    }
}
