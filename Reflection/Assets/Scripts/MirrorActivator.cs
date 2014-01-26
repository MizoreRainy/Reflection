using UnityEngine;
using System.Collections;

public class MirrorActivator : MonoBehaviour {


	public GameObject[] mirrors;

	void OnTriggerEnter(Collider obj){
		if(obj.collider.gameObject.tag == "Player"){
			foreach( GameObject element in mirrors){
				element.SendMessage("SetActi",true);
			}
		}
	}



	void OnTriggerExit(Collider obj){
		if(obj.collider.gameObject.tag == "Player"){
			foreach( GameObject element in mirrors){
				element.SendMessage("SetActi",false);
			}
		}
	}


}
