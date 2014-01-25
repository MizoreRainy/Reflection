using UnityEngine;
using System.Collections;

public class PlaySoundRandomly : MonoBehaviour {
	public AudioSource audioSource;
	public AudioClip [] soundsToPlay;

	private void OnTriggerEnter(Collider coll) {
		if (coll.collider.gameObject.tag == "Player") {
			int indexToPlay = Random.Range(0, soundsToPlay.Length);
			Debug.Log("Randomly Playing Sound " + indexToPlay);
			audioSource.clip = soundsToPlay[indexToPlay];
			audioSource.Play();
		}
	}
}
