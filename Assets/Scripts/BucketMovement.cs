using UnityEngine;
using System.Collections;

public class BucketMovement : MonoBehaviour {

	private GameObject bucket;
	private Vector3 leftpoint;
	private Vector3 rightpoint;
	private bool goRight;
	private float speed;

	private int counter = 0;

	// Use this for initialization
	void Start () {
		bucket = GameObject.FindGameObjectWithTag ("Bucket");
		leftpoint = new Vector3 (-10, 0, 0);
		rightpoint = new Vector3 (10, 0, 0); 
		goRight = true;
		speed = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curr = bucket.transform.position;

		if (goRight)
			bucket.transform.position += new Vector3 (speed, 0, 0);
			//bucket.transform.Translate (speed, 0, 0, Space.World);
		else
			bucket.transform.position -= new Vector3 (speed, 0, 0);

		if (curr.x > rightpoint.x)
			goRight = false;

		if (curr.x < leftpoint.x)
			goRight = true;

		if (Input.GetKeyDown ("space")) {
			Debug.Log ("Bucket emptied");
			// TODO: play animation
			counter = 0;
		}
	}

	void OnParticleCollision(GameObject other) {
		counter++;
		Debug.Log ("Hit" + counter);
		Renderer rend = bucket.GetComponentInChildren<Renderer> ();
		//rend.material.GetTexture
		//rend.material.SetColor("_Color", new Color(counter/255.0f, 0, 0));
	}
}
