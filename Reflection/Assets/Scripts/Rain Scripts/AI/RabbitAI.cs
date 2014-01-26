using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RabbitState
{
	idle		=	0,
	roaming		=	1,
	chasing		=	2,
	attacking	=	3,
	charge		=	4,
	runaway		=	5,
	dead		=	6
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(RabbitAnimationController))]

public class RabbitAI : MonoBehaviour 
{
	//------------------------------------------------------------------------

	public	bool				isGood					=	true;
	public	bool				isHaveBullet			=	true;
	
	//------------------------------------------------------------------------
	
	public	float				chargeDelay				=	2f;
	public	float				idleDelay				=	2f;
	public	float				sightRange				=	2.5f;
	public	float				interactiveRange		=	1.5f;
	public	float				nextWaypointOffset		=	1.5f;
    public  float               chasingSpeed           	=   .85f;
	public  float               roamingSpeed			=   2.5f;
	public  float               chargingSpeed			=   3f;
	
	//------------------------------------------------------------------------

	public  Collider              hitSensor;
	
	//------------------------------------------------------------------------
	
	public	LayerMask			sightMask;
	
	//------------------------------------------------------------------------
	
	public	CollideSensor		sightSensor;
	public	CollideSensor		interactiveSensor;
	
	//------------------------------------------------------------------------
	
	public	RabbitSoundController			soundController;
	
	//------------------------------------------------------------------------
	
	private	float				idleDelayCount			=	0f;

	private	Transform			trans;
	private	Transform			target;

	private	RabbitState			state					=	RabbitState.idle;
	
	private	NavMeshAgent					agent;
	private	RabbitAnimationController		animController;
	private	List<Vector3>					waypointList;

	//------------------------------------------------------------------------

	public	OnPlayerEnterDangerZone		onPlayerEnterDangerZone;
	public	delegate void				OnPlayerEnterDangerZone( Transform _trans);
	
	//------------------------------------------------------------------------

	#region Init Data

	void Awake () 
	{
		trans			=	transform;
		agent			=	GetComponent<NavMeshAgent>();
		animController	=	GetComponent<RabbitAnimationController>();

		target			=	null;

	}
	
	//------------------------------------------------------------------------
	
	void Start () 
	{
		
		if(sightSensor == null || interactiveSensor == null)
		{
			Debug.LogError( "Can't find sensor for the RabbitAI: " + this);
			return;
		}
		
		sightSensor.SetSensorRadius( sightRange );
		interactiveSensor.SetSensorRadius( interactiveRange );
		
		sightSensor.enterSensorDelegate		=	OnEnterSight;
		sightSensor.leaveSensorDelegate		=	OnLeaveSight;
		
		interactiveSensor.enterSensorDelegate		=	OnEnterInteractive;

		agent.enabled	=	true;
		SetWaypoint( AIManager.instance.GetWaypoint() );
//		MoveToNextWaypoint();
	}

	#endregion
	
	//------------------------------------------------------------------------

	#region Waypoint Setting
	
	public void SetWaypoint(List<Vector3> _waypointList)
	{
		waypointList	=	_waypointList;
	}
	
	//------------------------------------------------------------------------
	
	public void MoveToNextWaypoint()
	{
		if(state != RabbitState.dead)
		{
			if( waypointList.Count <= 1 )
			{
				Debug.LogError( "Can't find next waypoint" );
				return;
			}
			
			Vector3	_nextWaypoint	=	waypointList[ Random.Range(0, waypointList.Count)];
			
			while( _nextWaypoint == agent.destination )
				_nextWaypoint	=	waypointList[ Random.Range(0, waypointList.Count)];
			
			agent.SetDestination( _nextWaypoint );
		}

	}

	#endregion
	
	//------------------------------------------------------------------------

	#region Detection Codes

