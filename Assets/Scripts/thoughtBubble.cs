//using UnityEngine;
//using System.Collections;

//public class thoughtBubble : MonoBehaviour {

//	AI a;

//	// Use this for initialization
//	void Start () {
//		a = this.GetComponentInParent<AI> ();
//	}
	
//	// Update is called once per frame
//	void Update () {
//		this.transform.rotation = Quaternion.LookRotation (Camera.main.transform.forward);

//		if (a.onChair) {
//			this.GetComponent<MeshRenderer>().material = (Material) Resources.Load("thought bubble - sound");
//		}
//	}
//}
