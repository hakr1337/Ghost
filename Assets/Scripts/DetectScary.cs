using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class DetectScary : MonoBehaviour {

	public bool fromTV = false;
	public bool fromLamp = false;
	public bool fromRadio = false;

	private  GameObject canvas;
	public float scary = 0;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider c){
		Posessable p = c.GetComponent<Posessable> ();

		if (p == null) {
			p = c.GetComponentInChildren<Posessable>();
		}

		//print ("in");
		if(p != null){
			if(p.shouldScare && p.posessed){
				if(c.name == "tvwithcolor"){
					fromTV = true;
				}else if(c.name == "radio"){
					fromTV = false;
					fromRadio = true;
				}
				//print (c.name);

				scary += Time.deltaTime;
				canvas.GetComponentInChildren<Text>().text = "Scare Factor\n       " + (int)scary;
			}
		}

	}

	void OnTriggerEnter(Collider c){

	}

	float getScary(){
		return scary;
	}
}
