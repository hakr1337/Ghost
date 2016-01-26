using UnityEngine;
using System.Collections;

public class piano : MonoBehaviour {
    AudioSource march;
    Posessable p;
	// Use this for initialization
	void Start () {
       p = GetComponentInChildren<Posessable>();
       march = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update() {
		if (p.posessed && (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space))) {
            march.Play();
        }

        if (march.isPlaying) {
            p.shouldScare = true;
        } else {
            p.shouldScare = false;
        }
    }
}
