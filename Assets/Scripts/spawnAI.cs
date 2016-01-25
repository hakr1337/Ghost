using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnAI : MonoBehaviour {
	GameObject[] AIs;
	GameObject AI;
	//Material[] colors;
	public float timer = 0;
    public float waveTimer = 0;
	bool timing;
    int index;
    Vector3 spawn;
    int count;
    public int waveCount;
    int patronCount;
    public int killedPatrons;
    public float wavePrepTime;
    public float spawnRate;
    public float failTimer;
    Text timerText;
    Text waveText;
    GameOverScreen GO;
    public Scare[] scareObjects;
    public GameObject[] temp;
    // Use this for initialization
    void Start () {
		AI = (GameObject)Resources.Load ("Patron");
        timerText = GameObject.Find("WaveTimeUI").GetComponent<Text>();
        waveText = GameObject.Find("WaveCountUI").GetComponent<Text>();
        AIs = new GameObject[35];
        timing = true;
        index = 0;
        waveCount = 0;
        spawn = GameObject.Find("AI_spawn_point").GetComponent<Transform>().position;
		//GameObject t = (GameObject)Instantiate(AI, spawn, Quaternion.identity);

		//colors = new Material[4];
		//colors[0] = (Material)Resources.Load ("Red");
		//colors[1] = (Material)Resources.Load ("Green");
		//colors[2] = (Material)Resources.Load ("Blue");
		//colors[3] = (Material)Resources.Load ("Yellow");

		int r = Random.Range (0, 3);

        //t.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
        //t.GetComponent<NavAgent> ().setColor (r);

        temp = GameObject.FindGameObjectsWithTag("posessable");

        int objCount = temp.Length;
        scareObjects = new Scare[objCount];
        for (int i = 0; i < objCount; i++)
        {
            scareObjects[i] = temp[i].GetComponentInChildren<Scare>();
        }

        GO = GameObject.Find("gameover").GetComponent<GameOverScreen>();

        spawnNextWave();

    }
	
	// Update is called once per frame
	void Update () {

	    // all patrons for that wave were killed, start the next one
        if(killedPatrons >= waveCount)
        {
            spawnNextWave();
        }

        if(waveTimer >= failTimer)
        {
            failFunction();
        }


		if (timing) {
			timer += Time.deltaTime;
            waveTimer += Time.deltaTime;
            //prep timer
            if (timer < 0)
                timerText.text = "Timer: " + (int)timer;
            else//timer on the wave
                timerText.text = "Timer: " + (int)(failTimer - waveTimer);
		}

		if(timer > spawnRate){


			if(patronCount < waveCount){
               // for (int i = 0; i < count; i++)
                //{
                    GameObject temp = (GameObject)Instantiate(AI, spawn, Quaternion.identity);
					//int r = Random.Range (0, 3);
					//temp.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
					//temp.GetComponent<NavAgent> ().setColor (r);

                    //AIs[index] = temp;
                    //index++;
                //}
                patronCount++;
				
			}
            timer = 0;
		}

	}

    public void spawnNextWave()
    {
        waveCount++;
        timer = -wavePrepTime;
        waveTimer = -wavePrepTime;
        killedPatrons = 0;
        waveText.text = "Wave: " + waveCount + "";
        patronCount = 0;
        resetScares();

    }

    public int getWaveCount()
    {
        return waveCount;
    }

    public void patronWasKilled()
    {
        killedPatrons++;
    }

    public void failFunction()
    {
        //spawnNextWave();
        GO.Died();
    }

    //reset ability to scare on each wave
    public void resetScares()
    {
        foreach(Scare s in scareObjects)
        {
            s.resetUsed();
        }
    }
}
