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
    public bool used;
    public bool usedWindow;
    float cooldown;
    public int cooldownTime;

    public float timer2;
    public bool timing2;
    public bool global;

    void Start() {
        used = false;
        target = GameObject.Find("Target").GetComponent<Transform>();
        usedWindow = false;
		
        
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        cooldown += Time.deltaTime;
        if (usedWindow && (timer > usedTime))//allow window so one object can scare multiple people at once, could be cleaner
            wasUsed();

    }

    public void scareLocation(NavAgent person) {
        //target.position = coords;
        //target.position = new Vector3(8.18F, 11.35F, 0.59F);
        if (true) {
            timing2 = true;
            person.setTarget(target.position);
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
            timing2 = true;
            person.setTarget(goal.position);
            person.setView(goal.position);
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
