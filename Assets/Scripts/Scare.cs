using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

    // Use this for initialization
    public int scareVal;
    private Transform target;
    public float timer;
    public float usedTime;
    bool timing;
    bool moving;
    bool used;
    bool usedWindow;
    float cooldown;
    public int cooldownTime;

    float timer2;
    bool timing2;
    public bool global;

    Posessable posessScript;
    GameObject[] people;
    public float scareRadius;

    void Start() {
        used = false;
        target = GameObject.Find("Target").GetComponent<Transform>();
        posessScript = this.GetComponentInChildren<Posessable>();
        usedWindow = false;
        cooldown = cooldownTime;
		
        
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        cooldown += Time.deltaTime;
        //if (usedWindow && (timer > usedTime))//allow window so one object can scare multiple people at once, could be cleaner
        //wasUsed();

        if (posessScript.posessed)
        {
            if ((Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space)))
            {

                if (canScareNow())
                {
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
                                 scareLocation(person, target);
                                 scarePerson(person);
                            }
                        

                        }
                    }
                }


            }
        }

    }

    public void scareLocation(NavAgent person) {
        if (true) {
            
            person.setTarget(target.position);
            person.setView(this.GetComponentInParent<Transform>().position);
        }
        
    }

    //overloaded version
    public void scareLocation(NavAgent person, Transform goal)
    {

        if (true)
        {
            
            person.setTarget(goal.position);
            person.setView(this.GetComponentInParent<Transform>().position);
        }

    }

    public void scarePerson(NavAgent person) {
        if ( true) {
            person.scared(scareVal);
            timing = true;
            usedTime = timer + 0.01f;
            usedWindow = true;
            cooldown = 0;
        }
    }

    public bool isGlobal()
    {
        return global;
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
        usedTime = 0;
    }
}
