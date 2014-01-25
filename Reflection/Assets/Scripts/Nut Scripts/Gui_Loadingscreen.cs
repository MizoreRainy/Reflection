using UnityEngine;
using System.Collections;

public class Gui_Loadingscreen : MonoBehaviour {

    public static Gui_Loadingscreen instance;

    public Transform panel;
    public UILabel loading_Text;
    public bool stop_test;
    public float my_intensity;
    void ShakingText()
    {
        (loading_Text.GetComponent("GUI_ShakingText") as GUI_ShakingText).Shake(my_intensity, .75f, true);
    }

    void StopShakingText()
    {
        (loading_Text.GetComponent("GUI_ShakingText") as GUI_ShakingText).StopShake();
    }

    public void Show()
    {
        panel.gameObject.SetActive(true);
        ShakingText();
    }

    public void Hide()
    {
        StopShakingText();
        panel.gameObject.SetActive(false);
    }

    void Start()
    {
        instance = this;
        panel.gameObject.SetActive(false);
    }

}
