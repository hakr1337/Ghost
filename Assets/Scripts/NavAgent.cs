﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavAgent : MonoBehaviour {

    
    public float RotationSpeed;
    public float walkDelay;
	public float exitTime = 60;

	Vector3 point0;
	Vector3 target;
	int fearLevel;
    bool exiting = false;
	bool first = true;
    bool idle;
    int state;
    Vector3[] pathPoints;
    NavMeshAgent agent;
    Vector3 exit;
    Slider fearMeter; //REPLEACE WITH STATIC REFERENCE LATER BY MAKING PUBLIC
    Quaternion _lookRotation;
    Vector3 _direction;
    Vector3 viewTarget;
    float idleTimer;
    float walkTimer;
    float totalTimer;
    float scaredTimer;
    public int agentColor;
    int health;
    int maxHealth;
    Image healthBar;
	bool active; /////////////////NEW 
    bool scaredNow;

	Animator anim;


    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
        target = GetComponent<Transform>().position;
		anim = GetComponent<Animator> ();
		Image[] bars = GetComponentsInChildren<Image> ();
		foreach(Image i in bars){
			if (i.gameObject.tag == "healthImage") {
				healthBar = i;
			}
		}
        //healthBar = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        //fearLevel = 0;
        exit = GameObject.Find("exit").GetComponent<Transform>().position;
        //fearMeter = GameObject.Find("FearSlider").GetComponent<Slider>();
        viewTarget = point0;
        idle = true;
        state = 0;
        active = true;
        health = 4;
        maxHealth = 4;
        //agentColor = 3;
        scaredNow = false;
		anim.SetBool ("Walk", true);
        pathPoints = new Vector3[9];
        pathPoints[0] = GameObject.Find("AINode1").GetComponent<Transform>().position;
        pathPoints[1] = GameObject.Find("AINode2").GetComponent<Transform>().position;
        pathPoints[2] = GameObject.Find("AINode3").GetComponent<Transform>().position;
        pathPoints[3] = GameObject.Find("AINode4").GetComponent<Transform>().position;
		pathPoints[4] = GameObject.Find("AINode5").GetComponent<Transform>().position;
		pathPoints[5] = GameObject.Find("AINode6").GetComponent<Transform>().position;
		pathPoints[6] = GameObject.Find("AINode7").GetComponent<Transform>().position;
		pathPoints[7] = GameObject.Find("AINode8").GetComponent<Transform>().position;
		pathPoints[8] = GameObject.Find("AINode9").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update() {


    

        if (idle )
        {
			
            idleWalk();

        }

        
		if(active){///////////////NEW
			

                agent.SetDestination(target);
                changeView();
            if (scaredNow)
                active = false;
            

		}

        if(scaredNow)
        {
            scaredTimer += Time.deltaTime;
            if(scaredTimer > 5)
            {
                scaredNow = false;
				anim.SetBool ("Scared", false);
				anim.SetBool ("Walk", false);
                idle = true;
                active = true;
                scaredTimer = 0;
                idleWalk();
                agent.SetDestination(target);
                changeView();

            }
        }
        //this is shit, change later
    

		if (totalTimer > exitTime)
        {
            agent.SetDestination(exit);
            changeView();
            active = false;
        }

        if(health < 1)
        {
            //DO DEATH STUFF HERE

			GameObject bub = (GameObject)Resources.Load("Fear Bubble");///////////////NEW

			Vector3 dropLoc = transform.position;

			dropLoc.y = dropLoc.y + .5f;

			Instantiate(bub, dropLoc, Quaternion.identity);///////////////NEW

			//this.gameObject.SetActive(false);///////////////NEW
			active = false;///////////////NEW

			Destroy (gameObject);
        }
       


    }

    void idleWalk() {
		
        //state++;
        //state = state % 9;

        totalTimer += Time.deltaTime;
        active = true;
		//print ((transform.position.x - target.x) + " " + (transform.position.z - target.z));

		if (Mathf.Abs(transform.position.x - target.x) < .001f && Mathf.Abs(transform.position.z - target.z) < .001f) {
			scaredNow = false;
			anim.SetBool ("Walk", false);
            idleTimer += Time.deltaTime;
		}
        else
        {
			anim.SetBool ("Walk", true);
            walkTimer += Time.deltaTime;
        }

		if (idleTimer > 5 || first || walkTimer > 15) {
			
			first = false;
			float fstate = Random.Range (0, 8);

            state = (int)fstate;

			setTarget (pathPoints [state]);
			setView (pathPoints [(state + 1) % 9]);

			idleTimer = 0;
            walkTimer = 0;
        }
    }

    void changeView() {
		
        _direction = (viewTarget - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    public void setView(Vector3 newView) {
		//anim.SetBool ("Walk", true);
        viewTarget = newView;
    }


    public void setTarget(Vector3 newTarget) {
		
        target = newTarget;
    }

    public void setColor(int newColor)
    {
        agentColor = newColor;
    }

    public void scared(int scareColor) {

        if (scareColor == agentColor)
        {
            health -= 3;
        }
        else
        {
            health -= 1;
        }
			

        healthBar.fillAmount = (float)health / (float)maxHealth;
        scaredNow = true;

		float time = 0;
		anim.SetBool ("Scared", true);
		anim.SetBool ("Walk", false);

		idle = false;
		active = false;

		while (time < 10) {
			time += Time.deltaTime;
		}

		//idle = true;
		//active = true;
        
    }

    public bool isExiting() {
        return exiting;
    }
}