using UnityEngine;
using System.Collections;

public class lamp : MonoBehaviour {



    // Use this for initialization

	public AudioClip lampclick;
	//public AudioClip bulbbreak;


	private AudioSource source;



    Posessable posessScript;
    public GameObject[] people;
    void Start () {
        posessScript = this.GetComponentInChildren<Posessable>();

		source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		if (posessScript.posessed) {
			if((Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space))){
				source.PlayOneShot(lampclick, 1f);
                this.GetComponentInChildren<Light>().enabled = !this.GetComponentInChildren<Light>().enabled;


                
            }
		}
	}

}
