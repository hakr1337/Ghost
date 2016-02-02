using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnAI : MonoBehaviour {
    public int identifier;
	GameObject[] AIs;
	GameObject AI;
    public int[] waveEnemyCount;
    public int enemySpawnCount;
	//Material[] colors;
	public float timer;
    public float waveTimer;
	bool timing;
    int index;
    Vector3 spawn;
    int count;
<<<<<<< HEAD
    int waveCount;
    int totalWaves;
    int patronCount;
=======
    public int waveCount;
    public int patronCount;
>>>>>>> master
    int killedPatrons;
    public float wavePrepTime;
    public float spawnRate;
    public float failTimer;
    Text timerText;
    Text waveText;
    Text scoreText;
    int score;
    GameOverScreen GO;
    Scare[] scareObjects;
    // Use this for initialization
    void Start () {
		AI = (GameObject)Resources.Load ("Patron");
        timerText = GameObject.Find("WaveTimeUI").GetComponent<Text>();
        waveText = GameObject.Find("WaveCountUI").GetComponent<Text>();
<<<<<<< HEAD
        scoreText = GameObject.Find("Score").GetComponent<Text>();

=======
        //AIs = new GameObject[35];
>>>>>>> master
        timing = true;
        index = 0;
        waveCount = -1;
        score = 0;
        
        spawn = GameObject.Find("AI_spawn_point").GetComponent<Transform>().position;


		int r = Random.Range (0, 3);

<<<<<<< HEAD
=======
        //t.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
        //t.GetComponent<NavAgent> ().setColor (r);

>>>>>>> master
        GameObject[] temp = GameObject.FindGameObjectsWithTag("posessable");

        int objCount = temp.Length;
        scareObjects = new Scare[objCount];
        for (int i = 0; i < objCount; i++)
        {
            scareObjects[i] = temp[i].GetComponentInChildren<Scare>();
        }

        GO = GameObject.Find("gameover").GetComponent<GameOverScreen>();

<<<<<<< HEAD
        totalWaves = 3;
        waveEnemyCount = new int[totalWaves];
        waveEnemyCount[0] = 1;
=======
        waveEnemyCount = new int[10];
>>>>>>> master
        waveEnemyCount[1] = 3;
        waveEnemyCount[2] = 5;

        timer = -wavePrepTime;
        waveTimer = timer;

        spawnNextWave();
        

    }
	
	// Update is called once per frame
	void Update () {

	    // all patrons for that wave were killed, start the next one
<<<<<<< HEAD

        if(waveCount > totalWaves)
        {
            failFunction();//replace with win function later for final scene
        }
=======
>>>>>>> master
        if(killedPatrons >= enemySpawnCount)
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


			if(patronCount < enemySpawnCount && waveCount >= identifier){
<<<<<<< HEAD
=======
               // for (int i = 0; i < count; i++)
                //{
                    GameObject temp = (GameObject)Instantiate(AI, spawn, Quaternion.identity);
					//int r = Random.Range (0, 3);
					//temp.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
					//temp.GetComponent<NavAgent> ().setColor (r);
>>>>>>> master

                    GameObject temp = (GameObject)Instantiate(AI, spawn, Quaternion.identity);
			
                patronCount++;
                timer = 0;

            }
            
		}

	}

    public void spawnNextWave()
    {
        waveCount++;
        enemySpawnCount = waveEnemyCount[waveCount];
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
        score++;
        scoreText.text = "Score: " + score;
    }

    public void failFunction()
    {
        
        GO.Died();
    }

    public int getTotalWaves()
    {
        return totalWaves;
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
