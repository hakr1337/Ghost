using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class AIPath
{
    public Vector3[] path;
<<<<<<< HEAD
    public int v;
=======
    
>>>>>>> origin/Max_Work

    public AIPath()
    {
        path = new Vector3[10];
<<<<<<< HEAD
        v = 0;
=======
       
>>>>>>> origin/Max_Work
    }

    public AIPath(int pathSize)
    {
        path = new Vector3[pathSize];
<<<<<<< HEAD
        v = pathSize;
=======
        
>>>>>>> origin/Max_Work
    }

    public void setPoint(int i,Vector3 point)
    {
        path[i] = point;
    }

    public Vector3 getPoint(int i)
    {
<<<<<<< HEAD
        return path[i%v];
=======
        return path[i];
>>>>>>> origin/Max_Work
    }
}


public class NavAgent : MonoBehaviour {

    
    public float RotationSpeed;
    public float walkDelay;

	Vector3 point0;
	Vector3 target;
	int fearLevel;
    bool exiting = false;
	bool first = true;
    bool idle;
    int state;
    public AIPath[] pathPoints;
    public int[] pointCount;
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
    int totalWaves;

    public Vector3[] testing;
    public GameObject[] test2;
    public string[] stest;
    


    // Use this for initialization
    void Start() {

        spawnController = GameObject.Find("AI_spawn_point").GetComponent<spawnAI>(); 
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
        //exit = GameObject.Find("exit").GetComponent<Transform>().position;
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
        totalWaves = spawnController.getTotalWaves();

        GameObject[] tempPoints;
        pointCount = new int[totalWaves];
        stest = new string[3];
        
        pathPoints = new AIPath[totalWaves];
        for (int m = 0; m < totalWaves; m++)
        {
            string tag;
            tag = "AI_Path" + m;
            stest[m] = tag;
<<<<<<< HEAD
            //find all AI path points and then put there transforms into an array
=======
            //find all AI path points and then put their transforms into an array
>>>>>>> origin/Max_Work
            tempPoints = GameObject.FindGameObjectsWithTag(tag);
            


            pointCount[m] = tempPoints.Length;
            pathPoints[m] = new AIPath(pointCount[m]);
            

            for (int i = 0; i < pointCount[m]; i++)
            {
                pathPoints[m].setPoint(i, tempPoints[i].GetComponent<Transform>().position);

            }
            
            
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

			//GameObject bub = (GameObject)Resources.Load("Fear Bubble");///////////////NEW

			//Vector3 dropLoc = transform.position;

			//dropLoc.y = dropLoc.y + .5f;

			//Instantiate(bub, dropLoc, Quaternion.identity);///////////////NEW

			active = false;///////////////NEW

            spawnController.patronWasKilled();

			Destroy (gameObject);
        }
       


    }

    void idleWalk() {
		

        totalTimer += Time.deltaTime;
        active = true;

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
            int waveCount = spawnController.getWaveCount();
			float fstate = Random.Range (0, pointCount[waveCount]);

            state = (int)fstate;


            setTarget(pathPoints[waveCount].getPoint(state));
            setView(pathPoints[waveCount].getPoint(state));
            
          

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
