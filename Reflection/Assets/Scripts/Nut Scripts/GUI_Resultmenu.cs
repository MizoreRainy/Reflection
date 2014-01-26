using UnityEngine;
using System;
using System.Collections;

public class GUI_Resultmenu : MonoBehaviour {

    public static GUI_Resultmenu instance;

    public Transform panel;

    public UIButton retry_button;
    public UIButton backmenu_button;

    public UISprite winFace;
    public UISprite loseFace;

    public delegate void CallBack();

    public void SetDelegateToButton(UIButton _button , GUI_ButtonDelegate.myButtonClickDelegate _delegate)
    {
        (_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = _delegate;
    }
    
    public void Show()
    {
        panel.gameObject.SetActive(true);
    }

    public void Show(int condition)
    {
        if (condition == 1)
        {
            winFace.enabled = true;
            loseFace.enabled = false;
        }
        else if(condition == 2)
        {
            winFace.enabled = false;
            loseFace.enabled = true;
        }
        else
        {
            winFace.enabled = false;
            loseFace.enabled = false;
        }

        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Awake () {
        instance = this;
        winFace.enabled = false;
        loseFace.enabled = false;
        panel.gameObject.SetActive(false);
        SetDelegateToButton(retry_button ,delegate() 
        {
            Debug.Log("RETRY");
        });

        SetDelegateToButton(backmenu_button, delegate()
        {
            Debug.Log("BACK");
            Hide();
            //GUI_Startmenu.instance.Show();
			Application.LoadLevel("preload");
        });

	}
}
