using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class ButtonManager : MonoBehaviour {

    public Sprite[] buttons;

    public AudioClip[] sounds;

    private AudioSource soundManager;

    public AudioSource music;

    public Slider timeSlider;
    public RectTransform newfillRect;
    public float timeAmt; 
    float time; 


    public Text sliderScoreText;

    private int sequenceNum;
    private int buttonPresses;

  
    private int pointNum = 1000;

    private Image buttonImg;

    ScoreManager scoreManage;

    Highscores highscores;

    Animator anim;

    [SerializeField]
    public InputDevice device;
 
    public GameObject GameOverobj;

    public GameObject[] ScoreTexts;


    const int sliderValue = 100;

    public bool canPlay;
    public bool gameEnd;


    void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        buttonImg = GetComponent<Image>();
        scoreManage = FindObjectOfType<ScoreManager>();
        anim = GetComponent<Animator>();

        device = InputManager.ActiveDevice;

    }

    void Start()
    {
        time = timeAmt;
        timeSlider.value = sliderValue;
    
        ImageLoad(); 
    }

  
	
	void Update ()
    {
        QTEMode();
    }

    
 

    public void BeepEvent()
    {
        soundManager.PlayOneShot(sounds[0]);
    }

    void ImageLoad()
    {
        if(scoreManage.difficultyLvl >= ScoreManager.Difficulty.Middle)
        {
            buttonImg.sprite = buttons[Random.Range(0, buttons.Length)];
        }
        else
        {
            buttonImg.sprite = buttons[Random.Range(0, 4)];
        }
       
    }


    void QTEMode()
    {

       if(canPlay)
        {
            if (InputManager.ActiveDevice.Action2.WasPressed && buttonImg.sprite == buttons[0])
            {
                CorrectInput();
            }
                   
            else if (InputManager.ActiveDevice.Action2.WasPressed && buttonImg.sprite != buttons[0])
            {
                WrongInput();
            }

            if (InputManager.ActiveDevice.Action1.WasPressed && buttonImg.sprite == buttons[1])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.Action1.WasPressed && buttonImg.sprite != buttons[1])
            {
                WrongInput();
            }

            if (InputManager.ActiveDevice.Action3.WasPressed && buttonImg.sprite == buttons[2])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.Action3.WasPressed && buttonImg.sprite != buttons[2])
            {
                WrongInput();
            }

            if (InputManager.ActiveDevice.Action4.WasPressed && buttonImg.sprite == buttons[3])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.Action4.WasPressed && buttonImg.sprite != buttons[3])
            {
                WrongInput();
            }


            if (InputManager.ActiveDevice.DPadUp.WasPressed && buttonImg.sprite == buttons[4])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.DPadUp.WasPressed && buttonImg.sprite != buttons[4])
            {
                WrongInput();
            }


            if (InputManager.ActiveDevice.DPadDown.WasPressed && buttonImg.sprite == buttons[5])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.DPadDown.WasPressed && buttonImg.sprite != buttons[5])
            {
                WrongInput();
            }


            if (InputManager.ActiveDevice.DPadLeft.WasPressed && buttonImg.sprite == buttons[6])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.DPadLeft.WasPressed && buttonImg.sprite != buttons[6])
            {
                WrongInput();
            }



            if (InputManager.ActiveDevice.DPadRight.WasPressed && buttonImg.sprite == buttons[7])
            {
                CorrectInput();
            }

            else if (InputManager.ActiveDevice.DPadRight.WasPressed && buttonImg.sprite != buttons[7])
            {
                WrongInput();
            }

            Timer();
           
        }

        else
        {
            Debug.Log("Button Not active");
        }
    }


    //Button input matches button icon on screen
    private void CorrectInput()
    {
        soundManager.PlayOneShot(sounds[1]);

        if (buttonPresses <= 0)
        {
            StartCoroutine(Correct());  //fire method to pause, before loading new image (ienumerator) Play correct sound //gain points
        }
        else
        {
            buttonPresses--;
            scoreManage.pointsToBeAdded += pointNum + timeSlider.value;
            timeSlider.value = 100;
            ImageLoad();
            time = timeAmt;
        }

    }

    //Button input does not match button icon on screen
    private void WrongInput()
    {
        if (scoreManage.lives <= 1)
        {
            StartCoroutine(GameOver());

        }
        else
        {
            StartCoroutine(Wrong());
        }
    }








    void Timer()
    {
        if (time > 0)
        {
            timeSlider.value -= timeAmt;   
        }

        if(timeSlider.value <= 0)
        {
            if (scoreManage.lives <= 1)
            {
                StartCoroutine(GameOver());
               
            }
            else
            {
                StartCoroutine(Wrong());
                
            }
        }
    }

    void SequenceChooser()
    {
        sequenceNum = Random.Range(1, 5);
        buttonPresses = sequenceNum;
    }


    void ButtonAnimToggle(bool enabled)
    {
        if (enabled)
        {
            buttonImg.enabled = true;
            anim.enabled = true;
           
        }
        else if(!enabled)
        {
            buttonImg.enabled = false;
            anim.enabled = false;
        }
       
    }

  public void Init() // refresh status of variables e.g. timer, canPlay boolean, button toggle
    {
        time = timeAmt;
        timeSlider.value = 100;
        sliderScoreText.text = "";
        canPlay = true;
        ButtonAnimToggle(enabled);

        if (gameEnd)
        {
            scoreManage.ScoreTextRefresh();
            scoreManage.mid = false;
            scoreManage.sup = false;
            scoreManage.exp = false;
            scoreManage.ScoreChecker();
            timeSlider.gameObject.SetActive(true);
            gameEnd = false;
            ImageLoad();

            scoreManage.lives = 3;
            scoreManage.lifeIndex = scoreManage.lives;
            foreach(Image img in scoreManage.lifeImgs)
            {
                img.gameObject.SetActive(true);
            }
            music.Play();
            scoreManage.ScoreTextHideShow(true);
            
        }
    }


 

    IEnumerator Correct()
    {

        if(scoreManage.difficultyLvl <= ScoreManager.Difficulty.Middle)
        {
            scoreManage.Score += pointNum + timeSlider.value;
        }
        else
        {
            scoreManage.Score += scoreManage.pointsToBeAdded;
        }
         
        scoreManage.ScoreChecker();
        scoreManage.SetScoreText();
        soundManager.PlayOneShot(sounds[1]);
        sliderScoreText.text = "+ " + timeSlider.value;
        canPlay = false; // NEW
        ButtonAnimToggle(!enabled);
        ImageLoad();
        scoreManage.pointsToBeAdded = 0f;
        if(scoreManage.difficultyLvl >= ScoreManager.Difficulty.Expert)
        {
           SequenceChooser();
        }
        yield return new WaitForSeconds(1.5f);

        Init();

    }

    IEnumerator Wrong()
    {
        
        music.Stop(); 
        scoreManage.lives--;
        scoreManage.lifeIndex = scoreManage.lives;
        soundManager.PlayOneShot(sounds[2]);
        ButtonAnimToggle(!enabled);
        canPlay = false;
        ImageLoad();
        scoreManage.pointsToBeAdded = 0f;
        yield return new WaitForSeconds(2.0f);
        soundManager.PlayOneShot(sounds[3]);
        scoreManage.lifeImgs[scoreManage.lifeIndex].GetComponent<Animation>().Play();
        yield return new WaitForSeconds(4f);
        scoreManage.lifeImgs[scoreManage.lifeIndex].gameObject.SetActive(false);     
        music.Play();

        Init();
        
    }


    IEnumerator GameOver()
    {
        Debug.Log("Game Over pal");
        scoreManage.lives--;
        ButtonAnimToggle(!enabled);
        canPlay = false;
        music.Stop();
        soundManager.PlayOneShot(sounds[2]); 
        yield return new WaitForSeconds(2.0f);
        soundManager.PlayOneShot(sounds[3]);
        scoreManage.lifeImgs[0].GetComponent<Animation>().Play(); //animate life icon
        yield return new WaitForSeconds(4f);
        scoreManage.lifeImgs[0].gameObject.SetActive(false);
        GameOverobj.SetActive(true);
        timeSlider.gameObject.SetActive(false);
        scoreManage.HiScoreCheckEnd();
        gameEnd = true;
   
    }




  
    

}
