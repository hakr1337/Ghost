using UnityEngine;
using System.Collections;

public class tv : MonoBehaviour {

    // Use this for initialization

	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponentInChildren<Posessable> ().posessed) {
			if((Input.GetButtonDown("A") ||Input.GetMouseButtonDown(0))){
				//this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;
                
				//if(this.GetComponentInChildren<Light>().enabled){
				//	GameObject.Find("tvScreen").GetComponent<MeshRenderer>().material = (Material)Resources.Load("lambert6");
				//}else{
				//	GameObject.Find("tvScreen").GetComponent<MeshRenderer>().material = (Material)Resources.Load("lambert5");
				//}
			}
		}
	}
}
