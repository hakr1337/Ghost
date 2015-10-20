using UnityEngine;
using System.Collections;

public class tv : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponentInChildren<Posessable> ().posessed) {
			if(Input.GetKeyDown(KeyCode.Space)){
				this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
				if(this.GetComponentInChildren<Light>().enabled){
					GameObject.Find("pCube5").GetComponent<MeshRenderer>().material = (Material)Resources.Load("lambert6");
				}else{
					GameObject.Find("pCube5").GetComponent<MeshRenderer>().material = (Material)Resources.Load("lambert5");
				}
			}
		}
	}
}
