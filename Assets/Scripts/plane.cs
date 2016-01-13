using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponentInChildren<Posessable>().posessed == true)
        {
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.GetComponent<planeControl>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            gameObject.GetComponent<planeControl>().enabled = false;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void deposess()
    {
        Transform t = GameObject.Find("Player").transform;

        t.position = this.transform.position;
    }
}
