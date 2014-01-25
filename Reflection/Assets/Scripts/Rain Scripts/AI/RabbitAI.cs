using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RabbitState
{
	idle		=	0,
	chasing		=	1,
	attacking	=	2
}

[RequireComponent(typeof(NavMeshAgent))]

public class RabbitAI : MonoBehaviour 
{
	//------------------------------------------------------------------------

	public	float				sightRange				=	2.5f;
	public	float				interactiveRange		=	1.5f;
	public	float				nextWaypointOffset		=	1.5f;
	
	//------------------------------------------------------------------------
	
	public	LayerMask			sightMask;
	
	//------------------------------------------------------------------------
	
	public	CollideSensor		sightSensor;
	public	CollideSensor		interactiveSensor;
	
	//------------------------------------------------------------------------
	
	private	Transform			trans;
	private	Transform			target;

	public	RabbitState			state					=	RabbitState.idle;

	private	NavMeshAgent		agent;
	private	List<Vector3>		waypointList;

	//------------------------------------------------------------------------

	public	OnPlayerEnterDangerZone		onPlayerEnterDangerZone;
	public	delegate void				OnPlayerEnterDangerZone( Transform _trans);
	
	//------------------------------------------------------------------------

	#region Init Data

	void Awake () 
	{
		trans	=	transform;
		agent	=	GetComponent<NavMeshAgent>();
		
		target	=	null;
		
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
	}
	
	//------------------------------------------------------------------------
	
	void Start () 
	{
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

			SetIdleState(false);
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
	
	void SetIdleState(bool _isMoveNext = true)
	{
		state	=	RabbitState.idle;

		if( _isMoveNext )
			MoveToNextWaypoint();
	}

	//------------------------------------------------------------------------

	void StartChasing()
	{
		if( IsPlayerOnSight() )
		{
			state	=	RabbitState.chasing;
		}
	}

	#endregion
	#region AI Updates

	void Update () 
	{
		if( state == RabbitState.idle )
		{
			IdleUpdate();
		}
		else if ( state == RabbitState.chasing )
		{
			ChasingUpdate();
		}
	}
	
	//------------------------------------------------------------------------

	void IdleUpdate()
	{
		if( IsPlayerOnSight( true ) )
		{
			StartChasing();
		}
		else
		{
			if( Vector3.Distance( trans.position, agent.destination ) < agent.stoppingDistance + nextWaypointOffset )
				MoveToNextWaypoint();
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
				SetIdleState();
		}
		else
		{
			SetIdleState();
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
