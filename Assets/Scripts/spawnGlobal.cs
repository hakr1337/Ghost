using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnGlobal : MonoBehaviour {

    // Use this for initialization
    int totalPatrons;
    int killedPatrons;
    int waveCount;
    public int totalWaves;
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
    float deathTimer;
    bool dead = false;
    bool timing;

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
    int currSpawn;


    Text timerText;
    Text waveText;
    Text scoreText;
    Text kidText;
    Text momText;
    Text dadText;
    GameOverScreen GO;
    GameOverScreen winScreen;
    Image flameHealth;

    Image Ready;
    Image Wave;
    Image waveNumber;
    Image readyNumber;

    Sprite[] num;
    Sprite[] numDesat;

    int[] waveEnemyCountKid;
    int[] waveEnemyCountMom;
    int[] waveEnemyCountDad;

	public AudioClip collectfearsound;
	private AudioSource source; 

    spawnAI ms;
    spawnAI ks;
    spawnAI ws;
    spawnAI bs;

    //variables for powerup tracking
    int roomScareCurrent;
    public int roomScareMax;
    int stopTimeCurrent;
    public int stopTimeMax;
    int speedCurrent;
    public int speedMax;
    public float stopTimeLength;
    float stopTimeWindow;
    public float speedUpPower;
    public float speedLength;
    float speedWindow;
    bool fast;
    public int floodTime;
    public int floodScare;
    public float speedMod;
    Image speedUI;
    Image roomScareUI;
    Image stopTimeUI;

    player Ghoul;
    Cam mainCam;

    void Start () {
        timerText = GameObject.Find("WaveTimeUI").GetComponent<Text>();
        kidText = GameObject.Find("GirlCount").GetComponent<Text>();
        momText = GameObject.Find("MomCount").GetComponent<Text>();
        dadText = GameObject.Find("DadCount").GetComponent<Text>();
        GO = GameObject.Find("gameover").GetComponent<GameOverScreen>();
        winScreen = GameObject.Find("WinScreen").GetComponent<GameOverScreen>();
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
        Ghoul = GameObject.Find("Player").GetComponent<player>();
        stopTimeUI = GameObject.Find("timePower").GetComponent<Image>();
        speedUI = GameObject.Find("speedPower").GetComponent<Image>();
        roomScareUI = GameObject.Find("scarePower").GetComponent<Image>();
        mainCam = GameObject.Find("Camera").GetComponent<Cam>();

        score = 0;
        waveCount = -1;
        totalWaves = 10;
        momWait = 0;
        dadWait = 0;
        timing = true;

        timer = -wavePrepTime;
        waveTimer = timer;
        kitchenTimer = timer;
        mainTimer = timer;
        windowTimer = timer;
        bathroomTimer = timer;
        currSpawn = 0;

        roomScareCurrent = 0;

        stopTimeCurrent = 0;

        speedCurrent = 0;

        speedUI.fillAmount = (float)speedCurrent / (float)speedMax;
        roomScareUI.fillAmount = (float)roomScareCurrent / (float)roomScareMax;
        stopTimeUI.fillAmount = (float)stopTimeCurrent / (float)stopTimeMax;

        fast = false;

        if (GameModeControl.mode == 2)
            Time.timeScale = speedMod;
        else
            Time.timeScale = 1;

        totalWaves = 10;

        populateEnemy(GameModeControl.mode);

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
        if (!dead)
        {
            timer += Time.deltaTime;
            if (timer > stopTimeWindow && !timing)
                startTimer();
            if (timer > speedWindow && fast)
                slowPlayer();
            if (timing)
            {
                waveTimer += Time.deltaTime;
                kitchenTimer += Time.deltaTime;
                mainTimer += Time.deltaTime;
                windowTimer += Time.deltaTime;
                bathroomTimer += Time.deltaTime;
            }
        }
        //prep timer



        if (waveTimer < 0)
        {
            timerText.text = "00";
            Sprite us = num[(int)(-waveTimer)];
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
            {
                if(curr > 0)
                    timerText.text = "0" + curr;
                else
                    timerText.text = "00";
            }
            flameHealth.fillAmount = 1 - (waveTimer / failTime);

        }

        if (waveCount > totalWaves && !dead)//set win condition wave
        {
            dead = true;
            winFunction();//replace with win function later for final scene
        }
        if (killedPatrons >= enemySpawnCount && waveTimer > 0 && !dead)
        {
			source.PlayOneShot(collectfearsound, .5f);
            spawnNextWave();
        }

        if (waveTimer >= failTime)
        {
            dead = true; 
            if (deathTimer == 0)
            {
                GameObject player = GameObject.Find("Player");
                player.GetComponent<player>().killPlayer();
                //re-enable player
                SkinnedMeshRenderer[] skins = player.GetComponentsInChildren<SkinnedMeshRenderer>();//turn on mesh renderer
                foreach (SkinnedMeshRenderer s in skins)
                {
                    s.enabled = true;
                }
                //player.GetComponentInChildren<MeshRenderer>().enabled = true;
                player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;//turn on control
                player.GetComponent<Rigidbody>().isKinematic = false;//unfix
                player.GetComponent<player>().control = true;
                player.GetComponent<CapsuleCollider>().enabled = true;//turn on collider
                player.GetComponent<posess>().one = false;//enable posession of another object

            }
            deathTimer += Time.deltaTime;
            AnimatorStateInfo state = GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (deathTimer > 4 && dead)
            {
                failFunction();
                waveTimer = 0;
                
            }
        }
        checkSpawns();
        

    }

    void checkSpawns()
    {
        if (totalPatrons < enemySpawnCount)
        {
            if (kitchenTimer >= kitchenRate )
            {
                if (waveCount >= kitchenStart)
                {
                    ks.genAI();
                    kidCount++;
                    kidText.text = "" + kidCount;
                    kitchenTimer = 0;
                    totalPatrons++;
                    checkMom(ks);
                    checkDad(ks);
                    currSpawn++;
                    
                }

            }

        }
        if (totalPatrons < enemySpawnCount)
        {
            if (mainTimer >= mainRate )
            {
                if (waveCount >= mainStart)
                {
                    ms.genAI();
                    kidCount++;
                    kidText.text = "" + kidCount;
                    mainTimer = 0;
                    totalPatrons++;
                    checkMom(ms);
                    checkDad(ms);
                    currSpawn++;
                }


            }
        }
        if (totalPatrons < enemySpawnCount)
        {
            if (windowTimer >= windowRate )
            {
                if (waveCount >= windowStart)
                {
                    ws.genAI();
                    kidCount++;
                    kidText.text = "" + kidCount;
                    windowTimer = 0;
                    totalPatrons++;
                    checkMom(ws);
                    checkDad(ws);
                    currSpawn++;
                }

            }
        }

        if (totalPatrons < enemySpawnCount)
        {
            if (bathroomTimer >= bathroomRate )
            {
                if (waveCount >= bathroomStart)
                {
                    bs.genAI();
                    kidCount++;
                    kidText.text = "" + kidCount;
                    windowTimer = 0;
                    totalPatrons++;
                    checkMom(bs);
                    checkDad(bs);
                    currSpawn = 0;
                }

            }
        }
    }

    void checkMom(spawnAI s)
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

    void checkDad(spawnAI s)
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
        {
            kidCount--;
            if (speedCurrent < speedMax)
            {
                speedCurrent++;
                //image fill
                speedUI.fillAmount = (float)speedCurrent / (float)speedMax;

                if(GameModeControl.mode == 1)
                {
                    stopTimeCurrent+= floodTime;
                    //image fill
                    stopTimeUI.fillAmount = (float)stopTimeCurrent / (float)stopTimeMax;

                    roomScareCurrent += floodScare;
                    //image fill
                    roomScareUI.fillAmount = (float)roomScareCurrent / (float)roomScareMax;
                }

            }
        }
        else if (type == 1)
        {
            momCount--;
            if (stopTimeCurrent < stopTimeMax)
            {
                stopTimeCurrent++;
                //image fill
                stopTimeUI.fillAmount = (float)stopTimeCurrent / (float)stopTimeMax;
            }
        }
        else if (type == 2)
        {
            dadCount--;
            if (roomScareCurrent < roomScareMax)
            {
                roomScareCurrent++;
                //image fill
                roomScareUI.fillAmount = (float)roomScareCurrent / (float)roomScareMax;
            }
        }
        kidText.text = "" + kidCount;
        momText.text = "" + momCount;
        dadText.text = "" + dadCount;
        //scoreText.text = "" + score;
    }
    void populateEnemy(int mode)
    {
        waveEnemyCountKid = new int[totalWaves];
        waveEnemyCountMom = new int[totalWaves];
        waveEnemyCountDad = new int[totalWaves];
        if (mode == 1)
        {
            waveEnemyCountKid[0] = 10;
            waveEnemyCountKid[1] = 20;
            waveEnemyCountKid[2] = 30;
            waveEnemyCountKid[3] = 35;
            waveEnemyCountKid[4] = 40;
            waveEnemyCountKid[5] = 45;
            waveEnemyCountKid[6] = 50;
            waveEnemyCountKid[7] = 55;
            waveEnemyCountKid[8] = 60;
            waveEnemyCountKid[9] = 65;
            momRatio = 0;
            dadRatio = 0;
            kitchenStart = 0;
            windowStart = 0;
            bathroomStart = 0;
        }
        else
        {
            waveEnemyCountKid[0] = 1;
            waveEnemyCountKid[1] = 3;
            waveEnemyCountKid[2] = 4;
            waveEnemyCountKid[3] = 5;
            waveEnemyCountKid[4] = 7;
            waveEnemyCountKid[5] = 9;
            waveEnemyCountKid[6] = 10;
            waveEnemyCountKid[7] = 11;
            waveEnemyCountKid[8] = 12;
            waveEnemyCountKid[9] = 13;
        }

        if (momRatio != 0)
        {
            for (int i = 0; i < totalWaves; i++)
            {
                waveEnemyCountMom[i] = waveEnemyCountKid[i] / momRatio;
                waveEnemyCountDad[i] = waveEnemyCountKid[i] / dadRatio;
            }
        }
    }
    void failFunction()
    {
        //spawnNextWave();
        GameObject.Find("UI").gameObject.SetActive(false);
        GO.Died();
    }

    void winFunction()
    {
        GameObject.Find("UI").gameObject.SetActive(false);
        winScreen.Died();
        
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
        currSpawn = 0;
       
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

    public void stopTimer()
    {
        timing = false;
        stopTimeWindow = timer + stopTimeLength;
        stopTimeCurrent = 0;
        stopTimeUI.fillAmount = (float)stopTimeCurrent / (float)stopTimeMax;
    }
    public void startTimer()
    {
        timing = true;
    }

    public bool canFullRoomScare()
    {
        return roomScareCurrent == roomScareMax;
    }
    public bool canStopTime()
    {
        return stopTimeCurrent == stopTimeMax;
    }
    public bool canSpeedUp()
    {
        return speedCurrent == speedMax;
    }

    //find all scare objects in the room ghoul kid is in and activate them
    public void fullRoomScare(string room)
    {
        GameObject[] roomScares = GameObject.FindGameObjectsWithTag(room);

        foreach(GameObject g in roomScares)
        {
            //stupid fix for the piano and lamp being different than all other objects
            if(g.transform.parent.name == "pianobench")
            {
                g.transform.parent.GetComponent<piano>().stupidPiano();
            }
            if (g.transform.parent.name == "lamp")
            {
                g.transform.parent.GetComponent<lamp>().stupidLamp();
            }
            g.GetComponent<Scare>().startScare();
        }
        roomScareCurrent = 0;
        roomScareUI.fillAmount = (float)roomScareCurrent / (float)roomScareMax;
    }
    //increase the players speed.
    public void speedPlayer()
    {
        Ghoul.speed = Ghoul.speed * speedUpPower;
        Ghoul.flySpeed = Ghoul.flySpeed * speedUpPower;
        mainCam.scrollSpeed = mainCam.scrollSpeed * speedUpPower;
        speedWindow = timer + speedLength;
        fast = true;
        speedCurrent = 0;
        speedUI.fillAmount = (float)speedCurrent / (float)speedMax;
    }
    //put players speed back to normal
    public void slowPlayer()
    {
        Ghoul.speed = Ghoul.speed / speedUpPower;
        Ghoul.flySpeed = Ghoul.flySpeed / speedUpPower;
        mainCam.scrollSpeed = mainCam.scrollSpeed / speedUpPower;
        fast = false;
        
    }

    public void refillPowers()
    {
        speedCurrent = speedMax;
        roomScareCurrent = roomScareMax;
        stopTimeCurrent = stopTimeMax;
        speedUI.fillAmount = (float)speedCurrent / (float)speedMax;
        roomScareUI.fillAmount = (float)roomScareCurrent / (float)roomScareMax;
        stopTimeUI.fillAmount = (float)stopTimeCurrent / (float)stopTimeMax;
    }
}
