using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BucketMovement : MonoBehaviour
{

  private GameObject bucket;
  private Vector3 leftpoint;
  private Vector3 rightpoint;
  private bool goRight;
  private float speed;

  private int counter = 0;
  private Text scoreUI;
  private Plane playArea;

  public void Awake()
  {
    playArea = new Plane(new Vector3(0, 0, -1), 0);
    bucket = GameObject.FindGameObjectWithTag("Bucket");
    leftpoint = new Vector3(-10, 0, 0);
    rightpoint = new Vector3(10, 0, 0);
    goRight = true;
    speed = 0.08f;
    scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
  }

  void FixedUpdate()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    float playAreaDistance;
    playArea.Raycast(ray, out playAreaDistance);
    Vector3 playAreaIntersection = ray.GetPoint(playAreaDistance);


    Vector3 curr = bucket.transform.position;

    float rightPoint = playAreaIntersection.x + 2;
    float leftPoint = playAreaIntersection.x - 2;

    if (bucket.transform.position.x >= rightPoint)
      goRight = false;

    if (bucket.transform.position.x < leftPoint)
      goRight = true;

    bucket.transform.position += new Vector3(goRight ? speed : -speed, 0, 0);

    //if (curr.x > rightpoint.x)
    //  goRight = false;

    //if (curr.x < leftpoint.x)
    //  goRight = true;

    scoreUI.text = "Score: " + counter;
    PlayerPrefs.SetInt("lastscore", counter);


  }

  void OnParticleCollision(GameObject other)
  {
    counter++;
    //Debug.Log ("Hit" + counter);
  }
}
