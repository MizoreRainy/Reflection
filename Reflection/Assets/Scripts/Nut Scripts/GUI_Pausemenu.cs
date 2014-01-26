using UnityEngine;
using System;
using System.Collections;

public class GUI_Pausemenu : MonoBehaviour {

    public static GUI_Pausemenu instance;

    public Transform panel;
    public Transform pausemenu_Container;

    public Transform pause_Hidelocator;
    public Transform pause_Showlocator;

    public UIButton resume_Button;
    public UIButton restart_Button;

    public delegate void pauseDelegate();
    public pauseDelegate OnPauseDelegate;

    private bool isShowing = false;

    public bool showTest;
    IEnumerator PauseEnumer()
    {
        yield return new WaitForSeconds(1f);
        if (OnPauseDelegate != null)
        {
            OnPauseDelegate();
        }
    }

    public void SetDelegateToButton(UIButton _button, GUI_ButtonDelegate.myButtonClickDelegate _delegate)
    {
        (_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = _delegate;
    }

    public void Show()
    {
        if (isShowing)
        { return; }
        else
        { isShowing = true; }
        panel.gameObject.SetActive(true);
        TweenTransform.Begin(pausemenu_Container.gameObject, 1f, pause_Hidelocator, pause_Showlocator, UITweener.Method.BounceIn);
        Init();
    }

    public void Hide()
    {
        TweenTransform.Begin(pausemenu_Container.gameObject, 1f, pause_Showlocator, pause_Hidelocator, UITweener.Method.EaseOut);
        StartCoroutine(PauseEnumer());
    }

    void Init()
    {
        SetDelegateToButton(resume_Button, delegate()
        {
            Debug.Log("RESUME");
            Hide();
            OnPauseDelegate = delegate()
            {
                isShowing = false;
                panel.gameObject.SetActive(false);
                OnPauseDelegate = null;
                GUI_HUDIngame.instance.OnActiveAgain();
            };
        });
    }
	// Use this for initialization
	void Awake () {
        instance = this;
        panel.gameObject.SetActive(false);

        Init();
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
