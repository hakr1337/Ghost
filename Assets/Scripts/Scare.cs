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
    public bool global;
    public bool upstairs;

    Posessable posessScript;
    GameObject[] people;
    public float scareRadius;

    int scareHash;
    int reverseHash;
    int idleHash;

    Animator anim;

    bool started;
    bool reverse;


    void Start() {
        used = false;
        target = GameObject.Find("Target").GetComponent<Transform>();
        posessScript = this.GetComponentInChildren<Posessable>();
        usedWindow = false;
        cooldown = cooldownTime;
        cooldownBool = false;

        anim = GetComponentInParent<Animator>();
        animTimer = 0;

        scareHash = Animator.StringToHash("goScare");
        reverseHash = Animator.StringToHash("goReverse");
        idleHash = Animator.StringToHash("goIdle");

        started = false;
        reverse = false;


    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        cooldown += Time.deltaTime;
        if (cooldownBool && (timer > coolWindow))//allow window so one object can scare multiple people at once, could be cleaner
            setCooldown();

        if (posessScript.posessed)
        {
            if (!started && !reverse && (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space)))
            {

                if (canScareNow())
                {
                    if (anim != null)
                    {
                        anim.SetTrigger(scareHash);
                        started = true;
                    }
                    //Universal Script for a scare with wide reach, should place somewhere more accessible to all things
                    people = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject p in people)
                    {
                        NavAgent person = p.GetComponent<NavAgent>();
                        if (person != null)
                        {

                            Transform tempLoc = p.GetComponent<Transform>();

                            //set a range on how it can work
                            if (Vector3.Distance(tempLoc.position, this.GetComponent<Transform>().position) < scareRadius)
                            {
                                if ((upstairs && tempLoc.position.y > 14) || (!upstairs && tempLoc.position.y < 13.5))//check that the scare happens on the right floor
                                {
                                    scareLocation(person);
                                    scarePerson(person);
                                }
                            }
                        

                        }
                    }
                }


            }
            if (anim != null)
            {
                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

                if (started || reverse)
                    animTimer += Time.deltaTime;
                //playing = pianoRef.GetComponent<Animation>().IsPlaying("Take 001");
                if (started && timer > stateInfo.length)
                {
                    started = false;
                    reverse = true;
                    animTimer = 0;
                    anim.SetTrigger(reverseHash);
                }

                if (reverse && timer > stateInfo.length)
                {
                    started = false;
                    reverse = false;
                    animTimer = 0;
                    anim.SetTrigger(idleHash);
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

    public void scarePerson(NavAgent person) {
        
            person.scared(scareVal);
            timing = true;
            usedTime = timer + 0.01f;
            coolWindow = timer + 0.01f;
            usedWindow = true;
            cooldownBool = true;
        
    }

    public bool isGlobal()
    {
        return global;
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
