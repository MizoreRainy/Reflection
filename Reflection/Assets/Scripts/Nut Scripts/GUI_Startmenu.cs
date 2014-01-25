using UnityEngine;
using System.Collections;

public class GUI_Startmenu : MonoBehaviour {

    public static GUI_Startmenu instance;

    public UIButton start_button;
    public Transform start_Hidelocator;
    public Transform start_Showlocator;

    public UIButton exit_button;
    public Transform exit_Hidelocator;
    public Transform exit_Showlocator;


    public void Show()
    {
        TweenTransform.Begin(start_button.transform.gameObject, 1f, start_Hidelocator, start_Showlocator, UITweener.Method.EaseOut);
        TweenTransform.Begin(exit_button.transform.gameObject, .75f, exit_Hidelocator, exit_Showlocator, UITweener.Method.EaseOut);
    }
	// Use this for initialization
	void Awake () {
        instance = this;

        (start_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = delegate()
        {
            Debug.Log("START!");
        };


        (exit_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = delegate()
        {
            Debug.Log("EXIT!");
        };

        Show();

    }
}
