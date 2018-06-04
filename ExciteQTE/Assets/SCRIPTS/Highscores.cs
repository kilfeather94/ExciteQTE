using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour {

    const string privateCode = "-veYlPsSHE2hQfrvsSJiWA8kyHe41K3EqJbgdvUXi-ug";
    const string publicCode = "58e4073412e7f10688bdfa21";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    static Highscores instance;
    DisplayHighscores highscoresDisplay;

    void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
    }

    public static void AddNewHighscore(string username, float score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }
    
	IEnumerator UploadNewHighscore(string username, float score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if(string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }



    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }


    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/" );
        yield return www;

        if (string.IsNullOrEmpty(www.error)) // if no error
        {
            FormatHighscores(www.text);
            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }


    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        highscoresList = new Highscore[entries.Length];

        for(int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }

    public struct Highscore
    {
        public string username;
        public float score;

        public Highscore(string _username, float _score)
        {
            username = _username;
            score = _score;
        }
    }

}
