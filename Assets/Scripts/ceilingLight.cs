using UnityEngine;
using System.Collections;

public class ceilingLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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

            foreach (GameObject o in fires)
            {
                o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
                o.GetComponentInChildren<ParticleSystem>().Clear();
            }
        }
    }
}
