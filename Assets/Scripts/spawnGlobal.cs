using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnGlobal : MonoBehaviour {

    // Use this for initialization
    public int totalPatrons;
    public int killedPatrons;
    public int waveCount;
    public int totalWaves = 10;
    public int momRatio;
    public int dadRatio;
    int enemySpawnCount;
    int score;
    int momCount = 0;
    int dadCount = 0;
    int kidCount = 0;
    int momWait = 0;
    int dadWait = 0;
    public float failTime;
    public float wavePrepTime;
    float waveTimer;
    float timer;

    float kitchenTimer;
    float mainTimer;
    float windowTimer;
    float bathroomTimer;

    //enemy at each point will spawn every set number of seconds
    public float kitchenRate;
    public float mainRate;
    public float windowRate;
    public float bathroomRate;
    //wave for each spawn to start making enemies
    public int kitchenStart;
    public int mainStart;
    public int windowStart;
    public int bathroomStart;


    Text timerText;
    Text waveText;
    Text scoreText;
    Text kidText;
    Text momText;
    Text dadText;
    GameOverScreen GO;
    Image flameHealth;

    Image Ready;
    Image Wave;
    Image waveNumber;
    Image readyNumber;

    Sprite[] num;
    Sprite[] numDesat;

    public int[] waveEnemyCountKid;
    public int[] waveEnemyCountMom;
    public int[] waveEnemyCountDad;

	public AudioClip collectfearsound;
	private AudioSource source; 

    spawnAI ms;
    spawnAI ks;
    spawnAI ws;
    spawnAI bs;
    void Start () {
        timerText = GameObject.Find("WaveTimeUI").GetComponent<Text>();
        //waveText = GameObject.Find("WaveCountUI").GetComponent<Text>();
        //scoreText = GameObject.Find("Score").GetComponent<Text>();
        kidText = GameObject.Find("GirlCount").GetComponent<Text>();
        momText = GameObject.Find("MomCount").GetComponent<Text>();
        dadText = GameObject.Find("DadCount").GetComponent<Text>();
        GO = GameObject.Find("gameover").GetComponent<GameOverScreen>();
        flameHealth = GameObject.Find("SkullFlame").GetComponent<Image>();
        Ready = GameObject.Find("Ready").GetComponent<Image>();
        Wave = GameObject.Find("Wave").GetComponent<Image>();
        waveNumber = GameObject.Find("waveNumber").GetComponent<Image>();
        readyNumber = GameObject.Find("readyNumber").GetComponent<Image>();
        source = GetComponent<AudioSource>();
        ms = GameObject.Find("MainSpawn").GetComponentInChildren<spawnAI>();
        ks = GameObject.Find("KitchenSpawn").GetComponentInChildren<spawnAI>();
        ws = GameObject.Find("WindowSpawn").GetComponentInChildren<spawnAI>();
        bs = GameObject.Find("BathroomSpawn").GetComponentInChildren<spawnAI>();

        score = 0;
        waveCount = -1;
        totalWaves = 10;
        momWait = 0;
        dadWait = 0;
        

        timer = -wavePrepTime;
        waveTimer = timer;
        kitchenTimer = timer;
        mainTimer = timer;
        windowTimer = timer;
        bathroomTimer = timer;

        totalWaves = 10;
        waveEnemyCountKid = new int[totalWaves];
        waveEnemyCountMom = new int[totalWaves];
        waveEnemyCountDad = new int[totalWaves];
        waveEnemyCountKid[0] = 1;
        waveEnemyCountKid[1] = 3;
        waveEnemyCountKid[2] = 5;
        waveEnemyCountKid[3] = 4;
        waveEnemyCountKid[4] = 5;
        waveEnemyCountKid[5] = 4;
        waveEnemyCountKid[6] = 7;
        waveEnemyCountKid[7] = 9;
        waveEnemyCountKid[8] = 11;
        waveEnemyCountKid[9] = 14;

        for(int i = 0;  i < totalWaves; i++)
        {
            waveEnemyCountMom[i] = waveEnemyCountKid[i] / momRatio;
            waveEnemyCountDad[i] = waveEnemyCountKid[i] / dadRatio;
        }

        num = new Sprite[10];
        numDesat = new Sprite[10];

        for(int i = 0; i < 10; i++)
        {
            num[i] = Resources.Load<Sprite>("WaveUI/WaveUI/" + (i+1));
            numDesat[i] = Resources.Load<Sprite>("WaveUI/WaveUI/" + (i+1) + "desat");

            
        }

        //num[0] = Resources.Load<Sprite>("WaveUI/WaveUI/" + (1));
        //numDesat[0] = Resources.Load<Sprite>("WaveUI/WaveUI/" + (1) + "desat");



        spawnNextWave();

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        kitchenTimer += Time.deltaTime;
        mainTimer += Time.deltaTime;
        windowTimer += Time.deltaTime;
        bathroomTimer += Time.deltaTime;
        //prep timer



        if (timer < 0)
        {
            timerText.text = "00";
            Sprite us = num[(int)(-timer)];
            //Debug.Log(us.name);
            readyNumber.sprite = us;

        }
        else//timer on the wave
        {
            if (Wave.IsActive())
            {
                Wave.CrossFadeAlpha(0f, 0.2f, true);
                Ready.CrossFadeAlpha(0f, 0.2f, true);
                readyNumber.CrossFadeAlpha(0f, 0.2f, true);
                waveNumber.CrossFadeAlpha(0f, 0.2f, true);
            }


            int curr = (int)(failTime - waveTimer);
            if (curr > 9)
                timerText.text = "" + curr;
            else
                timerText.text = "0" + curr;
            flameHealth.fillAmount = 1 - (waveTimer / failTime);

        }

        if (waveCount > totalWaves)
        {
            failFunction();//replace with win function later for final scene
        }
        if (killedPatrons >= enemySpawnCount && waveTimer > 0)
        {
			source.PlayOneShot(collectfearsound, .5f);
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
                kidCount++;
                kidText.text = "" + kidCount;
                kitchenTimer = 0;
                totalPatrons++;
                checkMom(ks);
                checkDad(ks);

            }        
            
        }
        if (totalPatrons < enemySpawnCount)
        {
            if (mainTimer >= mainRate && waveCount >= mainStart)
            {
                ms.genAI();
                kidCount++;
                kidText.text = "" + kidCount;
                mainTimer = 0;
                totalPatrons++;
                checkMom(ms);
                checkDad(ms);


            }
        }
        if (totalPatrons < enemySpawnCount)
        {
            if (windowTimer >= windowRate && waveCount >= windowStart)
            {
                ws.genAI();
                kidCount++;
                kidText.text = "" + kidCount;
                windowTimer = 0;
                totalPatrons++;
                checkMom(ws);
                checkDad(ws);

            }
        }

        if (totalPatrons < enemySpawnCount)
        {
            if (bathroomTimer >= bathroomRate && waveCount >= bathroomStart)
            {
                bs.genAI();
                kidCount++;
                kidText.text = "" + kidCount;
                windowTimer = 0;
                totalPatrons++;
                checkMom(bs);
                checkDad(bs);

            }
        }

    }

    public void checkMom(spawnAI s)
    {
        momWait++;
        if (momWait >= momRatio && waveEnemyCountMom[waveCount] > momCount)
        {
            s.genMom();
            momWait -= momRatio;
            momCount++;
            momText.text = "" + momCount;
            totalPatrons++;
        }
    }

    public void checkDad(spawnAI s)
    {
        dadWait++;
        if (dadWait >= dadRatio && waveEnemyCountDad[waveCount] > dadCount)
        {
            s.genDad();
            dadWait -= dadRatio;
            dadCount++;
            dadText.text = "" + dadCount;
            totalPatrons++;
        }
    }

    public void patronWasKilled(int type)
    {
        score++;
        killedPatrons++;
        if (type == 0)
            kidCount--;
        else if (type == 1)
            momCount--;
        else if (type == 2)
            dadCount--;
        kidText.text = "" + kidCount;
        momText.text = "" + momCount;
        dadText.text = "" + dadCount;
        //scoreText.text = "" + score;
    }

    void failFunction()
    {
        //spawnNextWave();
        GO.Died();
    }

    public void spawnNextWave()
    {
        
        waveCount++;
        enemySpawnCount = waveEnemyCountKid[waveCount];
        enemySpawnCount += waveEnemyCountMom[waveCount];
        enemySpawnCount += waveEnemyCountDad[waveCount];
        killedPatrons = 0;
        totalPatrons = 0;
        momCount = 0;
        dadCount = 0;
        kidCount = 0;
        timer = -wavePrepTime;
        waveTimer = -wavePrepTime;
        kitchenTimer = timer;
        mainTimer = timer;
        windowTimer = timer;
        bathroomTimer = timer;
        //waveText.text = "Wave: " + (waveCount + 1) + "";
        flameHealth.fillAmount = 1;
       
        Wave.CrossFadeAlpha(1f, 0.2f, true);
        Ready.CrossFadeAlpha(1f, 0.2f, true);
        readyNumber.CrossFadeAlpha(1f, 0.2f, true);
        waveNumber.CrossFadeAlpha(1f, 0.2f, true);

        waveNumber.sprite = numDesat[waveCount];
        readyNumber.sprite = num[5];
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
