using UnityEngine;
using System.Collections;

public class ballControl : MonoBehaviour {
	public float speed;
    public float thrust;
    private Rigidbody rb;
    float timer;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //had to flip controls because of our axis switch
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
        timer += Time.deltaTime;
        rb.AddForce(movement*speed);

        if ( Input.GetButtonDown("A") )
        {
            rb.AddForce(Vector3.up * thrust);
            timer = 0;
        }
       
	}
}
