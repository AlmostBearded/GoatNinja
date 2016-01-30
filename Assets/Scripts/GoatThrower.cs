using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	public Vector2 minforce;
	public Vector2 maxforce;
	public GameObject goatprefab;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ThrowInterval");
	}

	void ThrowGoat() {
		GameObject obj = (GameObject)Instantiate (goatprefab, transform.position, Quaternion.identity);
		Vector3 force = new Vector3(Random.Range(minforce.x, maxforce.x), Random.Range(minforce.y, maxforce.y));
		obj.GetComponent<Rigidbody> ().AddForce (force);
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
