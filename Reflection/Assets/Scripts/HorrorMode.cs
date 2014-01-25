using UnityEngine;
using System.Collections;

public class HorrorMode : MonoBehaviour {

	public Animation horror;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			if(Input.GetKeyDown(KeyCode.O)){
			HorrorModeStatus(true);
		}

		if(Input.GetKeyDown(KeyCode.P)){
			HorrorModeStatus(false);
		}
	}

	public void HorrorModeStatus(bool status){
		if(status){
			horror.Play("horrorOn");
		}else{
			horror.Play("horrorOff");
		}
	}
}
