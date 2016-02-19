using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public bool visionOn = false;

	Vector3 Zoom1;
	Vector3 Left;
	Vector3 Center;
	Vector3 Right;

	GameObject player;

	Vector3[] zoomStates;
	public int zoomState;

	GameObject p;
	Posessable[] possessables;
	GameObject[] scareObjects;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		zoomState = 3;

		possessables = GameObject.FindObjectsOfType<Posessable>();

		//Zoom1 = new Vector3 (-7.12f, 14.54f, player.transform.position.z);
		//Zoom2 = new Vector3 (-.87f, 14.54f, player.transform.position.z);
		//Zoom1 = new Vector3 (3.88f, player.transform.position.y , player.transform.position.z);
		Left = new Vector3(3.6f, transform.position.y, 24.47f);
		Center = new Vector3 (3.36f, transform.position.y, 17.5f);
		Right = new Vector3 (3.36f, transform.position.y, 11.3f);
		Zoom1 = new Vector3 (3.88f, 12.82f, 24.9f);
		//Zoom2 = new Vector3 ();
		zoomStates = new Vector3[3];
		zoomStates [0] = Zoom1;
		//zoomStates [1] = Zoom2;
		//zoomStates [2] = Zoom3;

		scareObjects = GameObject.FindGameObjectsWithTag("posessable");
		foreach(GameObject o in scareObjects){
            //print(o.name);
            shaderGlow sg = o.GetComponent<shaderGlow>();
			if (sg != null)
			//	print (sg);
                sg.lightOff();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool inObject = false;
		//check if in player or in object
		foreach (Posessable f in possessables) {
			if (f.posessed) {
				p = f.gameObject;
				inObject = true;
			}
			if(!inObject){
				p = player;
			}
		}
		//print (p.gameObject.name + "yy");


		if (Input.GetButtonDown("Y") || Input.GetKeyDown(KeyCode.C)) {
			this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
			visionOn = !visionOn;
			
			//            GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;
			
			foreach (GameObject o in scareObjects) {

				if (visionOn) {
                    shaderGlow sg = o.GetComponent<shaderGlow>();
                    if (sg != null)
                        sg.lightOn();
                } 

				if(!visionOn){
                    shaderGlow sg = o.GetComponent<shaderGlow>();
                    if (sg != null)
                        sg.lightOff();
                }
				//o.GetComponentInParent<Posessable>().lit = !o.GetComponentInParent<Posessable>().lit;
			}
		}

		if(Input.GetButtonDown("RightStick") || Input.GetKeyDown(KeyCode.Z)){
			//zoomState++;



			zoomState = zoomState % 1;

			zoomStates[zoomState] = new Vector3(zoomStates[zoomState].x, zoomStates[zoomState].y, player.transform.position.z);
			this.transform.position = zoomStates[zoomState];
			zoomState++;
		}
		//print (zoomState);
		switch(zoomState%1){
			case 0: //if full zoom 
				//print ("0");
			if(player.GetComponent<player>().moveCenterFromLeft){
				//print ("movefromkleft");
				if (transform.position.z > Center.z) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y,
						transform.position.z - Time.deltaTime * 5f);
				} else {
					player.GetComponent<player> ().moveCenterFromLeft = false;
				}
			}else if(player.GetComponent<player>().moveRight){
				if (transform.position.z > Right.z) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y,
						transform.position.z - Time.deltaTime * 5f);
				} else {
					player.GetComponent<player> ().moveRight = false;
				}
			}else if(player.GetComponent<player>().moveCenterFromRight){
				if (transform.position.z < Center.z) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y,
						transform.position.z + Time.deltaTime * 5f);
				} else {
					player.GetComponent<player> ().moveCenterFromRight = false;
				}
			}else if(player.GetComponent<player>().moveLeft){

				if (transform.position.z < Left.z) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y,
						transform.position.z + Time.deltaTime * 5f);
				} else {
					player.GetComponent<player> ().moveLeft = false;
				}
			}else if(player.GetComponent<player>().moveDown){

				if (transform.position.y > 13.11f) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y - Time.deltaTime * 5f,
						transform.position.z );
				} else {
					player.GetComponent<player> ().moveDown = false;
				}
			}
			else if(player.GetComponent<player>().moveUp){

				if (transform.position.y < 16.355f) {//move right
					transform.position = new Vector3 (transform.position.x,
						transform.position.y + Time.deltaTime * 5f,
						transform.position.z );
				} else {
					player.GetComponent<player> ().moveUp = false;
				}
			}

				break;
			/*case 1: //no zoom
			//print (transform.position.z - p.transform.position.z);
				if(transform.position.z - p.transform.position.z > 1f){//move right
					//print ("right");
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y,
					                                 transform.position.z - Time.deltaTime * 1.3f);
				}else if(transform.position.z - p.transform.position.z < -1f){ //move left
					
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y,
				                                 transform.position.z + Time.deltaTime * 1.3f);
				//print (transform.position);
				}

				if(transform.position.x - p.transform.position.x < -13.5f){//move in
					transform.position = new Vector3(transform.position.x + Time.deltaTime * 1.3f,
					                                 transform.position.y,
					                                 transform.position.z);
				}else if(transform.position.x - p.transform.position.x > -12.5f){//move out
					transform.position = new Vector3(transform.position.x - Time.deltaTime * 1.3f,
					                                 transform.position.y,
					                                 transform.position.z);
				}

				if(transform.position.y - p.transform.position.y < .498f){//move up
					if(p.name == "Player"){
						p.GetComponent<player>().speed = 4;
					}
					
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y + Time.deltaTime * 1.3f,
					                                 transform.position.z);
				}else if(transform.position.y - p.transform.position.y > 2){//move down
					
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y - Time.deltaTime * 1.3f,
					                                 transform.position.z);
				}else{
					if(p.name == "Player"){
						p.GetComponent<player>().speed = 1.3f;
					}
				}
				break;
			/*case 2: //half zoom
				//print ("2");
				if(transform.position.z - p.transform.position.z > 1f){//move right
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y,
					                                 transform.position.z - Time.deltaTime * 1.7f);
				}else if(transform.position.z - p.transform.position.z < -1f){ //move left
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y,
					                                 transform.position.z + Time.deltaTime * 1.7f);
				}
				if(transform.position.x - p.transform.position.x < -7.3f){//move in
					transform.position = new Vector3(transform.position.x + Time.deltaTime * 1.7f,
					                                 transform.position.y,
					                                 transform.position.z);
				}else if(transform.position.x - p.transform.position.x > -3.5f){//move out
					transform.position = new Vector3(transform.position.x - Time.deltaTime * 1.7f,
					                                 transform.position.y,
					                                 transform.position.z);
				}
				if(transform.position.y - p.transform.position.y < .498f){//move up
					if(p.name == "Player"){
						p.GetComponent<player>().speed = 4;
					}
					//print(transform.position.y - p.transform.position.y);
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y + Time.deltaTime * 1.3f,
					                                 transform.position.z);
				}else if(transform.position.y - p.transform.position.y > 2){//move down
					
					transform.position = new Vector3(transform.position.x,
					                                 transform.position.y - Time.deltaTime * 1.3f,
					                                 transform.position.z);
				}else{
					if(p.name == "Player"){
						p.GetComponent<player>().speed = 1.3f;
					}
				}
				break;*/

		}

	}



	//turn off ghost vision
	public void turnOff(){
		
		this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
		visionOn = !visionOn;
		
		//GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;
		
		foreach(GameObject o in scareObjects){
            shaderGlow sg = o.GetComponent<shaderGlow>();
            if (sg != null)
                sg.lightOff();
        }
	}
}
