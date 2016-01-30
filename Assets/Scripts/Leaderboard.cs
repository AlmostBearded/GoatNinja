using UnityEngine;
using System.Collections;

public class Leaderboard : MonoBehaviour {

	private int currentHighscore;

	// Use this for initialization
	void Start () {
		currentHighscore = PlayerPrefs.GetInt("highscore", 0); 
	}

	void ReportScore(int score) {

		if (score > currentHighscore) {
			PlayerPrefs.SetInt ("highscore", score);
			Debug.Log ("Congratz new highscore: " + score);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			Debug.Log ("current Highscore: " + currentHighscore);
			ReportScore (150);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {  
			Application.LoadLevel (0);  
		}  
	}
}
