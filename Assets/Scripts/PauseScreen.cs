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
	public Canvas gameplayUI;

	bool usingController = false;
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
		exitControlsText.gameObject.SetActive (false);
		//exitControlsText.enabled = false;
		foreach (string word in Input.GetJoystickNames()) {
			if (word != "") {
				usingController = true;
			} 
		}
	}

	public void PausePress()

	{
		showButtons ();
		PauseMenu.enabled = true;
		resumeText.enabled = true;
		controlsText.enabled = true;
		exitText.enabled = true;
		restartText.enabled = true;
		gameplayUI.enabled = false;
		Time.timeScale = 0.0f;
		if (usingController) {
			resumeText.Select ();
		}
		c.pause (true);
		p.canFly = false;

	}

	public void ResumePress()

	{
		PauseMenu.enabled = false;
		hideButtons ();
		p.canFly = true;
		c.pause (false);
		Time.timeScale = 1.0f;
		gameplayUI.enabled = true;
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
		resumeText.gameObject.SetActive (false);
		exitText.gameObject.SetActive (false);
		restartText.gameObject.SetActive (false);
		controlsText.gameObject.SetActive (false);
	}

	void showButtons(){
		resumeText.gameObject.SetActive (true);
		exitText.gameObject.SetActive (true);
		restartText.gameObject.SetActive (true);
		controlsText.gameObject.SetActive (true);
	}

	public void ShowControl()
	{

		exitControlsText.gameObject.SetActive (true);
		ControlsImage.enabled = true;
		//exitControlsText.enabled = true;
		if (usingController) {
			exitControlsText.Select ();
		}
		hideButtons ();
	}
	public void HideControl()
	{
		//PauseMenu.enabled = false;
		exitControlsText.gameObject.SetActive (false);
		ControlsImage.enabled = false;
		//Time.timeScale = 1.0f;
		showButtons();
	}
	// Update is called once per frame



}
