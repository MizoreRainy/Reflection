using UnityEngine;
using System.Collections;

public class Intro_Key : MonoBehaviour {
	private Animator aniamtor;
	// Use this for initialization
	void Start () {
		aniamtor = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray,out hit,100)){
		//	Debug.Log(hit.collider.name);
			aniamtor.SetLookAtWeight(1);
			aniamtor.SetLookAtPosition( hit.point);
		}

	}
}
