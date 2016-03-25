
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour {
	public Button startText;
	//public Button exitText;
	public Button exitMenuText;
	public Button ControlsText;
    public Button floodButton;
    public Button doubleButton;
	public Canvas ControlMenu;
	public Canvas SplashCanvas;
	public RectTransform splashScreen;
	float timer = 0f;
	// Use this for initialization
	void Start () {
		ControlMenu.enabled = false;
		startText = startText.GetComponent<Button> ();
		startText.image.enabled = false;
		ControlsText.image.enabled = false;
	//	exitText = exitText.GetComponent<Button> ();
		exitMenuText = exitMenuText.GetComponent<Button> ();
		startText.image.enabled = true;
		ControlsText.image.enabled = true;
		//SplashCanvas.enabled=true;
		//splashScreen =SplashCanvas.GetComponent<RectTransform> ();
		//splashScreen.sizeDelta = new Vector2 (800, 285);
		//splashScreen.localPosition.Set(382f, 136f, 0f);
		//startText.enabled = false;
		//ControlsText.enabled = false;
		startText.Select ();

	}
	public void ExitPress()
	{
		startText.Select (); // exits controls screen and reselects startText
		ControlMenu.enabled = false;

    }

	public void StartLevel()
	{
        SceneManager.LoadScene(2);
        GameModeControl.mode = 0;

	}

    public void StartFlood()
    {
        SceneManager.LoadScene(2);
        GameModeControl.mode = 1;
    }
    public void StartDouble()
    {
        SceneManager.LoadScene(2);
        GameModeControl.mode = 2;
    }
    public void ShowControl()
	{
		ControlMenu.enabled = true;
		exitMenuText.Select ();
        floodButton.image.enabled = false;
        doubleButton.image.enabled = false;
	}
	public void HideControl()
	{
		ControlMenu.enabled = false;
		startText.Select ();
        floodButton.image.enabled = true;
        doubleButton.image.enabled = true;
    }
	public void ExitGame()
	{
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update () {
		/*timer += Time.deltaTime; 
		if (timer > 5f) {

			SplashCanvas.enabled=false;
			startText.image.enabled = true;
			ControlsText.image.enabled = true;

		}*/
	}
}
