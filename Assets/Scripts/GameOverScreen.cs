using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	public Canvas GameOverCanvas;

	public Button exitText;
	public Button restartText;

	// Use this for initialization
	void Start () {

		GameOverCanvas = GameOverCanvas.GetComponent<Canvas> ();

		exitText = exitText.GetComponent<Button> ();
		restartText = exitText.GetComponent<Button> ();

		GameOverCanvas.enabled = false;

	
	}
	public void Died()
		
	{
		GameOverCanvas.enabled = true;

		exitText.enabled = true;
		restartText.enabled = true;
		Time.timeScale = 0.0f;
		restartText.Select ();
	}

	public void ReStartLevel()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(1);
		GameOverCanvas.enabled = false;
	}

	public void ExitLevel()
	{
		Application.Quit();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
