using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	public Vector3 direction;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ThrowInterval");
	}

	void ThrowGoat() {
		GameObject prefab = GameObject.Find ("Goat");
		GameObject obj = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
		obj.GetComponent<Rigidbody> ().AddForce (direction);
	}

	IEnumerator ThrowInterval() {
		while (true) {
			float waittime = Random.Range(0.4f, 1.3f);
			yield return new WaitForSeconds (waittime);
			ThrowGoat ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
