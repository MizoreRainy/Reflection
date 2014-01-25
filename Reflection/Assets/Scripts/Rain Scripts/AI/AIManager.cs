using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour 
{
	//------------------------------------------------------------------------

	#region Singleton Implementation

	public static AIManager	instance;

	#endregion
	
	//------------------------------------------------------------------------
	
	public Transform	spawnNodes;
	public Transform	waypointNodes;
	public Transform	baseAITransform;
	
	//------------------------------------------------------------------------
	
	void Awake () 
	{
		instance	=	this;
	}
	
	//------------------------------------------------------------------------

	public List<Vector3> GetWaypoint()
	{
		List<Vector3>	_waypointList	=	new List<Vector3>();

		for(int i = 0; i < waypointNodes.childCount; i++)
			_waypointList.Add(waypointNodes.GetChild(i).position);

		return _waypointList;
	}
	
	//------------------------------------------------------------------------
	
	void Start () 
	{
		RabbitsPool.instance.InitRabbits( baseAITransform, transform );

		List<Vector3>	_spawnNode	=	GetRandomizeNode( 1 );

		for(int i = 0; i < 1; i++)
		{
			Transform	_rabit	=	RabbitsPool.instance.SpawnRabbit();
			_rabit.position		=	_spawnNode[i];
		}
	}
	
	//------------------------------------------------------------------------

	List<Vector3> GetRandomizeNode(int _nodeCount)
	{
		if( _nodeCount > spawnNodes.childCount )
		{
			Debug.LogError( "Not enough Node" );
			return null;
		}

		List<int>		_indexList	=	new List<int>();
		List<Vector3>	_nodeList	=	new List<Vector3>();

		for(int i = 0; i < _nodeCount; i++)
		{
			int	_nodeIndex	=	Random.Range(0, spawnNodes.childCount);

			while( _indexList.IndexOf( _nodeIndex ) != -1 )
				_nodeIndex	=	Random.Range(0, spawnNodes.childCount);

			_indexList.Add( _nodeIndex );
			_nodeList.Add( spawnNodes.GetChild( _nodeIndex ).position );
		}

		return	_nodeList;
	}
	
	//------------------------------------------------------------------------
	
	void SpawnRabbits () 
	{
		
	}
	
	//------------------------------------------------------------------------
}
