using UnityEngine;
using System.Collections;

public class GoatThrower : MonoBehaviour {

	public Vector2 minforce;
	public Vector2 maxforce;
	public float mintime;
	public float maxtime;
	public float percentDemon;

	public float ydeviation;

	public Vector3 torque;

	public GameObject goatprefab;
	public GameObject demonprefab;

	public AudioSource goatsoundgood;
	public AudioSource goatsoundevil;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ThrowInterval");
	}

	void ThrowGoat() {
		float ydev = Random.Range (-ydeviation, ydeviation);

		Vector3 startpos = new Vector3 (transform.position.x, transform.position.y + ydev, transform.position.z);

		GameObject obj;
		if (Random.Range(0.0f, 1.0f) < percentDemon) {
			obj = (GameObject)Instantiate (demonprefab, startpos, demonprefab.transform.rotation);
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = goatsoundevil.clip;
			audioSource.volume = 0.3f;
			audioSource.Play();
		} else {
			obj= (GameObject)Instantiate (goatprefab, startpos, goatprefab.transform.rotation);
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = goatsoundgood.clip;
			audioSource.volume = 0.2f;
			audioSource.Play();
		}
		Vector3 force = new Vector3(Random.Range(minforce.x, maxforce.x), Random.Range(minforce.y, maxforce.y));
		obj.GetComponent<Rigidbody> ().AddForce (force);
		obj.GetComponent<Rigidbody> ().AddTorque (torque);
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
