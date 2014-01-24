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

	public Transform	baseAITransform;
	public Transform[]	spawnNodes;
	
	//------------------------------------------------------------------------
	
	void Awake () 
	{
		instance	=	this;
	}
	
	//------------------------------------------------------------------------
	
	void Start () 
	{
		RabbitsPool.instance.InitRabbits( baseAITransform, transform );

		List<Vector3>	_spawnNode	=	GetRandomizeNode( 4 );

		for(int i = 0; i < 4; i++)
		{
			Transform	_rabit	=	RabbitsPool.instance.SpawnRabbit();
			_rabit.position		=	_spawnNode[i];
		}
	}
	
	//------------------------------------------------------------------------

	List<Vector3> GetRandomizeNode(int _nodeCount)
	{
		if( _nodeCount > spawnNodes.Length )
		{
			Debug.LogError( "Not enough Node" );
			return null;
		}

		List<int>		_indexList	=	new List<int>();
		List<Vector3>	_nodeList	=	new List<Vector3>();

		for(int i = 0; i < _nodeCount; i++)
		{
			int	_nodeIndex	=	Random.Range(0, spawnNodes.Length);

			while( _indexList.IndexOf( _nodeIndex ) != -1 )
				_nodeIndex	=	Random.Range(0, spawnNodes.Length);

			_nodeList.Add( spawnNodes[_nodeIndex].position );
		}

		return	_nodeList;
	}
	
	//------------------------------------------------------------------------
	
	void SpawnRabbits () 
	{
		
	}
	
	//------------------------------------------------------------------------
}
