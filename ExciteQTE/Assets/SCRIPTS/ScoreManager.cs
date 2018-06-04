using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text ScoreText;
    public Text lastScoreText;
    public Text hiscoreText;
    public float CountInterval = 0.05f, timer = 0;
    public float Score = 10, i = 0;  // was int
    public float hiscore; // current hi-score
    public float prevHiScore;
    public int lives = 3;
    public int lifeIndex;


    public int MiddleScore;
    public int ExpertScore;
    public int SupExpertScore;

    public Image[] lifeImgs;

  
    public AudioClip scorefx;

    int skillLvl;

    string username;

    UIController UIc;
   
    public Image sliderFill;
    public Text difficultyText;

   public ButtonManager btnManager;

    public GameObject SoundFXGO;

    public float pointsToBeAdded;

   public bool mid, exp, sup = false;

    Highscores highscores;


    public enum Difficulty
    {
        Beginner,
        Middle,
        Expert,
        Super_Expert
    }

    public Difficulty difficultyLvl;




    // Use this for initialization
    void Awake()
    {
       
        lifeIndex = lives;
        
	}

    void Start()
    {
        highscores = FindObjectOfType<Highscores>();
        UIc = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    
	
	// Update is called once per frame
	void Update ()
    {
        //   timer += Time.deltaTime;

        /*
        if (i < Score && timer >= CountInterval)
        {
            i++;
            scoreSound.PlayOneShot(scorefx);
            ScoreText.text = i.ToString();
            timer = 0;
        }
        */
      
            DifficultyChecker();
        
   

    }

    public void ScoreTextRefresh()
    {
    
        prevHiScore = highscores.highscoresList[0].score;  
        hiscoreText.text = prevHiScore.ToString();
        lastScoreText.text = prevHiScore.ToString();
        Score = 0;
        ScoreText.text = Score.ToString();
    }


    void RandomNameGenerator()
    {
        username = "";
        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            username += alphabet[Random.Range(0, alphabet.Length)];


        }
    }




    public void HiScoreCheckEnd()  // checks hi-score value at Game Over
    {

        //RandomNameGenerator();
      //  Highscores.AddNewHighscore(username, Score);

        UIc.input.gameObject.SetActive(true);
        UIc.FlashingText.gameObject.SetActive(false);
        UIc.hiscoredisplaybtn.gameObject.SetActive(false);

        //TODO: score submit with name

        if (Score > highscores.highscoresList[0].score)
        {
            Debug.Log("Hi Score Achieved");
            //play victory sound
            SoundFXGO.GetComponent<AudioSource>().PlayOneShot(SoundFXGO.GetComponent<Sounds>().soundfx[5]);
            //flash text

            hiscoreText.text = highscores.highscoresList[0].score.ToString();

            hiscoreText.transform.parent.gameObject.SetActive(true);
            ScoreText.transform.parent.gameObject.SetActive(false);
            lastScoreText.transform.parent.gameObject.SetActive(false);
        //    ScoreTextRefresh(); -- was originally uncommented


        }
        else
        {
            Debug.Log("No hi score");

            // ScoreTextHideShow(false); -- originally uncommented
            hiscoreText.transform.parent.gameObject.SetActive(false);
            ScoreText.transform.parent.gameObject.SetActive(true);
            lastScoreText.transform.parent.gameObject.SetActive(false);
            //  ScoreTextRefresh(); -- was originally uncommented


        }
    }


    public void ScoreTextHideShow(bool show)
    {
        if (show == false)
        {
            ScoreText.transform.parent.gameObject.SetActive(false);
            lastScoreText.transform.parent.gameObject.SetActive(false);
            hiscoreText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            ScoreText.transform.parent.gameObject.SetActive(true);
            lastScoreText.transform.parent.gameObject.SetActive(true);
            hiscoreText.transform.parent.gameObject.SetActive(true);
        }
    }

    public  void ScoreChecker()
    {
       

        if (Score < MiddleScore) //63000
        {
            difficultyLvl = Difficulty.Beginner;
           
        }

        if (Score >= MiddleScore && Score < ExpertScore) // 96000
        {
          
            if (!mid)
            {
                difficultyLvl = Difficulty.Middle;
                SoundFXGO.GetComponent<AudioSource>().PlayOneShot(SoundFXGO.GetComponent<Sounds>().soundfx[4]);
                mid = true;
            }
        }

        if (Score >= ExpertScore && Score < SupExpertScore) //254000
        {
            if (!exp)
            {
                difficultyLvl = Difficulty.Expert;
                SoundFXGO.GetComponent<AudioSource>().PlayOneShot(SoundFXGO.GetComponent<Sounds>().soundfx[4]);
                exp = true;
            }
        }

        if (Score >= SupExpertScore)
        {
            if (!sup)
            {
                difficultyLvl = Difficulty.Super_Expert;
                SoundFXGO.GetComponent<AudioSource>().PlayOneShot(SoundFXGO.GetComponent<Sounds>().soundfx[4]);
                sup = true;
            }
            
        }

    }


  public void DifficultyChecker()
    {
        switch (difficultyLvl)
        {
            case Difficulty.Beginner:
                sliderFill.color = new Color(0, 255, 255);
                difficultyText.text = "BEGINNER";
                difficultyText.color = new Color(0, 255, 255);
                btnManager.timeAmt = 1.25f;           
                break;
            case Difficulty.Middle:
                sliderFill.color = new Color(230, 255, 0);
                difficultyText.text = "MIDDLE";
                difficultyText.color = new Color(230, 255, 0);
              
                btnManager.timeAmt = 2f;
                break;
            case Difficulty.Expert:
                sliderFill.color = new Color(255, 0, 0);
                difficultyText.text = "EXPERT";
                difficultyText.color = new Color(255, 0, 0);
           
                btnManager.timeAmt = 2.5f;
                break;
            case Difficulty.Super_Expert:
                sliderFill.color = new Color(250, 0, 255);
                difficultyText.text = "SUPER EXPERT";
                difficultyText.color = new Color(250, 0, 255);
              
                btnManager.timeAmt = 2.8f;
                break;         
        }
    }


  

    public void SetScoreText()
    {
        ScoreText.text = Score.ToString();

        
   //     if (Score > highscores.highscoresList[0].score) // > prevHiScore
        if(Score > prevHiScore)
        {
            hiscore = Score;
            hiscoreText.text = hiscore.ToString();
           

           // Highscores.AddNewHighscore("TEST", Score);
        }
        

    }
}
