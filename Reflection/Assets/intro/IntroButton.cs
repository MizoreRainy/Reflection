using UnityEngine;
using System.Collections;

public class IntroButton : MonoBehaviour {

	public string msg;
	public GameObject target;

	void OnMouseDown(){
		Debug.Log("S");
		target.SendMessage(msg);
	}
}
