using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

    // Use this for initialization
    public int scareVal;
    private Transform target;
    float timer;
    bool timing;
    bool moving;
    public int color;

   public float timer2;
    public bool timing2;
    public bool global;

    void Start() {
        
        target = GameObject.Find("Target").GetComponent<Transform>();
		if (this.name == "trigger" || this.name == "Trigger") {
			color = GetComponent<Posessable> ().color;
		}
        
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

    }

    public void scareLocation(NavAgent person) {
        //target.position = coords;
        //target.position = new Vector3(8.18F, 11.35F, 0.59F);
        if (true) {
            timing2 = true;
            person.setTarget(target.position);
            person.setView(target.position);
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
            person.scared(color);
            timing = true;
        }
    }

    public bool isGlobal()
    {
        return global;
    }

    public bool canScareNow()
    {
        return timer > 10;
    }

    public void resetScareTimer()
    {
        timer = 0;
    }
}
