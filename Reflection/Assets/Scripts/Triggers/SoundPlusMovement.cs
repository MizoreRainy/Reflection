using UnityEngine;
using System.Collections;

public class SoundPlusMovement : MonoBehaviour {
	public AudioSource panicscare;
	public GameObject face;
	private bool moveFace = false;
	private Vector3 endTarget = new Vector3(0,0,3);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (moveFace == true) {
			face.transform.position = Vector3.Lerp(face.transform.position, endTarget, 0.2f);
			if (face.transform.position == endTarget) {
				moveFace = false;
			}
		}
	}

	private void OnTriggerEnter(Collider coll) {
		if (coll.collider.gameObject.tag == "Player") {
			Debug.Log("JUMPSCARE!");
			panicscare.Play();

			moveFace = true;
		}
	}
}
