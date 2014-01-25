using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RabbitsPool 
{
	//------------------------------------------------------------------------
	
	#region Singleton Implementation
	
	public	static 	RabbitsPool		instance
	{
		get
		{
			if(_instance == null)
				_instance = new RabbitsPool();

			return _instance;
		}
	}

	private static 	RabbitsPool		_instance;
	private RabbitsPool()			
	{
		rabbitsList		=	new List<Transform>();
	}
	
	#endregion
	
	//------------------------------------------------------------------------

	public	int					maxRabbit		=	6;

	private	List<Transform>		rabbitsList;
	
	//------------------------------------------------------------------------
	
	public void InitRabbits (Transform _rabbitTransform, Transform _parent) 
	{
		if( _rabbitTransform == null)
		{
			Debug.LogError( "Rabbit transform was null" );
			return;
		}
		
		for(int i = 0; i < maxRabbit; i++)
		{
			Transform	_rabbit		=	UnityEditor.Editor.Instantiate( _rabbitTransform )  as Transform;
			rabbitsList.Add( _rabbit );

			_rabbit.parent	=	_parent;
			_rabbit.gameObject.SetActive( false );
		}
	}
	
	//------------------------------------------------------------------------
	
	public Transform SpawnGoodRabbit() 
	{
		Transform	_rabbit		=	null;
		
		for(int i = 0; i < rabbitsList.Count; i++)
		{
			if( rabbitsList[i].gameObject.activeInHierarchy || !rabbitsList[i].name.Contains("Good")  )
				continue;
			
			rabbitsList[i].gameObject.SetActive( true );
			_rabbit		=	rabbitsList[i];
			break;
		}
		
		return _rabbit;
	}
	
	//------------------------------------------------------------------------
	
	public Transform SpawnBadRabbit() 
	{
		Transform	_rabbit		=	null;

		for(int i = 0; i < rabbitsList.Count; i++)
		{
			if( rabbitsList[i].gameObject.activeInHierarchy || !rabbitsList[i].name.Contains("Bad") )
				continue;
			
			rabbitsList[i].gameObject.SetActive( true );
			_rabbit		=	rabbitsList[i];
			break;
		}
		
		return _rabbit;
	}
	
	//------------------------------------------------------------------------
	
	public void DespawnRabbit(Transform _rabit) 
	{
		Transform	_despawn	=	rabbitsList[ rabbitsList.IndexOf( _rabit ) ];
		_despawn.gameObject.SetActive( false );
	}
	
	//------------------------------------------------------------------------
}
