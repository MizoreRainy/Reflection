using UnityEngine;
using System;
using System.Collections;

public class GUI_Startmenu : MonoBehaviour {

    public static GUI_Startmenu instance;

    public Transform panel;

    public UIButton start_button;
    public Transform start_Hidelocator;
    public Transform start_Showlocator;

    public UIButton exit_button;
    public Transform exit_Hidelocator;
    public Transform exit_Showlocator;

    private delegate void myDelegate();
    private myDelegate UseMyDelegate;

    IEnumerator HideEnumer()
    {
        yield return new WaitForSeconds(1f);
        panel.gameObject.SetActive(false);
        if (UseMyDelegate != null)
        {
            UseMyDelegate();
        }
    }

    public void Show()
    {
        panel.gameObject.SetActive(true);
        TweenTransform.Begin(start_button.transform.gameObject, 1f, start_Hidelocator, start_Showlocator, UITweener.Method.EaseOut);
        TweenTransform.Begin(exit_button.transform.gameObject, .75f, exit_Hidelocator, exit_Showlocator, UITweener.Method.EaseOut);
    }

    public void Hide()
    {
        TweenTransform.Begin(start_button.transform.gameObject, 1f, start_Showlocator, start_Hidelocator, UITweener.Method.EaseOut);
        TweenTransform.Begin(exit_button.transform.gameObject, .75f, exit_Showlocator, exit_Hidelocator, UITweener.Method.EaseOut);
        StartCoroutine(HideEnumer());
    }

	// Use this for initialization
	void Awake () {
        instance = this;
        
        (start_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = delegate()
        {
            Debug.Log("START!");
            UseMyDelegate = delegate()
            {
                Gui_Loadingscreen.instance.Show();
            };
            Hide();
        };

        (exit_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = delegate()
        {
            Debug.Log("EXIT!");
            UseMyDelegate = delegate()
            {
                Application.Quit();
            };
            Hide();
        };

        Show();

    }
}
