using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  void Start()
  {
  }

  void Update()
  {

    if (Input.GetKeyDown("escape"))
    {
      Application.Quit();
    }

    if (Input.GetKeyDown("space"))
    {
      SceneManager.LoadScene("Main");
    }

    if (Input.GetKeyDown(KeyCode.S))
    {
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().GetComponent<sshake>().shakeIt(1);
    }

  }

}
