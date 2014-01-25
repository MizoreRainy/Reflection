using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public ParticleEmitter _1;
	public ParticleEmitter _2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			MakeBullet();
		}
	}

	public void MakeBullet(){
		animation.Play("shoot");
		_1.Emit();
		_2.Emit();
		audio.Play();
		Debug.Log("BOOM");
	}

	public void ResetAnimation(){
		animation.CrossFade("revilverIdle");
		Debug.Log("reload");
	}

}
