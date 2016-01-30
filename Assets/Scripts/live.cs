using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class live : MonoBehaviour {
    public uint hp;
    private Text scoreUI;

    public void decreaseHP()
    {
        hp--;
        scoreUI.text = "HP left: " + hp;
        if (hp <= 0)
        {
            GameObject.FindGameObjectWithTag("end").GetComponent<Text>().text = " YOU LOST !" ;
            Time.timeScale = 0;

        }
    }

    // Use this for initialization
    void Start ()
    {
        hp = 50;
        scoreUI = GameObject.FindGameObjectWithTag("hp").GetComponent<Text>();
        scoreUI.text = "HP left: " + hp;
    }

    // Update is called once per frame
    void Update () {
	   
	}
}