	void OnEnterSight(GameObject _go)
	{
		if(state != RabbitState.dead)
		{
			if( _go.CompareTag("Player") )
			{
				target	=	_go.transform;

				StartChasing();
			}
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnLeaveSight(GameObject _go)
	{
		if(state != RabbitState.dead)
		{
			if( _go.CompareTag("Player") )
			{
				target	=	null;

				SetRoamingState(false);
			}
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnEnterInteractive(GameObject _go)
	{
		if(state != RabbitState.dead)
		{
			if( _go.CompareTag("Player") )
			{
				InteractWithPlayer(_go);
			}
		}
	}
	
	//------------------------------------------------------------------------

	bool IsPlayerOnSight(bool _isDrawline = false)
	{
		if( target == null )
			return false;

		RaycastHit	_hit;
		Vector3 	_rayDirection = target.position - trans.position;

		if( Physics.Raycast( trans.position, _rayDirection.normalized, out _hit, sightRange, sightMask ) ) 
		{
			if( _hit.collider.tag.Equals( "Player" ) )
			{
				if( _isDrawline )
					Debug.DrawLine (trans.position, _hit.point, Color.red);

//				Debug.Log("Found 'ya you little bitch!!");
				return true;
			}
		} 
		
//		Debug.Log("Where are you, I'm gonna fucking kill you!");
		return false;
	}

	#endregion
	
	//------------------------------------------------------------------------

	#region Action Codes
	
	void SetIdleState()
	{
		idleDelayCount	=	0f;
		state			=	RabbitState.idle;

		agent.SetDestination(trans.position);
		animController.SetAnimationState( RabbitAnimationState.Idle );
		soundController.PlaySound( SoundType.idle );
	}
	
	//------------------------------------------------------------------------
	
	void SetRoamingState(bool _isMoveNext = true)
	{
		state	=	RabbitState.roaming;
		animController.SetAnimationState( RabbitAnimationState.Walk );
        agent.speed = roamingSpeed;

		if( _isMoveNext )
			MoveToNextWaypoint();
	}
	
	//------------------------------------------------------------------------
	
	void StartChasing()
	{
		if( IsPlayerOnSight() )
		{
			state	=	RabbitState.chasing;
			agent.speed = chasingSpeed;
			soundController.PlaySound( SoundType.idle );
		}
	}
	
	//------------------------------------------------------------------------

	void StartCharging(GameObject _target)
	{
		agent.speed		=	chargingSpeed;
		agent.SetDestination( _target.transform.position );
		animController.SetAnimationState( RabbitAnimationState.Action );
	}
	
	//------------------------------------------------------------------------
	
	IEnumerator WaitThenDo(float _delay, System.Action _callback, bool _isIgnoreDead = false)
	{
		float	_startTime	=	Time.time;
		float	_endTime	=	_startTime + _delay;
		
//		Debug.Log("WAIT");
		while( Time.time < _endTime )
		{
			yield return new WaitForSeconds (.5f);

//			Debug.Log( "_startTime: " + _startTime + "\n" + "_endTime: " + _endTime );
		}
		
		if(state != RabbitState.dead)
			_callback();
		else if (_isIgnoreDead)
			_callback();

	}
	
	//------------------------------------------------------------------------
	
	void StartRunaway()
	{
		target	=	null;
		MoveToNextWaypoint();
		StartCoroutine( WaitThenDo( chargeDelay, SetRoamingState ) );
	}
	
	//------------------------------------------------------------------------
	
	public void RecieveBullet()
	{
		Debug.Log("GETGETGET");
		isHaveBullet	=	true;
	}
	
	//------------------------------------------------------------------------
	
	void Falldown()
	{
		hitSensor.enabled	=	false;
	}
	
	//------------------------------------------------------------------------
	
	void InteractWithPlayer(GameObject _player)
	{
		if( !isGood )
		{
			StartCharging(_player);
			_player.SendMessage("GetHit");
			soundController.PlaySound( SoundType.attack );
		}
		else
		{

			StartCharging(_player);
			StartCoroutine( WaitThenDo( chargeDelay, StartRunaway ) );

			if( isHaveBullet )
			{
				isHaveBullet	=	false;
				_player.SendMessage("GiveBullet");
				soundController.PlaySound( SoundType.givebullet );
			}
		}
	}
	
	//------------------------------------------------------------------------

	public GameObject fx_dead;

	void GetHit()
	{
		if(state != RabbitState.dead)
		{
			state	=	RabbitState.dead;
			agent.SetDestination( trans.position );
			agent.Stop();
			agent.enabled		=	false;
			if(hitSensor)
			{
				Debug.Log("TEST");
				rigidbody.AddTorque( new Vector3(.5f, 15f, .5f) );
				rigidbody.AddForce( new Vector3(1.5f, 2f, 1.5f) );
				Destroy(Instantiate(fx_dead,transform.position+Vector3.up,transform.rotation),4);
				soundController.PlaySound( SoundType.dead );
				StartCoroutine( WaitThenDo( 4f, Falldown, true ) );
			}
			
			animController.Revealed();
			animController.SetAnimationState( RabbitAnimationState.Dead );
		}
	}

	#endregion
	
	//------------------------------------------------------------------------

	#region AI Updates

	void Update () 
	{
		if(state ==  RabbitState.dead)
			return;

		if( state == RabbitState.idle )
		{
			IdleUpdate();
		}
		if( state == RabbitState.roaming )
		{
			RoamingUpdate();
		}
		else if ( state == RabbitState.chasing )
		{
			ChasingUpdate();
		}
	}
	
	//------------------------------------------------------------------------
	
	void IdleUpdate()
	{
		idleDelayCount	+=	Time.deltaTime;
		if( idleDelayCount >= idleDelay )
		{
			SetRoamingState(true);
		}
	}
	
	//------------------------------------------------------------------------

	void RoamingUpdate()
	{
		if( IsPlayerOnSight( true ) )
		{
			StartChasing();
		}
		else
		{
			if( Vector3.Distance( trans.position, agent.destination ) < agent.stoppingDistance + nextWaypointOffset )
				SetIdleState();
		}
	}
	
	//------------------------------------------------------------------------
	
	void ChasingUpdate()
	{
		if( target != null )
		{

			if( IsPlayerOnSight( true ) )
			{
				agent.SetDestination( target.position );
			}
			else
				SetRoamingState();
		}
		else
		{
			SetRoamingState();
		}
	}
	
	//------------------------------------------------------------------------
	
	void ActivateInteraction()
	{
		if( onPlayerEnterDangerZone != null )
			onPlayerEnterDangerZone( target );
	}

	#endregion
	
	//------------------------------------------------------------------------

}
