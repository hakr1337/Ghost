using UnityEngine;
using System.Collections;

public class FearBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if(c.name == "Player"){
			this.gameObject.SetActive(false);		
			c.GetComponent<player>().CollectFear();
		}
	}
}
