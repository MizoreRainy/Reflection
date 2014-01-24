using UnityEngine;
using System.Collections;

public class CollideSensor : MonoBehaviour 
{
	//------------------------------------------------------------------------

	public EnterSensorDelegate	enterSensorDelegate;
	public LeaveSensorDelegate	leaveSensorDelegate;

	public delegate	void EnterSensorDelegate(GameObject _go);
	public delegate	void LeaveSensorDelegate(GameObject _go);
	
	//------------------------------------------------------------------------
	
	public void SetSensorRadius (float _radius) 
	{
		SphereCollider	_collider	=	GetComponent<SphereCollider>();
		_collider.radius			=	_radius;
	}

	//------------------------------------------------------------------------
	
	void OnTriggerEnter (Collider _col) 
	{
		if( enterSensorDelegate != null )
			enterSensorDelegate(_col.gameObject);
	}

	//------------------------------------------------------------------------
	
	void OnTriggerExit (Collider _col) 
	{
		if( leaveSensorDelegate != null )
			leaveSensorDelegate(_col.gameObject);
	}
	
	//------------------------------------------------------------------------
}
