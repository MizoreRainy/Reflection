using UnityEngine;
using System.Collections;

public class IntroEvent : MonoBehaviour {

	public void StartGame(){

		Application.LoadLevel("Instruction");
	}

	public void ExitGame(){
		Application.Quit();
	}


}
