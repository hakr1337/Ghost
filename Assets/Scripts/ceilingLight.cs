using UnityEngine;
using System.Collections;

public class ceilingLight : MonoBehaviour {

    // Use this for initialization
    public NavAgent person;
    private Transform target;
    void Start()
    {
//        person = GameObject.Find("person").GetComponent<NavAgent>();
        target = GameObject.Find("Target").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "plane")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if(c.name == "tvwithcolor")
        {
            GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
            target.position = new Vector3(11.57F, 11.568F, -4.9F);
            //person.setTarget(target.position);

            foreach (GameObject o in fires)
            {
                o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
                o.GetComponentInChildren<ParticleSystem>().Clear();
            }
        }
    }
}
