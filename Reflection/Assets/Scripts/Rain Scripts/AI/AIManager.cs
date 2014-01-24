using UnityEngine;
using System.Collections;

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
	}
	
	//------------------------------------------------------------------------
	
	void SpawnRabbits () 
	{
		
	}
	
	//------------------------------------------------------------------------
}
