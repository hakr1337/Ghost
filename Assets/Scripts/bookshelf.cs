using UnityEngine;
using System.Collections;

public class bookshelf : MonoBehaviour {
    GameObject books;
    GameObject shelf;
    //public int scareValue = 3;
	// Use this for initialization
	void Start () {
		books = GameObject.Find("books (1)");
		shelf = GameObject.Find("bookshelf (1)");

        Transform[] book = GetComponentsInChildren<Transform>();
        

        foreach(Transform t in book) {
           Rigidbody r = t.gameObject.AddComponent<Rigidbody>();
           t.gameObject.AddComponent<book>();
           r.isKinematic = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (shelf.GetComponentInChildren<Posessable>().posessed) {
			if ((Input.GetButtonDown("A") || Input.GetMouseButtonDown(0))) {
                Transform[] book = GetComponentsInChildren<Transform>();
                shelf.transform.FindChild("Trigger").gameObject.GetComponent<Collider>().isTrigger = true;
                shelf.GetComponentInChildren<Collider>().isTrigger = true;
                foreach (Transform t in book) {
                    Rigidbody r = t.gameObject.GetComponent<Rigidbody>();
                    r.isKinematic = false;
                    /* t.localPosition = new Vector3(t.localPosition.x,
                                                   t.localPosition.y,
                                                   t.localPosition.z + .2f);
                     */
                    r.AddForce(new Vector3(0,0,100));
                    
                }
            }
        }
	}
}
