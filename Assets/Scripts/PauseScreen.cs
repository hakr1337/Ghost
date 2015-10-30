using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour {

	public Canvas PauseMenu;
	public Button resumeText;
	public Button restartText;
	public Button exitText;


	// Use this for initialization
	void Start () {
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		resumeText = resumeText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		restartText = exitText.GetComponent<Button> ();
		PauseMenu.enabled = false;
	}

	public void PausePress()
		
	{
		PauseMenu.enabled = true;
		resumeText.enabled = true;
		exitText.enabled = true;
		restartText.enabled = true;
		Time.timeScale = 0.0f;
	}

	public void ResumePress()
		
	{
		PauseMenu.enabled = false;
		Time.timeScale = 1.0f;
	}

	public void ReStartLevel()
	{
		Application.LoadLevel (1);
		
	}
	public void ExitLevel()
	{
		Application.LoadLevel (0);
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
