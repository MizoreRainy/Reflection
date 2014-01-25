using UnityEngine;
using System.Collections;

public class StormFX : MonoBehaviour {




	public AudioSource stormAudio;

	public AudioClip [] stormClips;

	// Use this for initialization
	void Start () {
		InvokeRepeating("PlayRandomSound",2,2);
	}
	



	private void PlayRandomSound(){

		if(Random.Range(1,2) ==  1 )
		{
			//animation.Play("stormAnim");
			stormAudio.clip = stormClips[Random.Range(0,stormClips.Length)];
			stormAudio.Play();

		}


		
		}


	}


