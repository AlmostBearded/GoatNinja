using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	void Start () {
	}

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("space"))
        {
            Application.LoadLevel("Main");
        }
    }
}
