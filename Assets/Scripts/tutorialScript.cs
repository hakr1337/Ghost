using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour {
    int tutCount;
    Sprite[] tutImages;
    Sprite[] cutImages;
    Image tutCurr;
    Image currCut;
    Image nextCut;
    int totalImages;
    int cutCount;
    float cutTimer;
    bool playing;
    float cutLength;
    float window;
    int totalCutCount;
    Text nextText;
    Text backText;
    // Use this for initialization
    void Start () {
        tutCount = 0;
        tutCurr = GameObject.Find("TutorialImage").GetComponent<Image>();
        currCut = GameObject.Find("CurrCut").GetComponent<Image>();
        nextCut = GameObject.Find("NextCut").GetComponent<Image>();
        nextText = GameObject.Find("Next").GetComponent<Text>();
        backText = GameObject.Find("Back").GetComponent<Text>();
        totalImages = 5;
        tutCount = 0;
        totalCutCount = 9;
        tutImages = new Sprite[totalImages];
        cutImages = new Sprite[totalCutCount];
        cutTimer = 0;
        playing = true;
        cutLength = 2.0f;
        cutCount = 0;
        window = 1;
        for(int i = 0; i < totalImages; i++)
        {
            tutImages[i] = Resources.Load<Sprite>("Tutorial/Tutorial/tut" + (i+1));
        }

        for (int i = 0; i < totalCutCount; i++)
        {
            cutImages[i] = Resources.Load<Sprite>("Cutscenes/Cutscenes/" + (i + 1));
        }

        tutCurr.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //cutscene
        if(playing)
        {
            backText.gameObject.SetActive(false);
            nextText.text = "Skip";
            cutTimer += Time.deltaTime;
            if(cutTimer > cutLength)
            {
                currCut.CrossFadeAlpha(0.0f, 0.4f, true);
                nextCut.CrossFadeAlpha(1.0f, 0.001f, true);
                cutCount++;
                cutTimer = 0;
            }

            if(cutCount < totalCutCount)
            {
                if (cutTimer > window)
                {
                    Sprite s = cutImages[cutCount];
                    currCut.sprite = s;
                    if (cutCount < totalCutCount-1)
                        nextCut.sprite = cutImages[cutCount + 1];

                    currCut.CrossFadeAlpha(1.0f, 0.0000001f, true);
                    nextCut.CrossFadeAlpha(0.0f, 0.0000001f, true);
                }

            }
            else
            {
                tutCurr.gameObject.SetActive(true);
                currCut.CrossFadeAlpha(0.0f, 1.0f, true);
                nextCut.gameObject.SetActive(false);
                playing = false;
                backText.gameObject.SetActive(true);
                nextText.text = "Next";
                GameObject.Find("ACut").SetActive(false);
            }

            if(Input.GetButtonDown("A") || Input.GetMouseButtonDown(0) || Input.GetButtonDown("B") || Input.GetMouseButtonDown(1))
            {
                tutCurr.gameObject.SetActive(true);
                currCut.gameObject.SetActive(false);
                nextCut.gameObject.SetActive(false);
                playing = false;
                backText.gameObject.SetActive(true);
                nextText.text = "Next";
                GameObject.Find("ACut").SetActive(false);
            }
        }
        //go forward
        if ((Input.GetButtonDown("A") || Input.GetMouseButtonDown(0)) && !playing)
        {
            tutCount++;

            if(tutCount >= totalImages-1)
            {
                //load scene
                changeImage(tutCount);
                SceneManager.LoadScene(1);
            }
            else
            {
                //load next image
                changeImage(tutCount);

                if (tutCount == totalImages - 2)
                    nextText.text = "StartGame";
            }
        }

            //go back
        if ((Input.GetButtonDown("B") || Input.GetMouseButtonDown(1)) && !playing)
        {
            if (tutCount > 0)
            {
                tutCount--;
                changeImage(tutCount);
                nextText.text = "Next";
                if (tutCount == 0)
                    backText.text = "Main Menu";
            }
            else
            {
                //load start screne
                SceneManager.LoadScene(0);
            }
        }

    }

    void changeImage(int i)
    {
        tutCurr.sprite = tutImages[i];
    }
}
