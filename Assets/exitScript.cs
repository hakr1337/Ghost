using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class exitScript : MonoBehaviour {
	Text lives;
	public int tot_lives = 3;
	int lives_left;
	GameOverScreen GO;
	// Use this for initialization
	void Start () {
		lives = GameObject.Find ("Lives").GetComponent<Text>();
		lives_left = tot_lives;
		GO = GameObject.Find ("gameover").GetComponent<GameOverScreen>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.GetComponent<NavMeshAgent>() != null){
			Destroy (c.gameObject);
			lives_left--;

			lives.text = "Lives: " + lives_left+"/"+tot_lives;
		}

		if(lives_left == 0){
			GO.Died();
		}
	}
}
