using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class live : MonoBehaviour {
    public uint hp;
    private Text scoreUI;
	public AudioSource gameover;

    public void decreaseHP()
    {
        hp--;
        scoreUI.text = "HP left: " + hp;
        if (hp <= 0)
        {
            //GameObject.FindGameObjectWithTag("end").GetComponent<Text>().text = " Game over!" ;
			gameover.Play ();
			GameObject[] throwers = GameObject.FindGameObjectsWithTag ("GoatThrower");
			foreach (GameObject go in throwers) {
				go.SetActive (false);
			}
			GameObject.FindGameObjectWithTag ("Player").SetActive (false);
			int currentHighscore = PlayerPrefs.GetInt ("highscore", 0);
			int lastScore = PlayerPrefs.GetInt ("lastscore", 0);
			if (lastScore > currentHighscore) {
				PlayerPrefs.SetInt ("highscore", lastScore);
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = "Game over! Congratz New Highscore: " + lastScore;
				//Debug.Log ("Congratz new highscore: " + lastScore);
			} else {
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = "Game over! Your Score: " + lastScore + " Current Highscore: " + currentHighscore;
			}
			//SceneManager.LoadScene ("Menu");
        }
    }

    // Use this for initialization
    void Start ()
    {
        hp = 10;
        scoreUI = GameObject.FindGameObjectWithTag("hp").GetComponent<Text>();
        scoreUI.text = "HP left: " + hp;
    }

    // Update is called once per frame
    void Update () {
	   
	}
}
