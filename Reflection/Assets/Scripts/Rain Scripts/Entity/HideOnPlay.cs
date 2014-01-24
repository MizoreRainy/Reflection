using UnityEngine;
using System.Collections;

public class HideOnPlay : MonoBehaviour 
{
	//------------------------------------------------------------------------

	void Start () 
	{
		if( renderer )
			renderer.enabled	=	false;
	}
	
	//------------------------------------------------------------------------
}
