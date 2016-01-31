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
			Debug.Log (currentHighscore + "current high");
			Debug.Log (lastScore + "last Score");
			if (lastScore > currentHighscore) {
				PlayerPrefs.SetInt ("highscore", lastScore);
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = 
					"Game over!\nCongratz New Highscore: " + lastScore + 
					"\nPress Space to Restart - Press Escape to Quit";
				GameObject.FindGameObjectWithTag ("end").GetComponent<Text> ().color = Color.red;
				//Debug.Log ("Congratz new highscore: " + lastScore);
			} else {
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = 
					"Game over!\nYour Score: " + lastScore + 
					"\nCurrent Highscore: " + currentHighscore + 
					"\nPress Space to Restart - Press Escape to Quit";
				GameObject.FindGameObjectWithTag ("end").GetComponent<Text> ().color = Color.white;
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
