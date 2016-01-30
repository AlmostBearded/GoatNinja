using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject prefab = GameObject.Find ("Goat");
		Instantiate (prefab, new Vector3(0, 4, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
