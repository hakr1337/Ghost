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
	Vector3 target;
	int fearLevel;
    bool exiting = false;
	bool first = true;
    bool idle;
    int state;
    AIPath[] pathPoints;
    public int[] pointCount;
    NavMeshAgent agent;
    Vector3 exit;
    Slider fearMeter; //REPLEACE WITH STATIC REFERENCE LATER BY MAKING PUBLIC
    Quaternion _lookRotation;
    Vector3 _direction;
    Vector3 viewTarget;
    CapsuleCollider cc;
    float idleTimer;
    float walkTimer;
    float totalTimer;
    float scaredTimer;
    float deathTimer;
    int health;
    public int maxHealth;
    Image healthBar;
	bool active; /////////////////NEW 
    bool scaredNow;
    bool isDead;

	Animator anim;
    spawnAI spawnController;
    spawnGlobal sg;
    int totalWaves;


    // Use this for initialization
    void Start() {
		
        cc = GetComponent<CapsuleCollider>();
        //spawnController = GameObject.Find("AI_spawn_point").GetComponent<spawnAI>(); 
        agent = GetComponent<NavMeshAgent>();
        target = GetComponent<Transform>().position;
		anim = GetComponent<Animator> ();
		Image[] bars = GetComponentsInChildren<Image> ();
		foreach(Image i in bars){
			if (i.gameObject.tag == "healthImage") {
				healthBar = i;
			}
		}
        sg = GameObject.Find("MetaSpawn").GetComponent<spawnGlobal>();
        viewTarget = point0;
        idle = true;
        state = 0;
        active = true;
        health = 4;
        maxHealth = 4;
        isDead = false;
        scaredNow = false;
		anim.SetBool ("Walk", true);
        
        deathTimer = 0;

        //GameObject[] tempPoints;
        //pointCount = new int[totalWaves];
        
        //pathPoints = new AIPath[totalWaves];
        //for (int m = 0; m < totalWaves; m++)
        //{
        //    string tag;
        //    tag = "AI_Path" + m;
        //    //find all AI path points and then put there transforms into an array
        //    tempPoints = GameObject.FindGameObjectsWithTag(tag);
            


        //    pointCount[m] = tempPoints.Length;
        //    pathPoints[m] = new AIPath(pointCount[m]);
            

        //    for (int i = 0; i < pointCount[m]; i++)
        //    {
        //        pathPoints[m].setPoint(i, tempPoints[i].GetComponent<Transform>().position);

        //    }
            
            
        //}
        
        
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
            //if (scaredNow)
            //    active = false;
            

		}

        if(scaredNow)
        {
            scaredTimer += Time.deltaTime;
            if(scaredTimer > 6)
            {
                
                
				anim.SetBool ("Scared", false);
				anim.SetBool ("Walk", false);
                anim.SetBool("Idle", false);
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
			
            deathTimer += Time.deltaTime;

            if (deathTimer > 3.05)
            {
                sg.patronWasKilled();
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

		if (Mathf.Abs(transform.position.x - target.x) < .1f && Mathf.Abs(transform.position.z - target.z) < .1f) {
			anim.SetBool ("Walk", false);
            anim.SetBool("Idle", true);
            idle = true;
            idleTimer += Time.deltaTime;
		}
        else
        {
			anim.SetBool ("Walk", true);
            anim.SetBool("Scared", false);
            anim.SetBool("Idle", false);
            walkTimer += Time.deltaTime;
        }

		if (scaredNow||idleTimer > 5 || first || walkTimer > 15) {
			
			first = false;
            scaredNow = false;
            int waveCount = sg.getWaveCount();
			float fstate = Random.Range (0, pointCount[waveCount]);

            state = (int)fstate;

            
            setTarget(pathPoints[waveCount].getPoint(state));
            setView(pathPoints[waveCount].getPoint(state));

            anim.SetBool("Walk", true);
            anim.SetBool("Scared", false);
            anim.SetBool("Idle", false);

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

		//float time = 0;
		anim.SetBool ("Scared", true);
		anim.SetBool ("Walk", false);
        anim.SetBool("Idle", false);

        idle = false;
		active = true;
        

		//while (time < 10) {
		//	time += Time.deltaTime;
		//}

		//idle = true;
		//active = true;
        
    }

    public bool isExiting() {
        return exiting;
    }

    public void setSpawnTag(int stag)
    {

        spawnController = GameObject.Find("AI_spawn_point"+stag+"").GetComponent<spawnAI>();
        totalWaves = 10;
        GameObject[] tempPoints;
        pointCount = new int[totalWaves];

        pathPoints = new AIPath[totalWaves];
        for (int m = 0; m < totalWaves; m++)
        {
            string tag;
            tag = stag+"AI_Path" + m;
            //find all AI path points and then put there transforms into an array
            try
            {
                tempPoints = GameObject.FindGameObjectsWithTag(tag);
            }

            catch
            {
                tag = "0AI_Path0";
                tempPoints = GameObject.FindGameObjectsWithTag(tag);
            }

            

            pointCount[m] = tempPoints.Length;
            pathPoints[m] = new AIPath(pointCount[m]);


            for (int i = 0; i < pointCount[m]; i++)
            {
                pathPoints[m].setPoint(i, tempPoints[i].GetComponent<Transform>().position);

            }


        }
    }

    public Vector3 getCenter()
    {
        return cc.center;
    }

     
}
