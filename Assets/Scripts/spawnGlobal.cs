using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnGlobal : MonoBehaviour {

    // Use this for initialization
    public int totalPatrons;
    public int killedPatrons;
    public int waveCount;
    public int totalWaves = 10;
    int enemySpawnCount;
    int score;
    public float failTime;
    public float wavePrepTime;
    float waveTimer;
    float timer;

    float kitchenTimer;
    float mainTimer;
    float windowTimer;

    //enemy at each point will spawn every set number of seconds
    public float kitchenRate;
    public float mainRate;
    public float windowRate;
    //wave for each spawn to start making enemies
    public int kitchenStart;
    public int mainStart;
    public int windowStart;


    Text timerText;
    Text waveText;
    Text scoreText;
    GameOverScreen GO;

    public int[] waveEnemyCount;

    spawnAI ms;
    spawnAI ks;
    spawnAI ws;
    void Start () {
        timerText = GameObject.Find("WaveTimeUI").GetComponent<Text>();
        waveText = GameObject.Find("WaveCountUI").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        GO = GameObject.Find("gameover").GetComponent<GameOverScreen>();

        ms = GameObject.Find("MainSpawn").GetComponentInChildren<spawnAI>();
        ks = GameObject.Find("KitchenSpawn").GetComponentInChildren<spawnAI>();
        ws = GameObject.Find("WindowSpawn").GetComponentInChildren<spawnAI>();

        score = 0;
        waveCount = -1;
        totalWaves = 10;

        

        timer = -wavePrepTime;
        waveTimer = timer;
        kitchenTimer = timer;
        mainTimer = timer;
        windowTimer = timer;

        totalWaves = 10;
        waveEnemyCount = new int[totalWaves];
        waveEnemyCount[0] = 1;
        waveEnemyCount[1] = 3;
        waveEnemyCount[2] = 5;
        waveEnemyCount[3] = 4;
        waveEnemyCount[4] = 5;
        waveEnemyCount[5] = 4;
        waveEnemyCount[6] = 7;
        waveEnemyCount[7] = 9;
        waveEnemyCount[8] = 11;
        waveEnemyCount[9] = 14;
        spawnNextWave();

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        kitchenTimer += Time.deltaTime;
        mainTimer += Time.deltaTime;
        windowTimer += Time.deltaTime;
        //prep timer
        if (timer < 0)
            timerText.text = "Timer: " + (int)timer;
        else//timer on the wave
            timerText.text = "Timer: " + (int)(failTime - waveTimer);

        if (waveCount > totalWaves)
        {
            failFunction();//replace with win function later for final scene
        }
        if (killedPatrons >= enemySpawnCount && waveTimer > 0)
        {
            spawnNextWave();
        }

        if (waveTimer >= failTime)
        {
            failFunction();
        }

        if (totalPatrons < enemySpawnCount)
        {
            if (kitchenTimer >= kitchenRate && waveCount >= kitchenStart)
            {
                ks.genAI();
                kitchenTimer = 0;
                totalPatrons++;

            }        
            
        }
        if (totalPatrons < enemySpawnCount)
        {
            if (mainTimer >= mainRate && waveCount >= mainStart)
            {
                ms.genAI();
                mainTimer = 0;
                totalPatrons++;

            }
        }
        if (totalPatrons < enemySpawnCount)
        {
            if (windowTimer >= windowRate && waveCount >= windowStart)
            {
                ws.genAI();
                windowTimer = 0;
                totalPatrons++;

            }
        }

    }


    public void patronWasKilled()
    {
        score++;
        killedPatrons++;
        scoreText.text = "" + score;
    }

    void failFunction()
    {
        //spawnNextWave();
        GO.Died();
    }

    public void spawnNextWave()
    {
        
        waveCount++;
        enemySpawnCount = waveEnemyCount[waveCount];
        killedPatrons = 0;
        totalPatrons = 0;
        timer = -wavePrepTime;
        waveTimer = -wavePrepTime;
        kitchenTimer = timer;
        mainTimer = timer;
        windowTimer = timer;
        waveText.text = "Wave: " + (waveCount + 1) + "";
    }

    public int getWaveCount()
    {
        return waveCount;
    }

    public int getTotalWaves()
    {
        return totalWaves;
    }
}
