using UnityEngine;
using System.Collections;

public class NUT_BulletScript : MonoBehaviour {

    public UISprite aviable_Bullet;
    public UISprite unaviable_Bullet;

    public void SetBulletActive(bool _flag)
    {
        if(_flag)
        {
            aviable_Bullet.enabled = true;
            unaviable_Bullet.enabled = false;
        }
        else
        {
            aviable_Bullet.enabled = false;
            unaviable_Bullet.enabled = true;
        }
    }
}
