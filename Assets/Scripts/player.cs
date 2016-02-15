﻿using UnityEngine;
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
    int headHash;
    //int stopHeadHash;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		score_text = GameObject.Find("Score").GetComponent<Text>();
        headHash = Animator.StringToHash("tossHead");
		Time.timeScale = 1;
        //stopHeadHash = Animator.StringToHash("stopHead");
    }
	
	// Update is called once per frame
	void Update () {
		float vert = Input.GetAxis("Vertical");
		float hori = Input.GetAxis("Horizontal");
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
			if (vert > 0) {
				transform.position = new Vector3 (transform.position.x + Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				inward = true;
                idleTimer = 0;
			} else if (vert < 0) {
				transform.position = new Vector3 (transform.position.x - Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				outward = true;
                idleTimer = 0;
            }

			//left and right
			if (hori > 0) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z - Time.deltaTime * speed);
				moving = true;
				right = true;
                idleTimer = 0;
            } else if (hori < 0) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z + Time.deltaTime * speed);
				left = true;
				moving = true;
                idleTimer = 0;
            }

            if(canFly)
            {
                if (flyCon < 0 && transform.position.y < 16.44348f)
                {
                    transform.position = new Vector3(transform.position.x,
						transform.position.y - (flyCon * flySpeed),
                                             transform.position.z);
                    idleTimer = 0;
                }
                if (flyCon > 0 && transform.position.y > 11.61123)
                {
                    transform.position = new Vector3(transform.position.x,
						transform.position.y - (flyCon * flySpeed),
                                             transform.position.z);
                    idleTimer = 0;
                }


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
		} else if (c.name == "moveCenterFromRight") {
			moveCenterFromRight = true;
		} else if (c.name == "moveRight") {
			moveRight = true;
		} else if (c.name == "moveLeft") {
			moveLeft = true;
		}else if(c.name == "moveUp"){
			moveUp = true;
		}else if(c.name == "moveDown"){
			moveDown = true;
		}
	}

	public void CollectFear(){
		score += 1;
		score_text.text = "Score: " + score;
	}


}
