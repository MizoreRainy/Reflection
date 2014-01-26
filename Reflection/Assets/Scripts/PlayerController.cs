using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
	
		animator= gameObject.GetComponent<Animator>();
	}

	public Transform rightHandObj;

	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		float f = Input.GetAxis("Vertical");

		float movement = Mathf.Abs(h) + Mathf.Abs(f);


		animator.SetFloat("speed",movement);

	}

	void OnAnimatorIK()
	{
		if(animator) {

				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);
				
				//set the position and the rotation of the right hand where the external object is
				if(rightHandObj != null) {
					animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
				}					

		}
	}	  
}
