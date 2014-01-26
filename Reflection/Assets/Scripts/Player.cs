using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public static int bulletCount = 0;
	public static int missCount = 0;

	public static bool isInGame = false;




	// Use this for initialization
	void Start () {
		isInGame = true;
		GUI_HUDIngame.instance.SetBulletAmount(0);
	}

	public void GetHit(){
		isInGame = false;
		Debug.Log("getHit");
		Camera.main.gameObject.SendMessage("Die");
		//todo finish game
	}

	public void GiveBullet(){

		if (bulletCount<3){
			bulletCount++;
			GUI_HUDIngame.instance.SetBulletAmount(bulletCount);
			//call gui 
		}
	}

	public void DecreaseBullet(){
		if(bulletCount>0){
			bulletCount--;
			GUI_HUDIngame.instance.SetBulletAmount(bulletCount);
		}
	}




	// Update is called once per frame
	void Update () {
	
	}
}
