using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
	public AudioSource sound1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider coll) {
		if (coll.collider.gameObject.tag == "Player") {
			Debug.Log ("ok");
			sound1.Play();
		}
	}
}
