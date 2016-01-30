using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	public Vector3 direction;
	public GameObject goatprefab;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ThrowInterval");
	}

	void ThrowGoat() {
		GameObject obj = (GameObject)Instantiate (goatprefab, transform.position, Quaternion.identity);
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
