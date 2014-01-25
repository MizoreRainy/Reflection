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
	
	public	LayerMask			playerLayer;
	public	LayerMask			wallLayer;
	
	//------------------------------------------------------------------------
	
	public	CollideSensor		sightSensor;
	public	CollideSensor		interactiveSensor;
	
	//------------------------------------------------------------------------
	
	private	Transform			trans;
	private	Transform			target;

	private	RabbitState			state					=	RabbitState.idle;

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
			Debug.Log( "Player founded!" );
			state	=	RabbitState.chasing;
			target	=	_go.transform;
			IsPlayerOnSight();
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnLeaveSight(GameObject _go)
	{
		if( _go.CompareTag("Player") )
		{
			target	=	null;
		}
	}
	
	//------------------------------------------------------------------------
	
	void OnEnterInteractive(GameObject _go)
	{
		if( _go.CompareTag("Player") )
		{
			state	=	RabbitState.attacking;
			ActivateInteraction();
		}
	}
	
	//------------------------------------------------------------------------

	bool IsPlayerOnSight()
	{
//		RaycastHit H;
//		
//		// goes through wall1 (layer 8,) hits wall2:
//		if(Physics.Raycast(transform.position, transform.forward, out H, 100, ~1<<8))
//			Debug.Log(H.transform.name);
//		
//		// goes through both walls (layer 8 and 9) and hits target:
//		int skip1n2 = ~((1<<8)|(1<<9));
//		if(Physics.Raycast(transform.position, transform.forward, out H, 100, skip1n2))
//			Debug.Log(H.transform.name);
//		
//		// The previous hits ignoreRaycast. This skips 8,9 and ignoreRC(layer 2):
//		int skip = ~((1<<8)|(1<<9)|(1<<2));
//		if(Physics.Raycast(transform.position, transform.forward, out H, 100, skip1n2))
//		{
//		}

		RaycastHit	_hit;
		Vector3 	_rayDirection = target.position - trans.position;

		if( Physics.Raycast( trans.position, _rayDirection.normalized, out _hit, sightRange, 1 << playerLayer ) ) {
			print ("Did Hit");
		} else {
			print ("Did not Hit");
		}
		return false;
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
		else if ( state == RabbitState.chasing )
		{
			ChasingUpdate();

			if(target != null)
				Debug.DrawLine (trans.position, target.transform.position, Color.red);
		}
	}
	
	//------------------------------------------------------------------------

	void IdleUpdate()
	{
		//		Debug.Log( Vector3.Distance( trans.position, agent.destination ) );
		if( Vector3.Distance( trans.position, agent.destination ) < agent.stoppingDistance + nextWaypointOffset )
			MoveToNextWaypoint();
	}
	
	//------------------------------------------------------------------------
	
	void ChasingUpdate()
	{
		if( target != null )
		{
			agent.SetDestination( target.position );
		}
		else
		{
			if( Vector3.Distance( trans.position, agent.destination ) < agent.stoppingDistance + nextWaypointOffset )
			{
				state	=	RabbitState.idle;
				MoveToNextWaypoint();
			}
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
