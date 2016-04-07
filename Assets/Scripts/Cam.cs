using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public bool visionOn = false;

	Vector3 Zoom1;
	Vector3 Zoom2;
	Vector3 Left;
	Vector3 Center;
	Vector3 Right;
    Vector3 UpperLeft;
    Vector3 UpperCenter;
    Vector3 UpperRight;

    GameObject player;
	bool paused;
    bool canZoom;
    public bool limitCamera;
	Vector3[] zoomStates;
    Vector3[] rooms;
    public int zoomState;

	GameObject p;
	Posessable[] possessables;
	shaderGlow[] scareObjects;
    public float scrollSpeed;
    float zoomCool;
    float timer;
    float zoomWindow;
    int camLocation; //corresponding to the locationKey in the player script
    int playerLoc;
    bool movingCamera;
    float movePercentage;

	player ps;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		zoomState = 0;
		ps = player.GetComponent<player> ();
		possessables = GameObject.FindObjectsOfType<Posessable>();
		paused = false;
        scrollSpeed = 17;

        Left = new Vector3(3.78f, transform.position.y, 24.9f);
        Center = new Vector3(3.78f, transform.position.y, 17.71f);
        Right = new Vector3(3.78f, transform.position.y, 11.3f);
        UpperLeft = new Vector3(Left.x, 16.355f, Left.z);
        UpperCenter = new Vector3(Center.x, 16.355f, Center.z);
        UpperRight = new Vector3(Right.x, 16.355f, Right.z);
        Zoom1 = new Vector3 (2.931f, 12.82f, 24.9f);
		Zoom2 = new Vector3 (-5.28f, 16.13f, 18.14f);
		zoomStates = new Vector3[2];
		zoomStates [0] = Zoom1;
		zoomStates [1] = Zoom2;
        //zoomStates [2] = Zoom3;
        rooms = new Vector3[6];
        rooms[0] = Left;
        rooms[1] = Center;
        rooms[2] = Right;
        rooms[3] = UpperLeft;
        rooms[4] = UpperCenter;
        rooms[5] = UpperRight;
        canZoom = true; //bool for if player can zoom out the camera
        timer = 0;
        camLocation = 0;


		scareObjects = GameObject.FindObjectsOfType<shaderGlow>();

	}

	// Update is called once per frame
	void FixedUpdate () {

        //enforce a time limit and cooldown on the zoomed out menu
        if(!canZoom && limitCamera)
        {
            timer += Time.deltaTime;

            if(timer > zoomWindow)
            {
                if (zoomState == 1)
                    zoomState--;
                zoomIn();
            }

            if (timer > zoomCool)
                canZoom = true;
        }

        //new code to move camera consistently and smoothly
        //checks to see if player is in different room than camera
        playerLoc = player.GetComponent<player>().currentLocation();
        if (zoomState == 0 && camLocation != playerLoc)
        {
            
            camLocation = playerLoc;
            movingCamera = true;
            movePercentage = 0;
        }
        //moves camera to new room if required
        if(movingCamera)
        {
            movePercentage += 0.005f;
            if (movePercentage > 1)
            {
                transform.position = Vector3.Lerp(transform.position, rooms[camLocation], 1);
                movingCamera = false;
            }
            else
                transform.position = Vector3.Lerp(transform.position, rooms[camLocation], movePercentage);
        }

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

				foreach (shaderGlow sg in scareObjects) {

					if (visionOn) {
						
						if (sg != null) {
							sg.lightOn ();
							//sg.useNormal = false;
						}

					

						//GetComponent<Camera> ().cullingMask = (1<<9)|(1<<10)|(1<<12)|(1<<13)|(1<<15)|(1<<16);
							
					} 

					if (!visionOn) {
						
						if (sg != null) {
							sg.lightOff ();
							//sg.useNormal = true;
						}
					}
					//o.GetComponentInParent<Posessable>().lit = !o.GetComponentInParent<Posessable>().lit;
				}
			}

			if ((Input.GetButtonDown ("RightStick") || Input.GetMouseButtonDown (2))) {

                //player zooms out the camera
                if (canZoom && zoomState == 0)
                {
                    zoomState++;
                    if (limitCamera)
                    {
                        zoomCool = timer + 20;
                        zoomWindow = timer + 4;
                        canZoom = false;
                    }
                }
                else if (zoomState == 1)
                    zoomState--;

				this.transform.position = zoomStates [zoomState];

				if (zoomState  == 0) {
                    zoomIn();

				}

			}
			
		} else {
			pauseMovement ();
		}

	}

    void zoomIn()
    {
        if (ps.bottomCenter)
        {
            this.transform.position = Center;
        }
        else if (ps.topCenter)
        {
            this.transform.position = UpperCenter;
        }
        else if (ps.topLeft)
        {
            this.transform.position = UpperLeft;
        }
        else if (ps.topRight)
        {
            this.transform.position = UpperRight;
        }
        else if (ps.bottomLeft)
        {
            this.transform.position = Left;
        }
        else if (ps.bottomRight)
        {
            this.transform.position = Right;
        }
    }

	//turn off ghost vision
	public void turnOff(){

		this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
		visionOn = !visionOn;

		//GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;

		foreach(shaderGlow sg in scareObjects){
			
			if (sg != null)
				sg.lightOff();
		}
	}


	public void pause(bool p){
		//paused = p;
		//if (p) {
		//	if (ps.bottomCenter) {
		//		start = Center;
		//		cb = true;
		//	} else if (ps.topCenter) {
		//		start = new Vector3 (Center.x, 16.355f, Center.z);
		//		ct = true;
		//	} else if (ps.topLeft) {
		//		start = new Vector3 (Left.x, 16.355f, Left.z);
		//		lt = true;
		//	} else if (ps.topRight) {
		//		start = new Vector3 (Right.x, 16.355f, Right.z);
		//		rt = true;
		//	} else if (ps.bottomLeft) {
		//		start = Left;
		//		lb = true;
		//	} else if (ps.bottomRight) {
		//		start = Right;
		//		rb = true;
		//	}
		//} else {
		//	this.transform.position = start;
		//	lt = false;
		//	lb = false;
		//	ct = false;
		//	rt = false;
		//	rb = false;
		//	cb = false;
		//}
	}

	public bool lt = false;
	public bool lb = false;
	public bool rt = false;
	public bool rb = false;
	public bool ct = false;
	public bool cb = false;

	Vector3 start;
	void pauseMovement(){

		//if (lb) {
		//	if (transform.position.y < 16.355f) {//move up
		//		transform.position = new Vector3 (transform.position.x,
		//			transform.position.y + .02f,
		//			transform.position.z);
		//	} else {
		//		lb = false;
		//		lt = true;
		//	}
		//}
		//if(ct||lt){
		//	if (transform.position.z > Right.z) {//move right
		//		transform.position = new Vector3 (transform.position.x,
		//			transform.position.y,
		//			transform.position.z - .02f);
		//	} else {
		//		ct = false;
		//		lt = false;
		//		rt = true;
		//	}
		//}
		//if(rt){
		//	if (transform.position.y > 13.11f) {//move down
		//		transform.position = new Vector3 (transform.position.x,
		//			transform.position.y - .02f,
		//			transform.position.z);
		//	} else {
		//		rt = false;
		//		rb = true;
		//	}
		//}
		//if(rb||cb){
		//	if (transform.position.z < Left.z) {//move left
		//		transform.position = new Vector3 (transform.position.x,
		//			transform.position.y,
		//			transform.position.z + .02f);
		//	} else {
		//		rb = false;
		//		cb = false;
		//		lb = true;
		//	}
		//}
	}
}
