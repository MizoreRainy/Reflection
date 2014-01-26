using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleSoundController : MonoBehaviour {

	public string[]  effectSoundList;
	public AudioClip[] effectList;
	public string[]  bgmSoundList;
	public AudioClip[] bgmList;
	
	private Dictionary<string, AudioClip> _effectList = new Dictionary<string, AudioClip>();
	private Dictionary<string, AudioClip> _musicList = new Dictionary<string, AudioClip>();
	
	public AudioSource effectSoundSource;
	public AudioSource bgmSoundSource;
	
	public static SimpleSoundController instance;
	
	void Awake () {
		DontDestroyOnLoad(gameObject);
		instance = this;
		
		for(int i = 0; i < effectSoundList.Length; i += 1){
			_effectList.Add (effectSoundList[i], effectList[i]);
		}
		
		for(int i = 0; i < bgmSoundList.Length; i += 1){
			_musicList.Add (bgmSoundList[i], bgmList[i]);
		}
	}
	
	public void PlaySoundEffect(string soundName){
		effectSoundSource.PlayOneShot(_effectList[soundName]);
	}
	
	public void PlayBGM(string bgmName){
		bgmSoundSource.Stop();
		bgmSoundSource.clip = _musicList[bgmName];
		bgmSoundSource.Play();	
		
		cachedBGM = bgmName;
	}
	
	private bool _SFXStatus = true;
	public bool SFXStatus{
		get{ return _SFXStatus; }
		set{
			_SFXStatus = value;
			if(_SFXStatus){
				effectSoundSource.volume = 1f;
			}else{
				effectSoundSource.Stop();
				effectSoundSource.volume = 0f;
			}
		}
	}
	private bool _BGMStatus = true;
	public bool BGMStatus{
		get{ return 	_BGMStatus; }
		set{
			_BGMStatus = value;
			if(_BGMStatus){
				bgmSoundSource.volume = 1f;
				if(!cachedBGM.Equals("")){
					PlayBGM(cachedBGM);
				}
			}else{
				bgmSoundSource.Stop();
				bgmSoundSource.volume = 0f;
			}
		}
	}
	private string cachedBGM = "";
}
