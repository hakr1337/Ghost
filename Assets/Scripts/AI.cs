using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public bool toChair = false;
	public bool atChair = false;
	public bool turnAround = false;
	public bool onChair = false;
	public bool atLamp = false;

	public float speed = .01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//get off couch
		if (this.GetComponentInChildren<DetectScary> ().scary > 5 && !toChair && !atChair && this.GetComponentInChildren<DetectScary> ().fromTV) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                       this.transform.localPosition.y,
                                                       this.transform.localPosition.z + speed);
		}

		//turn to chair
		if (toChair && this.transform.eulerAngles.y < 90) {

				this.transform.Rotate(new Vector3(this.transform.localRotation.x,
				                                  this.transform.localRotation.y + 2,
				                                  this.transform.localRotation.z));

		}

		//move to chair
		if(this.transform.eulerAngles.y > 90 && !atChair){
			this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z);
		}

		//turn away from chair
		if (atChair && !toChair && turnAround) {

			this.transform.Rotate(new Vector3(this.transform.localRotation.x,
			                                  this.transform.localRotation.y + 2,
			                                  this.transform.localRotation.z));
			
		}

		//stop turn
		if (this.transform.eulerAngles.y >= 270 && atChair && !onChair) {
			turnAround = false;

			if(this.transform.localPosition.y < 12.3){
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,
				                                           this.transform.localPosition.y  + .08f,
				                                           this.transform.localPosition.z);

				if(this.transform.localPosition.x < 11.4){
					this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed,
					                                           this.transform.localPosition.y,
					                                           this.transform.localPosition.z);
				}
			}


		}

		//move off chair
		if(onChair && this.GetComponentInChildren<DetectScary> ().fromRadio && this.GetComponentInChildren<DetectScary>().scary > 15){
			if(!atLamp){
				this.transform.localPosition = new Vector3(this.transform.localPosition.x - speed,
			                                           this.transform.localPosition.y,
			                                           this.transform.localPosition.z);
		
			}
		}


	}

	void OnTriggerEnter(Collider c){
		if (c.name == "off couch") {
			toChair = true;
		} else if (c.name == "at chair" && !onChair){
			toChair = false;
			atChair = true;
			turnAround = true;
		}else if(c.name == "on chair"){
			onChair = true;
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
			//turnAround = false;
		}else if(c.name == "at lamp"){
			onChair = false;
			atLamp = true;
		}

	}


}
