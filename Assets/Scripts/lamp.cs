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
			if((Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space))){
                this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
                    

                
            }
		}
	}

}
