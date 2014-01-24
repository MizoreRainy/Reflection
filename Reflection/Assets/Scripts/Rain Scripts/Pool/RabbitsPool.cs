using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RabbitsPool : MonoBehaviour 
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
			Transform	_rabbit		=	Instantiate( _rabbitTransform )  as Transform;
			rabbitsList.Add( _rabbit );

			_rabbit.parent	=	_parent;
		}
	}
	
	//------------------------------------------------------------------------
	
	public void DespawnRabbit(Transform _rabit) 
	{
		Transform	_despawn	=	rabbitsList[ rabbitsList.IndexOf( _rabit ) ];
		_despawn.gameObject.SetActive( false );
	}
	
	//------------------------------------------------------------------------
}
