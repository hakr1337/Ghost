using UnityEngine;
using System.Collections;

public class piano : MonoBehaviour {
    AudioSource march;
    Posessable p;
    GameObject pianoRef;
    Animator anim;
    bool started;
    bool reverse;
    float timer;
    float waitLength;
    int scareHash;
    int reverseHash;
    int idleHash;
	// Use this for initialization
	void Start () {
       p = GetComponentInChildren<Posessable>();
       march = GetComponent<AudioSource>();
        pianoRef = GameObject.Find("coloredpiano");
        started = false;
        reverse = false;
        anim = pianoRef.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
		if (!started && !reverse && p.posessed && (Input.GetButtonDown("A") || Input.GetMouseButtonDown(0))) {
            march.Play();
            anim.SetBool("IdleBool", false);
            anim.SetBool("ScareBool", true);
            started = true;
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (started || reverse)
            timer += Time.deltaTime;
            //playing = pianoRef.GetComponent<Animation>().IsPlaying("Take 001");
        if(started && timer > stateInfo.length)
        {
            started = false;
            reverse = true;
            timer = 0;
            anim.SetBool("ReverseBool", true);
            anim.SetBool("ScareBool", false);
        }

        if (reverse && timer > stateInfo.length)
        {
            started = false;
            reverse = false;
            timer = 0;
            anim.SetBool("IdleBool", true);
            anim.SetBool("ReverseBool", false);
        }

        if (march.isPlaying) {
            p.shouldScare = true;
        } else {
            p.shouldScare = false;
        }
    }
}
