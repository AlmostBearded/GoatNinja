using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	public Vector2 minforce;
	public Vector2 maxforce;
	public float mintime;
	public float maxtime;
	public float percentDemon;

	public GameObject goatprefab;
	public GameObject demonprefab;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ThrowInterval");
	}

	void ThrowGoat() {
		GameObject obj;
		if (Random.Range(0.0f, 1.0f) < percentDemon) {
			obj = (GameObject)Instantiate (demonprefab, transform.position, Quaternion.identity);
		} else {
			obj= (GameObject)Instantiate (goatprefab, transform.position, Quaternion.identity); 
		}
		Vector3 force = new Vector3(Random.Range(minforce.x, maxforce.x), Random.Range(minforce.y, maxforce.y));
		obj.GetComponent<Rigidbody> ().AddForce (force);
	}

	IEnumerator ThrowInterval() {
		while (true) {
			float waittime = Random.Range(mintime, maxtime);
			yield return new WaitForSeconds (waittime);
			ThrowGoat ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
