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

	public int			badRabbitCount	=	3;
	public int			goodRabbitCount	=	3;

	public Transform	spawnNodes;
	public Transform	waypointNodes;
	public Transform	goodRabbitTransform;
	public Transform	badRabbitTransform;
	
	//------------------------------------------------------------------------

	public	Player		player;
	
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
		
		List<Vector3>	_spawnNode	=	GetRandomizeNode( badRabbitCount + goodRabbitCount );
		
		int	_index	=	0;

		RabbitsPool.instance.InitRabbits( badRabbitTransform, transform );

		for(_index = 0; _index < badRabbitCount; _index++)
		{
			Transform	_rabit	=	RabbitsPool.instance.SpawnBadRabbit();
			_rabit.position		=	_spawnNode[_index];
		}
		
		RabbitsPool.instance.InitRabbits( goodRabbitTransform, transform );
		
		for(_index = badRabbitCount ; _index < (badRabbitCount + goodRabbitCount); _index++)
		{
			Transform	_rabit	=	RabbitsPool.instance.SpawnGoodRabbit();
			_rabit.position		=	_spawnNode[_index];
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
	
	public void MissBullet () 
	{
		RabbitsPool.instance.GiveBullet();
	}
	
	//------------------------------------------------------------------------
}
