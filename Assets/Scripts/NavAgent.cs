using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavAgent : MonoBehaviour {

    
    public float RotationSpeed;
    public float walkDelay;
	public float exitTime = 30;

	Vector3 point0;
	Vector3 target;
	int fearLevel;
    bool exiting = false;
	bool first = true;
    bool idle;
    int state;
    Vector3[] pathPoints;
    int pointCount;
    Vector3[] pathPoints2;
    int pointCount2;
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
    public int maxHealth;
    Image healthBar;
	bool active; /////////////////NEW 
    bool scaredNow;

	Animator anim;
    spawnAI spawnController;
    


    // Use this for initialization
    void Start() {

        spawnController = GameObject.Find("AI_spawn_point").GetComponent<spawnAI>(); ;
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
 


        //find all AI path points and then put there transforms into an array
        GameObject[] tempPoints = GameObject.FindGameObjectsWithTag("AI_Path");

        pointCount = tempPoints.Length;
        pathPoints = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            pathPoints[i] = tempPoints[i].GetComponent<Transform>().position;
        }

        //find all AI path points and then put there transforms into an array
        GameObject[] tempPoints2 = GameObject.FindGameObjectsWithTag("AI_Path2");

        pointCount2 = tempPoints.Length;
        pathPoints2 = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            pathPoints2[i] = tempPoints[i].GetComponent<Transform>().position;
        }
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
   

        if(health < 1)
        {
            //DO DEATH STUFF HERE

			GameObject bub = (GameObject)Resources.Load("Fear Bubble");///////////////NEW

			Vector3 dropLoc = transform.position;

			dropLoc.y = dropLoc.y + .5f;

			Instantiate(bub, dropLoc, Quaternion.identity);///////////////NEW

			//this.gameObject.SetActive(false);///////////////NEW
			active = false;///////////////NEW

            spawnController.patronWasKilled();

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
			float fstate = Random.Range (0, pointCount);

            state = (int)fstate;

            if (spawnController.getWaveCount() == 1)
            {
                setTarget(pathPoints[state]);
                setView(pathPoints[(state + 1) % pointCount]);
            }
            else
            {
                setTarget(pathPoints2[state]);
                setView(pathPoints2[(state + 1) % pointCount]);
            }

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

    public void scared(int scareVal) {


        health -= scareVal;

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