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


	private Image exitImage;
	private Image resumeImage;
	private Image ShowControlsImage;
	private Image RestartImage;


	// Use this for initialization
	void Start () {
		PauseMenu = PauseMenu.GetComponent<Canvas> ();

		resumeText = resumeText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		restartText = restartText.GetComponent<Button> ();
		controlsText= controlsText.GetComponent<Button> ();


		exitImage = exitText.GetComponent<Image> ();
		resumeImage = resumeText.GetComponent<Image> ();
		ShowControlsImage = controlsText.GetComponent<Image> ();
		RestartImage = restartText.GetComponent<Image> ();

		PauseMenu.enabled = false;
		ControlsImage.enabled = false;
		resumeText.Select ();
		//exitControlsText.enabled = false;
	}

	public void PausePress()
		
	{
		

		PauseMenu.enabled = true;
		resumeText.enabled = true;
		controlsText.enabled = true;
		exitText.enabled = true;
		restartText.enabled = true;

		exitImage.enabled = true;
		ShowControlsImage.enabled = true;
		RestartImage.enabled = true;
		resumeImage.enabled = true;

		Time.timeScale = 0.0f;
		resumeText.Select ();
	}

	public void ResumePress()
		
	{
		PauseMenu.enabled = false;
		ControlsImage.enabled = false;
		hideButtons ();
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
		restartText.enabled = false;
		resumeText.enabled = false;

		exitImage.enabled = false;
		ShowControlsImage.enabled = false;
		RestartImage.enabled = false;
		resumeImage.enabled = false;

	}
	public void HideControl()
	{
		resumeText.Select ();
		PauseMenu.enabled = false;
		ControlsImage.enabled = false;
		Time.timeScale = 1.0f;
	}
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown ("Start")||Input.GetButtonDown ("B")) {

			if (ControlsImage.enabled == true) {
				ResumePress ();
			}
			else PausePress ();
		}
	}
}
