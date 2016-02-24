using UnityEngine;
using System.Collections;

public class fireplace : MonoBehaviour {
    Posessable p;
    ParticleSystem fire;
    float timer;
    bool timing;

	// Use this for initialization
	void Start () {
        p = GetComponentInChildren<Posessable>();

        //ParticleSystem[] x = GetComponentsInChildren<ParticleSystem>();
		fire = GameObject.Find ("fireplace_fire").GetComponent<ParticleSystem>();
        fire.enableEmission = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (p.posessed && (Input.GetButtonDown("A") || Input.GetMouseButtonDown(0))) {
            fire.enableEmission = true;
            timing = true;
        }

        if (timer > 5 && fire.isPlaying) {
            fire.enableEmission = false;
            timer = 0;
            timing = false;
        }

        if (timing) {
            timer += Time.deltaTime;
        }
	}
}
