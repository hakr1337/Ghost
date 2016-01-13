using UnityEngine;
using System.Collections;

public class planeControl : MonoBehaviour {

    // Use this for initialization
    public float tilt;//how fast the plane tilts side to side
    public float speed;//how fast the plane moves forward
    public float rot;//how fast the plane rotates
    public float smooth;
    public float rise;//how much the plane will tilt up or down
    private Rigidbody rb;
    private Transform trans; 
   

    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        trans = gameObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.AddRelativeForce(speed, 0, 0);
        
        //bank plane left
        if(Input.GetAxis("Horizontal") < 0)
        {
            Quaternion target;
            Vector3 rotVector;
            float rotPos = trans.rotation.eulerAngles.x;//current x rotation value

            if (rotPos < 45 - tilt || rotPos > 315)
            {
                rotVector = trans.eulerAngles;
                rotVector.x = rotVector.x + tilt;
                target = Quaternion.Euler(rotVector);
                
                    trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
            }


            rotVector = trans.eulerAngles;
            rotVector.y = rotVector.y - rot;
            target = Quaternion.Euler(rotVector);
            trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
        }

       

        //bank right
        if (Input.GetAxis("Horizontal") > 0)
        {

            Quaternion target;
            Vector3 rotVector;
            float rotPos = trans.rotation.eulerAngles.x; //current x rotation value

            if (rotPos < 45 || rotPos > 315 + tilt)
            {
                rotVector = trans.eulerAngles;
                rotVector.x = rotVector.x - tilt;
                target = Quaternion.Euler(rotVector);
                
                    trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
            }

            rotVector = trans.eulerAngles;
            rotVector.y = rotVector.y + rot;
            target = Quaternion.Euler(rotVector);
            trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
        }

        //put plane right side up again
        if (Input.GetAxis("Vertical") < 0)
        {
            float rotPos = trans.rotation.eulerAngles.z; //current x rotation value

            Quaternion target;
            Vector3 rotVector = trans.eulerAngles;
            if (rotPos > 330 || rotPos < 30 - rise)
            {
                rotVector.z = rotVector.z + rise;
                target = Quaternion.Euler(rotVector);
                
                    trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
            }
        }

        //put plane right side up again
        if (Input.GetAxis("Vertical") > 0)
        {
            float rotPos = trans.rotation.eulerAngles.z; //current x rotation value
            
            Quaternion target;
            Vector3 rotVector = trans.eulerAngles;
            if (rotPos > 330 + rise || rotPos < 30)
            {
                rotVector.z = rotVector.z - rise;
                target = Quaternion.Euler(rotVector);

                
                    trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * smooth);
            }
        }
    }
}
