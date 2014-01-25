using UnityEngine;
using System.Collections;

public enum RabbitAnimationState
{
	Idle,
	Walk,
	Run,
	Dead,
	Action
}

public class RabbitAnimationController : MonoBehaviour 
{
	
	//------------------------------------------------------------------------
	
	public RabbitAI	rabbitAI;
	public Animator	rabbitAnimator;
	
	//------------------------------------------------------------------------

	public void SetAnimationState( RabbitAnimationState	_state )
	{
		if( _state == RabbitAnimationState.Idle )
		{
			SetSpeed(0f);
		}
		else if( _state == RabbitAnimationState.Walk )
		{
			SetSpeed(1f);
		}
		else if( _state == RabbitAnimationState.Run )
		{
			SetSpeed(2f);
		}
		else if( _state == RabbitAnimationState.Dead )
		{
			PlayDeath();
		}
		else if( _state == RabbitAnimationState.Action )
		{
			ActivateAction();
		}
	}

	//------------------------------------------------------------------------

	void SetSpeed (float _speed) 
	{
		rabbitAnimator.SetFloat("Speed", _speed);
	}
	
	//------------------------------------------------------------------------
	
	void PlayDeath () 
	{
		rabbitAnimator.SetTrigger("Dead");
	}
	
	//------------------------------------------------------------------------
	
	void ActivateAction () 
	{
		rabbitAnimator.SetBool( "isHaveBullet", rabbitAI.isHaveBullet );
		rabbitAnimator.SetTrigger("Action");
	}
	
	//------------------------------------------------------------------------
	
	public void SetGood (bool _isGood) 
	{
		rabbitAnimator.SetBool("isGood", _isGood);
	}
	
	//------------------------------------------------------------------------
}
