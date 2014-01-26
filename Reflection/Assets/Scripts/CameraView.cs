using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {

	public Camera camer;

	// Use this for initialization
	void Start () {

	}
	


	void SetActi(bool flag){
		camer.gameObject.SetActive(flag);
	}


}
