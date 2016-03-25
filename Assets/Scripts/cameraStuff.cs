using UnityEngine;
using System.Collections;

public class cameraStuff : MonoBehaviour {
	GameObject player;
	GameObject[] x;

    GameObject p;
    Posessable[] posessables;

	public bool visionOn = false;

    public float moveSpeedZ = 8f;
    public float moveThresholdZ = .8f;


    // Use this for initialization
    void Start () {
		player = GameObject.Find("Player");
		x = GameObject.FindGameObjectsWithTag("Particles");
        posessables = GameObject.FindObjectsOfType<Posessable>();
		//foreach(GameObject o in x){
		//	o.GetComponentInChildren<ParticleSystem>().enableEmission = false;	
		//}
	}

	// Update is called once per frame
	void Update () {
        bool inObject = false;

        foreach (Posessable f in posessables) {
            if (f.posessed) {
                p = f.gameObject;
                inObject = true;
            }
            if (!inObject) {
                p = player;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = !this.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
            visionOn = !visionOn;

//            GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled = !GameObject.Find("thought bubble").GetComponent<MeshRenderer>().enabled;

            foreach (GameObject o in x) {
                o.GetComponentInChildren<ParticleSystem>().enableEmission = !o.GetComponentInChildren<ParticleSystem>().enableEmission;
                o.GetComponentInChildren<ParticleSystem>().Clear();
            }
        }

        //move left and right
        if (this.transform.localPosition.z > p.transform.position.z + moveThresholdZ || this.transform.localPosition.z < p.transform.position.z - moveThresholdZ) {
            if (this.transform.localPosition.z < p.transform.position.z) {
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
        if (this.transform.localPosition.x < p.transform.position.x - 3 && this.transform.position.x < 11.5f) {

            this.transform.localPosition = new Vector3(this.transform.localPosition.x + Time.deltaTime * 1.5f,
                                                        this.transform.localPosition.y,
                                                        this.transform.localPosition.z);

        }

        //move backward
        if (Input.GetKey(KeyCode.S) && this.transform.localPosition.x > 3.8f) {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - Time.deltaTime * 1.5f,
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
