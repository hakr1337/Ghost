using UnityEngine;
using System.Collections;

public class radio : MonoBehaviour {
    float timer;
    bool timing;

    AudioSource aud;
    Posessable p;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        p = GetComponentInChildren<Posessable>();
    }
	
	// Update is called once per frame
	void Update () {
		if (p.posessed) {
			if((Input.GetButtonDown("A") ||Input.GetMouseButtonDown(0))){
				if(aud.isPlaying){
					aud.Stop();
				}else{
					aud.Play();
                    timing = true;
				}
			}
		}

        if (timing) {
            timer += Time.deltaTime;
        }

        if (timer > 5) {
            timing = false;
            timer = 0;
            aud.Stop();
            p.shouldScare = false;
        }
	}
}
