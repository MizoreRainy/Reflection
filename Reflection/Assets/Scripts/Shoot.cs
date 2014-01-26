using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public ParticleEmitter _1;
	public ParticleEmitter _2;
	public Camera cam;

	public Transform spawner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			MakeBullet();
		}
	}


	public LayerMask mask;

	public int badCount =0;

	public void MakeBullet(){

		if( Player.bulletCount <= 0 ) return;


		transform.root.gameObject.SendMessage("DecreaseBullet");
		_1.Emit();
		_2.Emit();
		audio.Play();

		Ray ray = cam.ViewportPointToRay (new Vector3(0.5f,0.5f,0));

		RaycastHit hit;


		if(Physics.Raycast(ray,out hit,100000)) {
			Debug.Log(hit.collider.name);

			if(hit.collider.tag == "bad_bunny" || hit.collider.tag == "good_bunny")
			{
				hit.collider.gameObject.SendMessage("GetHit");

				if(hit.collider.tag == "bad_bunny"){
					badCount ++ ;
				}

				if(badCount>2){
					Debug.Log("win");
					GUI_Resultmenu.instance.Show(1);
				}

			}
			else
			{
				AIManager.instance.MissBullet();
			}


		}



	}

	public void ResetAnimation(){
		animation.CrossFade("revilverIdle");
		Debug.Log("reload");
	}

}
