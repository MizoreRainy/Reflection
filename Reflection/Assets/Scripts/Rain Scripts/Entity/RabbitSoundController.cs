using UnityEngine;
using System.Collections;

public enum SoundType
{
	idle		=	0,
	attack		=	1,
	givebullet	=	2,
	dead		=	3
}

public class RabbitSoundController : MonoBehaviour 
{
	//------------------------------------------------------------------------

	public	AudioSource		source;
	public	AudioClip[]		soundList;

	//------------------------------------------------------------------------

	public void PlaySound (SoundType _soundType) 
	{
		if(soundList.Length != 4)
			return;

		source.clip	=	soundList[ (int)_soundType ];
		source.audio.Play();
	}
	
	//------------------------------------------------------------------------
}
