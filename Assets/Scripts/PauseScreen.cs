using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour {

	public Canvas PauseMenu;
	public Image ControlsImage;
	public Button resumeText;
	public Button restartText;
	public Button exitText;
	public Button controlsText;
	public Button exitControlsText;
	Cam c;
	player p;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player").GetComponent<player> ();
		c = Camera.main.GetComponent<Cam> ();
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		resumeText = resumeText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		restartText = exitText.GetComponent<Button> ();
		controlsText= controlsText.GetComponent<Button> ();
		exitControlsText = exitControlsText.GetComponent<Button> ();
		PauseMenu.enabled = false;
		ControlsImage.enabled = false;
		//exitControlsText.enabled = false;
	}

	public void PausePress()
		
	{
		PauseMenu.enabled = true;
		resumeText.enabled = true;
		controlsText.enabled = true;
		exitText.enabled = true;
		restartText.enabled = true;
		Time.timeScale = 0.0f;
		resumeText.Select ();
		c.pause (true);
		p.canFly = false;

	}

	public void ResumePress()
		
	{
		PauseMenu.enabled = false;
		hideButtons ();
		p.canFly = true;
		Time.timeScale = 1.0f;
	}

	public void ReStartLevel()
	{
        Time.timeScale = 1.0f;
        Application.LoadLevel(1);
	}
	public void ExitLevel()
	{
		Application.Quit();
		
	}

	void hideButtons(){
		resumeText.enabled = false;
		exitText.enabled = false;
		restartText.enabled = false;
		controlsText.enabled = false;
	}

	public void ShowControl()
	{

		ControlsImage.enabled = true;
		exitControlsText.enabled = true;
		exitControlsText.Select ();
	}
	public void HideControl()
	{
		PauseMenu.enabled = false;
		ControlsImage.enabled = false;
		Time.timeScale = 1.0f;
	}
	// Update is called once per frame

}
