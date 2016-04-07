using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public float speed = 1.5f;
	public bool control = true;
    public bool canFly = false;
    public float flySpeed = 0.5f;
	Animator anim;
	public int score = 0;
	Text score_text;
    float flyCon;
    float idleTimer;
    bool isIdle;
    bool dead = false;
    int headHash;
    Light deathLight;
    string roomLocation;
    //reference for an array of camera positions in the camera script rooms correspond to 0,1,2 along bottom and 3,4,5 on top floor inorder
    int locationKey; 
    spawnGlobal spawn;
    //int stopHeadHash;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        spawn = GameObject.Find("MetaSpawn").GetComponent<spawnGlobal>();
        headHash = Animator.StringToHash("tossHead");
        deathLight = gameObject.GetComponentInChildren<Light>();
        deathLight.enabled = false;
		
        //stopHeadHash = Animator.StringToHash("stopHead");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		float vert = Input.GetAxis("Vertical");
		float hori = Input.GetAxis("Horizontal");
        float Dx = Input.GetAxis("DPadX");
        float Dy = Input.GetAxis("DPadY");
        bool moving = false;
        bool left = false;
        bool right = false;
        bool inward = false;
        bool outward = false;
        flyCon = Input.GetAxis("Fly");

        idleTimer += Time.deltaTime;

        if(idleTimer > 5)
        {
            anim.SetTrigger(headHash);

        }

		//in and out
		if (control) {
			if (vert > 0 && !dead) {
				transform.position = new Vector3 (transform.position.x + Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				inward = true;
                idleTimer = 0;
			} else if (vert < 0 && !dead) {
				transform.position = new Vector3 (transform.position.x - Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				outward = true;
                idleTimer = 0;
            }

			//left and right
			if (hori > 0 && transform.position.z > 7.3f && !dead) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z - Time.deltaTime * speed);
				moving = true;
				right = true;
                idleTimer = 0;
            } else if (hori < 0 && transform.position.z < 27.9f && !dead) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z + Time.deltaTime * speed);
				left = true;
				moving = true;
                idleTimer = 0;
            }

            if(canFly)
            {
                if (flyCon < 0 && transform.position.y < 17f && !dead)
                {
                    transform.position = new Vector3(transform.position.x,
						transform.position.y - (flyCon * flySpeed),
                                             transform.position.z);
                    idleTimer = 0;
                }
                if (flyCon > 0 && transform.position.y > 12.32f && !dead)
                {
                    transform.position = new Vector3(transform.position.x,
						transform.position.y - (flyCon * flySpeed),
                                             transform.position.z);
                    idleTimer = 0;
                }


            }

            if(Dx > 0 && !dead)
            {
                //full room scare
                if (spawn.canFullRoomScare())
                    spawn.fullRoomScare(roomLocation);
            }
            if (Dx < 0 && !dead)
            {
                //stop time
                if (spawn.canStopTime())
                    spawn.stopTimer();
            }
            if (Dy < 0 && !dead)
            {
                //speed up
                if (spawn.canSpeedUp())
                    spawn.speedPlayer(roomLocation);
            }
            if (Dy > 0 && !dead)
            {
                //refill power ups
                spawn.refillPowers();
            }


        }
	}

	public bool moveCenterFromLeft = false;
	public bool moveCenterFromRight = false;
	public bool moveLeft = false;
	public bool moveRight = false;
	public bool moveUp = false;
	public bool moveDown = false;


	void OnTriggerEnter(Collider c){
		if (c.name == "moveCenterFromLeft") {
			moveCenterFromLeft = true;
			moveLeft = false;
		} else if (c.name == "moveCenterFromRight") {
			moveCenterFromRight = true;
			moveRight = false;
		} else if (c.name == "moveRight") {
			moveRight = true;
			moveCenterFromRight = false;
		} else if (c.name == "moveLeft") {
			moveLeft = true;
			moveCenterFromLeft = false;
		}else if(c.name == "moveUp"){
			moveUp = true;
			moveDown = false;
		}else if(c.name == "moveDown"){
			moveDown = true;
			moveUp = false;
		}
	}

	public bool topLeft = false;
	public bool topRight = false;
	public bool bottomLeft = false;
	public bool bottomRight = false;
	public bool bottomCenter = false;
	public bool topCenter = false;

	void OnTriggerStay(Collider c){
		if (c.name == "BottomLeft") {
			bottomLeft = true;
			topLeft = false;
			topRight = false;
			bottomRight = false;
			bottomCenter = false;
			topCenter = false;
            roomLocation = c.name;
            locationKey = 0;

		} else if (c.name == "BottomRight") {
			bottomLeft = false;
			topLeft = false;
			topRight = false;
			bottomRight = true;
			bottomCenter = false;
			topCenter = false;
            roomLocation = c.name;
            locationKey = 2;
        } else if (c.name == "TopRight") {
			bottomLeft = false;
			topLeft = false;
			topRight = true;
			bottomRight = false;
			bottomCenter = false;
			topCenter = false;
            roomLocation = c.name;
            locationKey = 5;
        } else if (c.name == "TopLeft") {
			bottomLeft = false;
			topLeft = true;
			topRight = false;
			bottomRight = false;
			bottomCenter = false;
			topCenter = false;
            roomLocation = c.name;
            locationKey = 3;
        }
        else if(c.name == "BottomCenter"){
			bottomLeft = false;
			topLeft = false;
			topRight = false;
			bottomRight = false;
			bottomCenter = true;
			topCenter = false;
            roomLocation = c.name;
            locationKey = 1;
        }
        else if(c.name == "TopCenter"){
			bottomLeft = false;
			topLeft = false;
			topRight = false;
			bottomRight = false;
			bottomCenter = false;
			topCenter = true;
            roomLocation = c.name;
            locationKey = 4;
        }
	}

	public void CollectFear(){
		//score += 1;
		//score_text.text = "Score: " + score;
	}

    //function to find out what room the palyer is in
    public int currentLocation()
    {
        return locationKey;
    }

    public void killPlayer()
    {
        dead = true;
        anim.SetBool("die", true);
        GameObject.Find("Lights").gameObject.SetActive(false);
        deathLight.enabled = true;
    }


}
