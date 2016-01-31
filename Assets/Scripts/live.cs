using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class live : MonoBehaviour {
    public uint hp;
    private Text scoreUI;

    public void decreaseHP()
    {
        hp--;
        scoreUI.text = "HP left: " + hp;
        if (hp <= 0)
        {
           // GameObject.FindGameObjectWithTag("end").GetComponent<Text>().text = " YOU LOST !" ;
			SceneManager.LoadScene ("Menu");
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
