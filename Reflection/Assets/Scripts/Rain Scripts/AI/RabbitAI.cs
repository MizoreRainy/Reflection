using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RabbitState
{
	idle		=	0,
	roaming		=	1,
	chasing		=	2,
	attacking	=	3
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(RabbitAnimationController))]

public class RabbitAI : MonoBehaviour 
{
	//------------------------------------------------------------------------

	public	bool				isGood					=	true;
	
	//------------------------------------------------------------------------

	public	float				idleDelay				=	2f;
	public	float				sightRange				=	2.5f;
	public	float				interactiveRange		=	1.5f;
	public	float				nextWaypointOffset		=	1.5f;
    public  float               chasing_Speed           =   2.5f;
    public  float               roaming_Speed           =   1f;
	
	//------------------------------------------------------------------------
	
	public	LayerMask			sightMask;
	
	//------------------------------------------------------------------------
	
	public	CollideSensor		sightSensor;
	public	CollideSensor		interactiveSensor;
	
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
		animController.SetGood( isGood );

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

		SetWaypoint( AIManager.instance.GetWaypoint() );
		MoveToNextWaypoint();
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

	#endregion
	
	//------------------------------------------------------------------------

	#region Detection Codes

	void OnEnterSight(GameObject _go)
	{
		if( _go.CompareTag("Player") )
		{
			target	=	_go.transform;

			StartChasing();
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnLeaveSight(GameObject _go)
	{
		if( _go.CompareTag("Player") )
		{
			target	=	null;

			SetRoamingState(false);
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnEnterInteractive(GameObject _go)
	{
		if( _go.CompareTag("Player") )
		{
//			Debug.Log( "Attacking!" );
//			state	=	RabbitState.attacking;
//			ActivateInteraction();
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

				Debug.Log("Found 'ya you little bitch!!");
				return true;
			}
		} 
		
		Debug.Log("Where are you, I'm gonna fucking kill you!");
		return false;
	}

	#endregion
	
	//------------------------------------------------------------------------

	#region Action Codes
	
	void SetIdleState()
	{
		idleDelayCount	=	0f;
		state			=	RabbitState.idle;
		animController.SetAnimationState( RabbitAnimationState.Idle );
	}
	
	void SetRoamingState(bool _isMoveNext = true)
	{
		state	=	RabbitState.roaming;
		animController.SetAnimationState( RabbitAnimationState.Walk );
        agent.speed = roaming_Speed;

		if( _isMoveNext )
			MoveToNextWaypoint();
	}

	//------------------------------------------------------------------------

	void StartChasing()
	{
		if( IsPlayerOnSight() )
		{
			state	=	RabbitState.chasing;
            agent.speed = chasing_Speed;
		}
	}

	#endregion
	
	//------------------------------------------------------------------------

	#region AI Updates

	void Update () 
	{
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
