using UnityEngine;
using System.Collections;

public class cameraStuff : MonoBehaviour {
	GameObject p;
	GameObject[] x;

	public bool visionOn = false;

    public float moveSpeedZ = 4f;
    public float moveThresholdZ = .8f;


    // Use this for initialization
    void Start () {
		p = GameObject.Find("Player");
		x = GameObject.FindGameObjectsWithTag("Particles");

		foreach(GameObject o in x){
			o.GetComponentInChildren<ParticleSystem>().enableEmission = false;	
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
			visionOn = !visionOn;

			GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;

			foreach(GameObject o in x){
				o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
				o.GetComponentInChildren<ParticleSystem>().Clear();
			}
		}

        //move left and right
        if (this.transform.localPosition.z > p.transform.localPosition.z + moveThresholdZ || this.transform.localPosition.z < p.transform.localPosition.z - moveThresholdZ) {
            if (this.transform.localPosition.z < p.transform.localPosition.z) {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                            this.transform.localPosition.y,
                                                            this.transform.localPosition.z + Time.deltaTime * moveSpeedZ);
            } else {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                            this.transform.localPosition.y,
                                                            this.transform.localPosition.z - Time.deltaTime * moveSpeedZ);
            }
        }
        
        //move forward
        if ( this.transform.localPosition.x < p.transform.localPosition.x - 3 && this.transform.localPosition.x < 6.5f) {
            
                this.transform.localPosition = new Vector3(this.transform.localPosition.x + Time.deltaTime * 6,
                                                            this.transform.localPosition.y,
                                                            this.transform.localPosition.z);
           
        }

        //move backward
        if (Input.GetKey(KeyCode.S) && this.transform.localPosition.x > 3.8f) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - Time.deltaTime * 6,
                                                            this.transform.localPosition.y,
                                                            this.transform.localPosition.z);
        }



    }

    //turn off ghost vision
	public void turnOff(){

		this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
		visionOn = !visionOn;
		
		GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;
		
		foreach(GameObject o in x){
			o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
			o.GetComponentInChildren<ParticleSystem>().Clear();
		}
	}
}
