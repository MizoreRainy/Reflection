using UnityEngine;
using System.Collections;

public class VisibilityChecker : MonoBehaviour {
	public GameObject visibilityTarget; // Enemy
	public AudioSource audioPlayedWhenVisible;
	public bool onlyOnce = true;
	private bool alreadyTriggered = false; // 
	
	// Update is called once per frame
	void Update () {
		if (onlyOnce == true && alreadyTriggered == false) {
			Vector3 here = transform.position;

			// if target exists and in in range of the camera
			if (visibilityTarget && visibilityTarget.renderer.isVisible) {
				// do a linecast
				Vector3 pos = visibilityTarget.transform.position;
				RaycastHit hit = new RaycastHit ();
				bool linecastResult = Physics.Linecast (here, pos, out hit);

				// if nothing is bobscuring the target ...
				if (!linecastResult) { //?  && hit.transform == visibilityTarget.transform
					Debug.Log ("Target Visible!");
					if (audioPlayedWhenVisible) {
						audioPlayedWhenVisible.Play();
					}
					alreadyTriggered = true;
				}
			}
		}
	}
}
