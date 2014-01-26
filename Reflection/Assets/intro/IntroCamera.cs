using UnityEngine;
using System.Collections;

public class IntroCamera : MonoBehaviour {
	public Animation _animation;
	public Animator animator;
	// Update is called once per frame
	void StartGame () {
		_animation.Play("start");
		animator.SetBool("StartGame",true);
	}

	void ExitGame () {
		_animation.Play("exit");
		animator.SetBool("StartGame",true);
	}


}
