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
  }
}
