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

    bool started;
    bool reverse;
    bool playing;
    

    public bool hasParticle;

    //variable when scare tirggered
    //use to change th shader glow to gray and then blue 
    //again when scare is available
    bool change;

    void Start() {
        used = false;
        target = GameObject.Find("Target").GetComponent<Transform>();
        //parts =  GetComponentInChildren<ParticleSystem>();
        posessScript = this.gameObject.GetComponent<Posessable>();
        
        usedWindow = false;
        cooldown = cooldownTime;
        cooldownBool = false;

        anim = GetComponentInParent<Animator>();
        animTimer = 0;


        playing = false;
        started = false;
        reverse = false;

        change = false;

        


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

        if (posessScript.posessed)
        {
			if (!started && !reverse && (Input.GetButtonDown("A") || Input.GetMouseButtonDown(0)))
            {

                if (canScareNow())
                {
                    if (anim != null && !playing)
                    {
                        
                        started = true;
                        anim.SetBool("IdleBool", false);
                        anim.SetBool("ScareBool", true);
                        playing = true;
                    }
                    if(parts!= null)
                    {
                        parts.gameObject.SetActive(true);
     
                        
                    }
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
                                if ((upstairs && tempLoc.position.y > 14) || (!upstairs && tempLoc.position.y < 13.5))//check that the scare happens on the right floor
                                {
                                    scareLocation(person);
                                    scarePerson(person);
                                    //change outline color
                                    shaderGlow sg = gameObject.transform.parent.GetComponent<shaderGlow>();
                                    sg.changeColor(Color.red);
                                    sg.lightOff();
                                    sg.lightOn();
                                    change = true;
                                }
                            }
                        

                        }
                    }
                }


            }
           
        }

        if (anim != null && playing)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

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

    public void scarePerson(NavAgent person) {
        
            person.scared(scareVal);
            timing = true;
            usedTime = timer + 0.01f;
            coolWindow = timer + 0.01f;
            usedWindow = true;
            cooldownBool = true;
        
    }


    void setCooldown()
    {
        cooldown = 0;
        cooldownBool = false;
    }

    public bool canScareNow()
    {
        return (timer > 1) && (cooldown > cooldownTime);
    }

    public void resetScareTimer()
    {
        //timer = 0;
    }

    public void wasUsed()
    {
        used = true;
    }

    public void resetUsed()
    {
        used = false;
        usedWindow = false;
        cooldownBool = false;
        usedTime = 0;
        cooldown = cooldownTime + 0.5f;//reset the timer with some grace for error
        timer = 0;
    }
}
