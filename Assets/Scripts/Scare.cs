using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

    // Use this for initialization
    public int scareVal;
    private Transform target;
    public float timer;
    float animTimer;
    public float usedTime;
    bool timing;
    bool moving;
    bool used;
    bool usedWindow;
    bool cooldownBool;
    float cooldown;
    float coolWindow;
    float exitWindow;
    public int cooldownTime = 10;

    float timer2;
    bool timing2;
    public bool upstairs;

    Posessable posessScript;
    GameObject[] people;
    public int scareRadius;

    int scareHash;
    int reverseHash;
    int idleHash;

    Animator anim;
    ParticleSystem parts;
    AudioSource scareSound;

    bool started;
    bool reverse;
    bool playing;
    public bool scaring;
    bool stayIn;
    bool continueScare;
    bool scared;
    string scareName;

    public bool hasParticle;
    AnimatorStateInfo stateInfo;

    //variable when scare tirggered
    //use to change the shader glow to gray and then blue 
    //again when scare is available
    bool change;

    void Start() {
        used = false;
        target = GameObject.Find("Target").GetComponent<Transform>();
        //parts =  GetComponentInChildren<ParticleSystem>();
        posessScript = this.gameObject.GetComponent<Posessable>();
        scareSound = this.gameObject.GetComponent<AudioSource>();
        usedWindow = false;
        cooldown = cooldownTime;
        cooldownBool = false;
        scareName = this.transform.parent.name;
        anim = GetComponentInParent<Animator>();
        animTimer = 0;


        playing = false;
        started = false;
        reverse = false;

        //bool for handeling the glow color change
        change = false;

        //bool for determining if the object is currently scaring
        scaring = false;
        stayIn = true;
        continueScare = true;

        


    }

    // Update is called once per frame
    void Update() {

        if(hasParticle)
        {
            //Transform t = this.gameObject.transform.parent.parent.gameObject.GetComponentInChildren<ParticleSystem>().transform;
            //Debug.Log("Found: " + t.name);
            parts = this.gameObject.transform.parent.parent.gameObject.GetComponentInChildren<ParticleSystem>();
            if (parts != null)
            {
                parts.gameObject.SetActive(false);
            }

            hasParticle = false;
        }
        timer += Time.deltaTime;
        cooldown += Time.deltaTime;
        if (cooldownBool && (timer > coolWindow))//allow window so one object can scare multiple people at once, could be cleaner
            setCooldown();
        //changes glow color back to blue when cooldown is done
        if (cooldown > cooldownTime && change)
        {
            shaderGlow sg = gameObject.transform.parent.GetComponent<shaderGlow>();
            sg.glowColor = Color.blue;
            sg.lightOff();
            change = false;
            if(posessScript.posessed)
                sg.lightOn();
        }

        //handle the initiation of the scare
        if (posessScript.posessed)
        {
			if (!started && !reverse && !scaring && (Input.GetButtonDown("A") || Input.GetMouseButtonDown(0)))
            {

                if (canScareNow())
                {
                    if(!started)
                        startAnimation();
                    if (!continueScare)
                        startScare();
                    else
                    {
                        scaring = true;
                        exitWindow = timer + 0.5f;//need to account for transition time in the animation controller
                    }
                }


            }
           
        }

        if (continueScare && scaring)
        {
            startScare();
        }

        
        if (anim != null)
             stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (anim != null && playing)
        {
            
           
            if (started || reverse)
                animTimer += Time.deltaTime;
            //playing = pianoRef.GetComponent<Animation>().IsPlaying("Take 001");
            if (started && animTimer > stateInfo.length)
            {
                started = false;
                reverse = true;
                animTimer = 0;

                anim.SetBool("ReverseBool", true);
                anim.SetBool("ScareBool", false);
            }

            if (reverse && animTimer > stateInfo.length)
            {
                started = false;
                reverse = false;
                animTimer = 0;
                anim.SetBool("IdleBool", true);
                anim.SetBool("ReverseBool", false);
                playing = false;
            }

           
        }

        //revert all things back to normal after scare is done
        if (scaring && stateInfo.IsName("Idle") && timer > exitWindow)
        {
            endScare();
        }

        if (scaring && (scareName.Equals("lamp") || scareName.Equals("bedlamp")) && timer > exitWindow)
        {
            endScare();
        }

       

    }

    //resets the scare object back to default
    void endScare()
    {
        scaring = false;

        people = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject p in people)
        {
            NavAgent person = p.GetComponent<NavAgent>();
            if (person != null)
            {
                if (person.getCurrentScare().Equals(scareName))
                    person.setCurrentScare("none");

            }
        }

        if (stayIn)
            this.gameObject.GetComponent<Posessable>().deposess();
    }

    //initiate the animation loop
    public void startAnimation()
    {
        if (scareSound != null)
        {
            scareSound.Play();
        }
        if (anim != null && !playing)
        {

            started = true;
            anim.SetBool("IdleBool", false);
            anim.SetBool("ScareBool", true);
            playing = true;
        }
        if (parts != null)
        {
            parts.gameObject.SetActive(true);
            //ensure all subparticles activate to get full effect
            //ParticleSystem[] subp = this.transform.parent.parent.GetComponentsInChildren<ParticleSystem>();
            //foreach (ParticleSystem ps in subp)
            //{
            //    //Debug.Log(ps.name);
            //    ps.enableEmission = true;
            //}


        }
    }

    //initiating the variables and functions for scaring
    public void startScare()
    {
        
        //Universal Script for a scare with wide reach, should place somewhere more accessible to all things
        people = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject p in people)
        {
            NavAgent person = p.GetComponent<NavAgent>();
            if (person != null)
            {

                Transform tempLoc = p.GetComponent<Transform>();
                //weird vooddoo to get the range circle
                Transform radiusLocation = this.gameObject.transform.parent.transform.parent.FindChild("Circle");


                //set a range on how it can work
                if (Vector3.Distance(tempLoc.position, radiusLocation.position) < scareRadius)
                {
                    if ((upstairs && tempLoc.position.y > 14) || (!upstairs && tempLoc.position.y < 13.5) && (!person.getCurrentScare().Equals(scareName) || !continueScare))//check that the scare happens on the right floor
                    {
                        scareLocation(person);
                        scarePerson(person,scareName);
                        //change outline color
                        shaderGlow sg = gameObject.transform.parent.GetComponent<shaderGlow>();
                        sg.changeColor(Color.red);
                        sg.lightOff();
                        if(posessScript.posessed)
                            sg.lightOn();
                        change = true;
                    }
                }


            }
        }
    }
    public void scareLocation(NavAgent person) {
        //target.position = coords;
        //target.position = new Vector3(8.18F, 11.35F, 0.59F);
        if (true) {
            
            person.setTarget(person.getCenter());
            person.setView(this.GetComponent<Transform>().position);
        }
        
    }

    //overloaded version
    public void scareLocation(NavAgent person, Transform goal)
    {
        //target.position = coords;
        //target.position = new Vector3(8.18F, 11.35F, 0.59F);
        if (true)
        {
            
            person.setTarget(goal.position);
            person.setView(this.GetComponent<Transform>().position);
        }

    }

    public void scarePerson(NavAgent person, string scareObject) {
        
            person.scared(scareVal,scareObject);
            timing = true;
            
            //set a window to scare multiple people if not using the constant scare
            if (!continueScare)
            {
                //usedTime = timer + 0.01f;
                //usedWindow = true;
                cooldownBool = true;
                coolWindow = timer + 0.01f;
            }
            else
            {
                cooldownBool = true;
                setCooldown();
            }

    }

    //start the cooldwon timing of the object
    void setCooldown()
    {
        cooldown = 0;
        cooldownBool = false;
    }

    //takes the item off cooldown
    public void resetCooldown()
    {
        cooldown = cooldownTime+0.1f;
        change = true;
    }

    public bool canScareNow()
    {
        return !scaring && (cooldown > cooldownTime);
    }

}
