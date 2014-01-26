using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RabbitsPool : MonoBehaviour
{
	//------------------------------------------------------------------------
	
	#region Singleton Implementation
	
	public	static 	RabbitsPool		instance;
	
	#endregion
	
	//------------------------------------------------------------------------

	public	int					maxRabbit		=	6;

	private	List<Transform>		rabbitsList		=	new List<Transform>();
	
	//------------------------------------------------------------------------

	void Awake()
	{
		instance	=	this;
	}
	
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

			rabbitsList[i].gameObject.name	=	rabbitsList[i].gameObject.name + i;
			rabbitsList[i].gameObject.SetActive( true );
			_rabbit		=	rabbitsList[i];
			break;
		}
		
		return _rabbit;
	}
	
	//------------------------------------------------------------------------
	
	public void GiveBullet() 
	{
		List<RabbitAI>	_rabbitList		=	new List<RabbitAI>();
		for(int i = 0; i < rabbitsList.Count; i++)
		{
			if( !rabbitsList[i].gameObject.activeInHierarchy || !rabbitsList[i].name.Contains("Good") )
				continue;

			RabbitAI	_rabbitAI	=	rabbitsList[i].gameObject.GetComponent<RabbitAI>();

			if(!_rabbitAI.isHaveBullet)
			{
				_rabbitList.Add(_rabbitAI);
			}
		}

		int	_randomIndex	=	Random.Range(0, _rabbitList.Count);
		_rabbitList[_randomIndex].RecieveBullet();
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
