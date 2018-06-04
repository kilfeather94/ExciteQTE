using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InControl;

public class UIController : MonoBehaviour
{

    public GameObject[] UIObjects;

    public AudioClip selectSound;

    public InputField input;

    ScoreManager scoreManage;

    public InputDevice device;

    public ButtonManager btnManager;

    public Text HiScoreDisplayText;
    public Text FlashingText;
    public Image hiscoredisplaybtn;

    Highscores highscores;

    public Button ScoreSubmitbtn;




    // Use this for initialization
    void Awake()
    {       
        device = InputManager.ActiveDevice;
        scoreManage = FindObjectOfType<ScoreManager>();
        highscores = FindObjectOfType<Highscores>();
    }

    private void Update()
    {
        if(UIObjects[2].gameObject.activeInHierarchy) // if 'MainMenuObjects' is active
        {
            if (InputManager.ActiveDevice.MenuWasPressed)
            {
                UIObjects[2].SetActive(false);
                UIObjects[3].SetActive(true);
                //  scoreManage.SetAllScores();  setting last score
                scoreManage.ScoreTextHideShow(true);
               scoreManage.prevHiScore = highscores.highscoresList[0].score;
                scoreManage.hiscoreText.text = scoreManage.prevHiScore.ToString();
                scoreManage.lastScoreText.text = scoreManage.prevHiScore.ToString();
            }
        }


        //If in Game over state
        if(InputManager.ActiveDevice.MenuWasPressed && btnManager.gameEnd && UIObjects[0].activeInHierarchy == false && FlashingText.gameObject.activeInHierarchy == true)
        {
            UIObjects[1].SetActive(false); // disable 'Game Over Menu'
            btnManager.Init();
            

        }

        if (InputManager.ActiveDevice.Action4.WasPressed && btnManager.gameEnd && input.gameObject.activeInHierarchy == false && hiscoredisplaybtn.gameObject.activeInHierarchy == true)
        {
           if(UIObjects[1].GetComponent<Text>().enabled)
            {
                UIObjects[1].GetComponent<Text>().enabled = false;
                FlashingText.gameObject.SetActive(false);
                UIObjects[0].SetActive(true);
                HiScoreDisplayText.text = "Back";
            }
           else if(UIObjects[0].activeInHierarchy)
            {
                UIObjects[0].SetActive(false);
                UIObjects[1].GetComponent<Text>().enabled = true;
                if (input.gameObject.activeInHierarchy == false)
                {
                    FlashingText.gameObject.SetActive(true);
                }
                HiScoreDisplayText.text = "Hi-scores";
            }

        }
    }



    public void ViewLeaderboard(BaseEventData eventData)
    {
        UIObjects[0].SetActive(true);
        UIObjects[1].SetActive(false);

        foreach(GameObject ui in UIObjects)
        {
            if(ui.gameObject.tag == "Leaderboard")
            {
                ui.SetActive(true);
            }
            else
            {
                ui.SetActive(false);
            }
        }

    }


    public void GetUsernameInput(string username)
    {
        Debug.Log("Username submitted: " + username);
        input.text = "";

     //   Highscores.AddNewHighscore(username, scoreManage.Score);
    }

    public void SubmitScore(string name)
    {
        if (input.text != "")
        {
            name = input.text;
            Highscores.AddNewHighscore(name, scoreManage.Score);
            input.text = "";
            input.gameObject.SetActive(false);
            FlashingText.gameObject.SetActive(true);
            hiscoredisplaybtn.gameObject.SetActive(true);
        }
    }




}
