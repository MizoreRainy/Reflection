using UnityEngine;
using System.Collections;

public class PlayerDie : MonoBehaviour {

	public MeshRenderer rend;

	public bool isDie = false;
	private float shaderValue = 1;


	void Awake(){

		rend.material.SetFloat("_Amount",1);
	}

	// Use this for initialization
	void LateUpdate () {
	
		return;
		if(isDie){




			rend.material.SetFloat("_Amount",Mathf.Lerp(1,0,Time.time/13));
			if(rend.material.GetFloat("_Amount") < 0.3f){

				GUI_Resultmenu.instance.Show(2);
				Destroy(this);

			}
		}

	}


	IEnumerator Waitt(){

		yield return new WaitForSeconds(2);
		GUI_Resultmenu.instance.Show(2);
		Destroy(this);


	}

	void Die(){
		
	//	return;

		Debug.Log("DIE");
		if(isDie == false){


			StartCoroutine("Waitt");
		shaderValue = 1;
		isDie = true;

		}
	}
}
