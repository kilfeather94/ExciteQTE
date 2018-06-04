using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{

    public Text[] highscoreText;
    Highscores highscoreManager;

    

	void Start ()
    {
		
        for(int i = 0; i < highscoreText.Length; i ++)
        {
            highscoreText[i].text = i + 1 + ". Fetching...";
        }

        highscoreManager = GetComponent<Highscores>();

        StartCoroutine("RefreshHighScores");
	}


    public void OnHighscoresDownloaded(Highscores.Highscore[] highscoreList)
    {
        for(int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if(highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }


    IEnumerator RefreshHighScores()
    {
        while(true)
        {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
	

	void Update ()
    {
		
	}


}
