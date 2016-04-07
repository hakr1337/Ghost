using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class AIPath
{
    public Vector3[] path;
    public int v;

    public AIPath()
    {
        path = new Vector3[10];
        v = 0;
    }

    public AIPath(int pathSize)
    {
        path = new Vector3[pathSize];
        v = pathSize;
    }

    public void setPoint(int i,Vector3 point)
    {
        path[i] = point;
    }

    public Vector3 getPoint(int i)
    {
        return path[i%v];
    }
}


public class NavAgent : MonoBehaviour {

    
    public float RotationSpeed;
    public float walkDelay;

	Vector3 point0;
	public Vector3 target;
	int fearLevel;
    bool exiting = false;
	bool first = true;
    bool idle;
    int state;
    public AIPath pathPoints;
    public int pointCount;
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
    float deathTimer;
    int health;
    public int maxHealth;
    public Image healthBar;
	bool active; /////////////////NEW 
    bool scaredNow;
    bool isDead;
    bool dying;

	Animator anim;
    spawnGlobal sg;
    int totalWaves;
    public int type;

    string currentScare;


    // Use this for initialization
    void Start() {


        //spawnController = GameObject.Find("AI_spawn_point").GetComponent<spawnAI>(); 
        agent = GetComponent<NavMeshAgent>();
        //target = GameObject.Find("AINode9").GetComponent<Transform>().position;
		anim = GetComponent<Animator> ();
        Image[] bars = GetComponentsInChildren<Image>();
        foreach (Image i in bars)
        {
            if (i.gameObject.name == "Health")
            {
                healthBar = i;
            }
        }
        sg = GameObject.Find("MetaSpawn").GetComponent<spawnGlobal>();
        viewTarget = point0;
        idle = true;
        state = 0;
        active = true;
        health = maxHealth;
        isDead = false;
        scaredNow = false;
        dying = false;
		anim.SetBool ("Walk", true);
        currentScare = "none";
        deathTimer = 0;
        
        agent.avoidancePriority = Random.Range(1, 100);

        setSpawnTag();
        
        
    }

    // Update is called once per frame
    void Update() {


    


        if (idle )
        {
			
            idleWalk();

        }

        
		if(active){///////////////NEW
			

              agent.SetDestination(target);
              //changeView();
            //if (scaredNow)
            //    active = false;
            

		}

        if(scaredNow)
        {
            changeView();
            scaredTimer += Time.deltaTime;
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (scaredTimer > stateInfo.length || scaredTimer > 6)
            {
                
                
				anim.SetBool ("Scared", false);
				anim.SetBool ("Walk", false);
                anim.SetBool("Idle", false);
                //anim.SetBool("Running", true);
                //running = true;
                //set nav agent speed
                idle = true;
                active = true;
                scaredTimer = 0;
                idleWalk();
                agent.SetDestination(target);
                changeView();

            }
        }

        //if(runnning)
        //{
        //    checkProximity();
        //    if (idle)
        //        running = false;
        //}
        //this is shit, change later
   

        if(health < 1)
        {
            //DO DEATH STUFF HERE

			//GameObject bub = (GameObject)Resources.Load("Fear Bubble");///////////////NEW

			//Vector3 dropLoc = transform.position;

			//dropLoc.y = dropLoc.y + .5f;

			//Instantiate(bub, dropLoc, Quaternion.identity);///////////////NEW

			//this.gameObject.SetActive(false);///////////////NEW
			active = false;///////////////NEW
            idle = false;
            scaredNow = false;
            setTarget(getCenter());
            agent.SetDestination(target);

            
            anim.SetBool("Death", true);
            isDead = true;
            
        }

        if(isDead)
        {
            if(!dying)
            {
                sg.patronWasKilled(type);
                dying = true;
            }
            deathTimer += Time.deltaTime;
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (deathTimer > stateInfo.length)
            {
                
                Destroy(gameObject);
            }
        }
       


    }

    void idleWalk() {
		
        //state++;
        //state = state % 9;

        totalTimer += Time.deltaTime;
        active = true;
        //print ((transform.position.x - target.x) + " " + (transform.position.z - target.z));

        checkProximity();

		if (scaredNow||idleTimer > 5 || first || walkTimer > 15) {
			
			first = false;
            scaredNow = false;
            int waveCount = sg.getWaveCount();
			float fstate = Random.Range (0, pointCount);

            state = (int)fstate;

            
            setTarget(pathPoints.getPoint(state));
            setView(pathPoints.getPoint(state));

            anim.SetBool("Walk", true);
            anim.SetBool("Scared", false);
            anim.SetBool("Idle", false);

            idleTimer = 0;
            walkTimer = 0;
        }
    }

    void checkProximity()
    {
        if (Mathf.Abs(transform.position.x - target.x) < .1f && Mathf.Abs(transform.position.z - target.z) < .1f)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            idle = true;
            idleTimer += Time.deltaTime;
        }
        else
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Scared", false);
            anim.SetBool("Idle", false);
            walkTimer += Time.deltaTime;
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

    public void scared(int scareVal, string scareObject) {


        health -= scareVal;
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / (float)maxHealth;
            scaredNow = true;
            setCurrentScare(scareObject);
            //float time = 0;
            anim.SetBool("Scared", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", false);

            idle = false;
            active = true;

            changeView();
            //while (time < 10) {
            //	time += Time.deltaTime;
            //}

            //idle = true;
            //active = true;
        }

    }

    public bool isExiting() {
        return exiting;
    }

    public void setSpawnTag()
    {


        totalWaves = 10;
        GameObject[] tempPoints;
        pointCount = 0;

        pathPoints = new AIPath();
        
            string tag;

 
            tag = "0AI_Path1";
            tempPoints = GameObject.FindGameObjectsWithTag(tag);
            

            

            pointCount = tempPoints.Length;
            pathPoints = new AIPath(pointCount);


            for (int i = 0; i < pointCount; i++)
            {
                pathPoints.setPoint(i, tempPoints[i].GetComponent<Transform>().position);

            }


        
    }

    public string getCurrentScare()
    {
        return currentScare;
    }

    public void setCurrentScare(string newScare)
    {
        currentScare = newScare;
    }

    public Vector3 getCenter()
    {
        return transform.position;
    }

     
}
