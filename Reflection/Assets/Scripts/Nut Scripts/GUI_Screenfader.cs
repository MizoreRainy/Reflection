using UnityEngine;
using System;
using System.Collections;

public class GUI_Screenfader : MonoBehaviour
{
    public static GUI_Screenfader instance;

    public float delay;
	public Transform panel;
    public UISprite vignette;
	public Camera	fader_Camera;
    public delegate void faderDelegate();
	public faderDelegate FaderDelegate;

    IEnumerator ForDelay()
    {
        yield return new WaitForSeconds(delay);
		if(FaderDelegate != null){FaderDelegate();}
    }

    public void Show()
    {
		panel.gameObject.SetActive(true);
        TweenAlpha.Begin(vignette.gameObject, delay, 1f);
		StartCoroutine(ForDelay());
    }

    public void Hide()
    {
		FaderDelegate	=	delegate() {
			panel.gameObject.SetActive(false);
		};
        TweenAlpha.Begin(vignette.gameObject, delay, 0f);
    }

    void Start() {
        instance = this;
		panel.gameObject.SetActive(false);
	}

	void OnLevelWasLoaded(int _num)
	{
		if(Application.loadedLevelName == "test")
		  {
			Hide();
			//panel.gameObject.SetActive(false);
		}
	}

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.W))
    //    {
    //        if (!isShow)
    //        {
    //            return;
    //        }

    //        isShow = false;
    //        Hide();
    //    }
    //}
}
