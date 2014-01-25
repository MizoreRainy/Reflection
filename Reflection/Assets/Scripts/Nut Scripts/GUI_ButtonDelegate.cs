using UnityEngine;
using System.Collections;

public class GUI_ButtonDelegate : MonoBehaviour {

    public delegate void myButtonClickDelegate();
    public myButtonClickDelegate MyButtonClickDelegate;

    public void OnMyButtonClickDeleage()
    {
        if (MyButtonClickDelegate != null)
        {
            MyButtonClickDelegate();
        }
    }

}
