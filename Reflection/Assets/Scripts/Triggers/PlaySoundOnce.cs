using UnityEngine;
using System.Collections;

public class PlaySoundOnce : MonoBehaviour {
	public AudioSource soundToPlay;
	private bool soundAlreadyPlayed = false;

	private void OnTriggerEnter(Collider coll) {
		if (coll.collider.gameObject.tag == "Player" && soundAlreadyPlayed == false) {
			Debug.Log("Playing Sound once");
			soundToPlay.Play();
			soundAlreadyPlayed = true;
		}
	}
}
