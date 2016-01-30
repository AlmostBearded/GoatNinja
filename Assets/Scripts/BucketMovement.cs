using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BucketMovement : MonoBehaviour {

	private GameObject bucket;
	private Vector3 leftpoint;
	private Vector3 rightpoint;
	private bool goRight;
	private float speed;

	private int counter = 0;
	private Text scoreUI;

	// Use this for initialization
	void Start () {
		bucket = GameObject.FindGameObjectWithTag ("Bucket");
		leftpoint = new Vector3 (-10, 0, 0);
		rightpoint = new Vector3 (10, 0, 0); 
		goRight = true;
		speed = 0.1f;
		scoreUI = GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>();

	}
		void FixedUpdate () {
		Vector3 curr = bucket.transform.position;

		if (goRight)
			bucket.transform.position += new Vector3 (speed, 0, 0);
		else
			bucket.transform.position -= new Vector3 (speed, 0, 0);

		if (curr.x > rightpoint.x)
			goRight = false;

		if (curr.x < leftpoint.x)
			goRight = true;

        scoreUI.text = "Score: " + counter;


    }

	void OnParticleCollision(GameObject other) {
		counter++;
		Debug.Log ("Hit" + counter);
	}
}
