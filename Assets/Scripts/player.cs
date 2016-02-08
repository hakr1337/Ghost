using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {

	public float speed = 1.3f;
	public bool control = true;
    public bool canFly = false;
    public float flySpeed = 0.5f;
	Animator anim;
	public int score = 0;
	Text score_text;
    public float flyCon;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		score_text = GameObject.Find("Score").GetComponent<Text>();
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


		//in and out
		if (control) {
			if (vert > 0) {
				transform.position = new Vector3 (transform.position.x + Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				inward = true;
			} else if (vert < 0) {
				transform.position = new Vector3 (transform.position.x - Time.deltaTime * speed,
			                                 transform.position.y, 
			                                 transform.position.z);
				moving = true;
				outward = true;
			}

			//left and right
			if (hori > 0) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z - Time.deltaTime * speed);
				moving = true;
				right = true;
			} else if (hori < 0) {
				transform.position = new Vector3 (transform.position.x,
			                                 transform.position.y, 
			                                 transform.position.z + Time.deltaTime * speed);
				left = true;
				moving = true;
			}

            if(canFly)
            {
                if(flyCon < 0 && transform.position.y < 16.44348f)
                    transform.position = new Vector3(transform.position.x,
                                             transform.position.y - (flyCon*flySpeed),
                                             transform.position.z);
                if (flyCon > 0 && transform.position.y > 11.61123)
                    transform.position = new Vector3(transform.position.x,
                                             transform.position.y - (flyCon * flySpeed),
                                             transform.position.z);


            }


		}
	}


	public void CollectFear(){
		score += 1;
		score_text.text = "Score: " + score;
	}
}
