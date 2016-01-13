using UnityEngine;
using System.Collections;

public class fearMeter : MonoBehaviour {
    Transform t;
	// Use this for initialization
	void Start () {
        t = GameObject.Find("person").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        this.transform.position = new Vector3(t.position.x, transform.position.y,t.position.z);
    }
}
