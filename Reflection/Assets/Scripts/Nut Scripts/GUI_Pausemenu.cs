using UnityEngine;
using System;
using System.Collections;

public class GUI_Pausemenu : MonoBehaviour {

    public static GUI_Pausemenu instance;

    public Transform panel;

    public Transform pause_Hidelocator;
    public Transform pause_Showlocator;

    public UIButton resume_Button;
    public UIButton restart_Button;

    public delegate void pauseDelegate();
    public pauseDelegate OnPauseDelegate;

    public bool showTest;
    IEnumerator PauseEnumer()
    {
        yield return new WaitForSeconds(1f);
        if (OnPauseDelegate != null)
        {
            OnPauseDelegate();
        }
    }

    public void Show()
    {
        TweenTransform.Begin(panel.gameObject, 1f, pause_Hidelocator, pause_Showlocator, UITweener.Method.BounceIn);
    }

    public void Hide()
    {
        TweenTransform.Begin(panel.gameObject, 1f, pause_Showlocator, pause_Hidelocator, UITweener.Method.BounceIn);
    }

	// Use this for initialization
	void Awake () {
        instance = this;
        Hide();
	}

    void Update()
    {
        if (showTest)
        {
            Show();
            showTest = false;
        }

    }
	
}
