using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
	public AudioSource soundToPlay;

	private void OnTriggerEnter(Collider coll) {
		if (coll.collider.gameObject.tag == "Player") {
			Debug.Log("Playing Sound");
			soundToPlay.Play();
		}
	}
}
