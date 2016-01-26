using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


	bool timing;
	float timer;
	Animation anim;
	OffMeshLink link;
	public bool open;
	// Use this for initialization
	void Start () {
		timing = false;
		timer = 0;
		anim = GetComponent<Animation> ();
		open = true;
		anim.Play("new_opening");
		//anim.SetBool ("Open", true);
		link = GetComponentInChildren<OffMeshLink> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(this.GetComponentInChildren<Posessable>().posessed && Input.GetButtonDown("A")){
			this.GetComponentInChildren<Posessable>().shouldScare = false;
			if(!anim.isPlaying){
				if(!open){
					anim.Play("new_opening");
					open = true;
					//anim.SetBool("Open", true);
					link.activated = true;
					timing = false;
					timer = 0;
				}else{
					open = false;
					anim ["new_closing"].speed = -1;
					anim["new_closing"].time = anim["new_closing"].length;
					anim.Play("new_closing");
					timing = true;
					link.activated = false;
					//timer = 0;
				}
			}
		}

		if(timing){
			timer += Time.deltaTime;
		}

		if(timer > 5){
			timing = false;
			link.activated = true;
			timer = 0;
			anim.Play("new_opening");
			//anim.SetBool("Open", true);
		}
	}
	
}
