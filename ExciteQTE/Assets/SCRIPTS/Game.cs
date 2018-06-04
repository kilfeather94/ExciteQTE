using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	
	void Start () {
		
	}
	

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            float score = Random.Range(0, 2000);
            string username = "";
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                username += alphabet[Random.Range(0, alphabet.Length)];


            }

            Highscores.AddNewHighscore(username, score);
        }
	}
}
