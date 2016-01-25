using UnityEngine;
using System.Collections;

public class lamp : MonoBehaviour {

    // Use this for initialization
    Scare s;
    Transform target;
    public float scareTimer;
    Posessable posessScript;
    public GameObject[] people;
    void Start () {
        s = GetComponentInChildren<Scare>();
        scareTimer = 6;
        target = GameObject.Find("AINode1").GetComponent<Transform>();
        posessScript = this.GetComponentInChildren<Posessable>();

    }
	
	// Update is called once per frame
	void Update () {
		if (posessScript.posessed) {
			if(Input.GetButtonDown("A")){
                this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
                    
                    //Universal Script for a scare with wide reach, should place somewhere more accessible to all things
                    people = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject p in people)
                    {
                        NavAgent person = p.GetComponent<NavAgent>();
                        if (person != null)
                        {

                        if (s.canScareNow())
                        {
                            Transform tempLoc = p.GetComponent<Transform>();

                            //set a range on how it can work
                            if (Vector3.Distance(tempLoc.position, this.GetComponent<Transform>().position) < 3.5f)
                            {
                                s.scareLocation(person, target);
                                s.scarePerson(person);
                            }
                        }

                    }
                    }

                
            }
		}
	}

}
