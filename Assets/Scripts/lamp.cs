using UnityEngine;
using System.Collections;

public class lamp : MonoBehaviour {



    // Use this for initialization

	//public AudioClip lampclick;
	//public AudioClip bulbbreak;


	//private AudioSource source;



    Posessable posessScript;
    public GameObject[] people;
    void Start () {
        posessScript = this.GetComponentInChildren<Posessable>();

		//source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		if (posessScript.posessed) {
			if((Input.GetButtonDown("A") || Input.GetMouseButtonDown(0))){


                stupidLamp();
            }
		}
	}

    public void stupidLamp()
    {
        //source.PlayOneShot(lampclick, 1f);


        Light[] temp = this.GetComponentsInChildren<Light>();

        foreach (Light l in temp)
        {
            l.enabled = !l.enabled;
        }
    }

}
