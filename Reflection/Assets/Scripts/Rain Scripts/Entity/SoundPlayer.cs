using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SimpleSoundController.instance.PlayBGM("menu");
	}
	
	// Update is called once per frame
	void OnLevelWasLoaded (int _level) 
	{
		if(Application.loadedLevelName.Equals("StartScene"))
		{
			SimpleSoundController.instance.PlayBGM("menu");
		}
		else if(Application.loadedLevelName.Equals("test"))
		{
			SimpleSoundController.instance.PlayBGM("gameplay");
		}
	}
}
