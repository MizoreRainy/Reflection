using UnityEngine;
using System.Collections;

public class OnOff : MonoBehaviour {
	public GameObject triggerTarget = null;

	public void OnTriggerEnter(Collider coll) {
		GameObject target;
		if (triggerTarget) {
			target = triggerTarget;
		} else {
			target = transform.parent.gameObject;
		}

		target.SendMessage ("On");
	}

	public void OnTriggerExit(Collider coll) {
		GameObject target;
		if (triggerTarget) {
			target = triggerTarget;
		} else {
			target = transform.parent.gameObject;
		}

		target.SendMessage ("Off");
	}
}
