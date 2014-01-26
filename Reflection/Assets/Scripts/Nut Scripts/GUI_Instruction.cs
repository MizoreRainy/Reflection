using UnityEngine;
using System.Collections;

public class GUI_Instruction : MonoBehaviour {

    public static GUI_Instruction instance;

    public UISprite page_1;
    public UISprite page_2;

    public UIButton button_Next;
    public UIButton button_Play;

    public void SetDelegateToButton(UIButton _button, GUI_ButtonDelegate.myButtonClickDelegate _delegate)
    {
        (_button.GetComponent("GUI_ButtonDelegate") as GUI_ButtonDelegate).MyButtonClickDelegate = _delegate;
    }


	// Use this for initialization
	void Start () {
        instance = this;
        button_Play.gameObject.SetActive(false);
        page_2.enabled = false;

        SetDelegateToButton(button_Play, delegate()
        {
            button_Play.gameObject.SetActive(false);
            page_2.enabled = false;

            Application.LoadLevel("test");
        });

        SetDelegateToButton(button_Next, delegate()
        {
            Debug.Log("NEXT");

            button_Next.gameObject.SetActive(false);
            page_1.enabled = false;

            button_Play.gameObject.SetActive(true);
            page_2.enabled = true;
        });
	}
}
