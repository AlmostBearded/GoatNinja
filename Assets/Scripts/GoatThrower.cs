using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject prefab = GameObject.Find ("Goat");
		GameObject obj = (GameObject)Instantiate (prefab, new Vector3(0, 4, 0), Quaternion.identity);
		obj.GetComponent<Rigidbody> ().AddForce (new Vector3 (100, 0, 0));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
