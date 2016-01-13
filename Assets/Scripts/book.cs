using UnityEngine;
using System.Collections;

public class book : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < 11.3f) {
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
	}
}
