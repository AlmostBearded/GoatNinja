using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class live : MonoBehaviour {
    public uint hp;
    private Text scoreUI;
	public AudioSource gameover;
	public AudioSource failSound;

	 IEnumerator noob() {
		GameObject texture = GameObject.FindGameObjectWithTag ("Error");
		texture.GetComponent<Image>().color = new Color (0.15625f, 1, 0, 0.3f);
		yield return new WaitForSeconds(0.07f);
		//yield return new WaitForEndOfFrame ();
		texture.GetComponent<Image>().color = new Color (1, 1, 1, 0);
	}

	IEnumerator sshake() {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		Vector3 campos = camera.transform.position;
		float anim = 0.0f;
		while (anim < 0.3f) {
			anim += Time.deltaTime;
			float x = Random.Range (-0.2f, 0.2f);
			float y = Random.Range (-0.2f, 0.2f);
			camera.transform.position = new Vector3 (campos.x + x, campos.y + y, campos.z);

			yield return null;
		}

		camera.transform.position = campos;
	}

    public void decreaseHP()
    {
        hp--;
        scoreUI.text = "HP: " + hp;
		if (hp > 0) {
			StartCoroutine ("noob");
			StartCoroutine ("sshake");
			failSound.Play ();
		}
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
			GameObject.FindGameObjectWithTag ("Score").SetActive (false);
			hp = 0;
			Debug.Log (currentHighscore + "current high");
			Debug.Log (lastScore + "last Score");
			if (lastScore > currentHighscore) {
				PlayerPrefs.SetInt ("highscore", lastScore);
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = 
					"Game over!\nCongratz New Highscore: " + lastScore + 
					"\nPress Space to Restart - Press Escape to Quit";
				//GameObject.FindGameObjectWithTag ("end").GetComponent<Text> ().color = Color.red;
				//Debug.Log ("Congratz new highscore: " + lastScore);
			} else {
				GameObject.FindGameObjectWithTag("end").GetComponent<Text> ().text = 
					"Game over!\nYour Score: " + lastScore + 
					"\nCurrent Highscore: " + currentHighscore + 
					"\nPress Space to Restart - Press Escape to Quit";
				//GameObject.FindGameObjectWithTag ("end").GetComponent<Text> ().color = Color.white;
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
