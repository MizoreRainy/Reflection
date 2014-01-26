using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Awake () {
		if(tag == "fader")
		{
			Debug.Log("3333");
			GameObject[]	_find	=	GameObject.FindGameObjectsWithTag("fader");

			if( _find.Length > 1 )
			{
				Debug.Log("111111");
				Destroy(this);
			}
			else
			{
				Debug.Log("22222");
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}
