using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public bool visionOn = false;

	Vector3 Zoom1;
	Vector3 Zoom2;
	Vector3 Left;
	Vector3 Center;
	Vector3 Right;

	GameObject player;
	bool paused;
	Vector3[] zoomStates;
	public int zoomState;

	GameObject p;
	Posessable[] possessables;
	GameObject[] scareObjects;


	player ps;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		zoomState = 0;
		ps = player.GetComponent<player> ();
		possessables = GameObject.FindObjectsOfType<Posessable>();
		paused = false;


		//Zoom1 = new Vector3 (-7.12f, 14.54f, player.transform.position.z);
		//Zoom2 = new Vector3 (-.87f, 14.54f, player.transform.position.z);
		//Zoom1 = new Vector3 (3.88f, player.transform.position.y , player.transform.position.z);
		Left = new Vector3(3.6f, transform.position.y, 24.47f);
		Center = new Vector3 (2.931f, transform.position.y, 17.5f);
		Right = new Vector3 (2.931f, transform.position.y, 11.3f);
		Zoom1 = new Vector3 (2.931f, 12.82f, 24.9f);
		Zoom2 = new Vector3 (-5.28f, 16.13f, 18.14f);
		zoomStates = new Vector3[2];
		zoomStates [0] = Zoom1;
		zoomStates [1] = Zoom2;
		//zoomStates [2] = Zoom3;

		scareObjects = GameObject.FindGameObjectsWithTag("posessable");
		foreach(GameObject o in scareObjects){
			//print(o.name);
			/*shaderGlow sg = o.GetComponent<shaderGlow>();
			if (sg != null)
				//	print (sg);
				sg.lightOff();	
		*/}
	}

	// Update is called once per frame
	void Update () {

		if (!paused) {

			bool inObject = false;
			//check if in player or in object
			foreach (Posessable f in possessables) {
				if (f.posessed) {
					p = f.gameObject;
					inObject = true;
				}
				if (!inObject) {
					p = player;
				}
			}
			//print (p.gameObject.name + "yy");


			if (Input.GetButtonDown ("Y") || Input.GetKeyDown (KeyCode.C)) {
				this.gameObject.GetComponentInChildren<MeshRenderer> ().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer> ().enabled;
				visionOn = !visionOn;

				//            GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;

				foreach (GameObject o in scareObjects) {

					if (visionOn) {
						shaderGlow sg = o.GetComponent<shaderGlow> ();
						if (sg != null)
							sg.lightOn ();
					} 

					if (!visionOn) {
						shaderGlow sg = o.GetComponent<shaderGlow> ();
						if (sg != null)
							sg.lightOff ();
					}
					//o.GetComponentInParent<Posessable>().lit = !o.GetComponentInParent<Posessable>().lit;
				}
			}

			if (Input.GetButtonDown ("RightStick") || Input.GetMouseButtonDown (2)) {
				zoomState++;



				zoomState = zoomState % 2;
				zoomStates [zoomState] = new Vector3 (zoomStates [zoomState].x, zoomStates [zoomState].y, zoomStates [zoomState].z);
				this.transform.position = zoomStates [zoomState];


				if (zoomState % 2 == 0) {
					if (ps.bottomCenter) {
						this.transform.position = Center;
					} else if (ps.topCenter) {
						this.transform.position = new Vector3 (Center.x, 16.355f, Center.z);
					} else if (ps.topLeft) {
						this.transform.position = new Vector3 (Left.x, 16.355f, Left.z);
					} else if (ps.topRight) {
						this.transform.position = new Vector3 (Right.x, 16.355f, Right.z);
					} else if (ps.bottomLeft) {
						this.transform.position = Left;
					} else if (ps.bottomRight) {
						this.transform.position = Right;
					}

				}


				//zoomState++;
			}
			//print (zoomState);
			switch (zoomState % 2) {
			case 0: //if full zoom 
				//print ("0");
				if (player.GetComponent<player> ().moveCenterFromLeft) {
					//print ("movefromkleft");
					if (transform.position.z > Center.z) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y,
							transform.position.z - Time.deltaTime * 5f);
					} else {
						player.GetComponent<player> ().moveCenterFromLeft = false;
					}
				} else if (player.GetComponent<player> ().moveRight) {
					if (transform.position.z > Right.z) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y,
							transform.position.z - Time.deltaTime * 5f);
					} else {
						player.GetComponent<player> ().moveRight = false;
					}
				} else if (player.GetComponent<player> ().moveCenterFromRight) {
					if (transform.position.z < Center.z) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y,
							transform.position.z + Time.deltaTime * 5f);
					} else {
						player.GetComponent<player> ().moveCenterFromRight = false;
					}
				} else if (player.GetComponent<player> ().moveLeft) {

					if (transform.position.z < Left.z) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y,
							transform.position.z + Time.deltaTime * 5f);
					} else {
						player.GetComponent<player> ().moveLeft = false;
					}
				}

				if (player.GetComponent<player> ().moveDown) {

					if (transform.position.y > 13.11f) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y - Time.deltaTime * 5f,
							transform.position.z);
					} else {
						player.GetComponent<player> ().moveDown = false;
					}
				} else if (player.GetComponent<player> ().moveUp) {

					if (transform.position.y < 16.355f) {//move right
						transform.position = new Vector3 (transform.position.x,
							transform.position.y + Time.deltaTime * 5f,
							transform.position.z);
					} else {
						player.GetComponent<player> ().moveUp = false;
					}
				}

				break;

			case 1:



				break;

			}
		} else {

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


	public void pause(bool p){
		paused = p;
	}
}
