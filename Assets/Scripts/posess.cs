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
		if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject.Find("pause").GetComponent<PauseScreen>().PausePress();
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

                    //start posession change to object being posessed
                   // c.GetComponent<Collider>().isTrigger = false;//turn off object being posessed's trigger
                    //c.GetComponent<Posessable>().posessed = true;//mark object as posessed
                    //c.GetComponentInChildren<ParticleSystem>().enableEmission = false;//turn off and clear particle system
                    //c.GetComponentInChildren<ParticleSystem>().Clear();

                    //start posession change to player
					SkinnedMeshRenderer[] skins = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();//turn off mesh renderer
					//this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
					foreach(SkinnedMeshRenderer s in skins){
						s.enabled = false;
					}

                    //bad variable use, could be cleaner
                    Scare sc = c.GetComponent<Scare>();
                    int sr = sc.scareRadius;

                    Transform rt = c.gameObject.transform.parent.transform.parent.FindChild("Circle");


                    switch (sr)
                    {
                        case 1:
                            rt.localScale = new Vector3(32.90985f, 66.6f, 32.90985f);
                            break;
                        case 2:
                            rt.localScale = new Vector3(74.35389f, 66.6f, 74.65389f);
                            break;
                        case 3:
                            rt.localScale = new Vector3(103.4047f, 66.6f, 103.4047f);
                            break;
                    }

                    rt.gameObject.GetComponent<MeshRenderer>().enabled = true;

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
