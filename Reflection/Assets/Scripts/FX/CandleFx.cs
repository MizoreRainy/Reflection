using UnityEngine;
using System.Collections;

public class CandleFx : MonoBehaviour {

	public GameObject prefab;


	void On () {
		prefab.SetActive(true);
	}

	void Off () {
		prefab.SetActive(false);
	}
}
