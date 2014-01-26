using UnityEngine;
using System.Collections;

public class GUI_HUDIngame : MonoBehaviour {

    public static GUI_HUDIngame instance;

    public Transform panel;

    public NUT_BulletScript[] _bulletslot; 

    public UIProgressBar timebar_Right;
    public UIProgressBar timebar_Left;
    [HideInInspector]public float _time    =   0;

    #region TimeBar
    [HideInInspector]public float _startFLOAT_time;
    private float _permanant_Startime;
    [HideInInspector]public float _maxFLOAT_time;
    private float _permanant_Maxtime;
    [HideInInspector]public bool isTimeActive;

    public void SetBulletAmount(int _amount)
    {
        for (int i = 0; i < 3; ++i )
        {
            _bulletslot[i].SetBulletActive(false);
        }

        if (_amount == 1)
        {
            _bulletslot[0].SetBulletActive(true);
        }
        else if (_amount == 2)
        {
            _bulletslot[0].SetBulletActive(true);
            _bulletslot[1].SetBulletActive(true);
        }
        else if (_amount == 3)
        {
            _bulletslot[0].SetBulletActive(true);
            _bulletslot[1].SetBulletActive(true);
            _bulletslot[2].SetBulletActive(true);
        }
        else
            Debug.Log("BULET AMOUNT MORE THAN 3!");
    }

    public void OnAnimateTimebar(float _time)
    {
        _startFLOAT_time = _time;
        _maxFLOAT_time = _time;

        _permanant_Startime = _startFLOAT_time;
        _permanant_Maxtime = _maxFLOAT_time;

        isTimeActive = true;
        StartCoroutine(TestTimeBar());
    }

    public void Show()
    {
        panel.gameObject.SetActive(true);
        isTimeActive = true;
        StartCoroutine(TestTimeBar());
        //StartCoroutine(TestCount());
    }

    public void Hide()
    {
        _startFLOAT_time = _permanant_Startime;
        _maxFLOAT_time = _permanant_Maxtime;
        panel.gameObject.SetActive(false);
    }

    //IEnumerator TestCount()
    //{
    //    if (!isTimeActive)
    //        yield return null;

    //    while (isTimeActive)
    //    {

    //        _time++;
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    public void OnActiveAgain()
    {
        isTimeActive = true;
        StartCoroutine(TestTimeBar());
    }

    public void UpdateTimeBar()
    {
        _startFLOAT_time -= 0.1059f;
        TimeBar_SetValue(_startFLOAT_time, _maxFLOAT_time);
    }

    IEnumerator TestTimeBar()
    {
        while (isTimeActive)
        {
            if (_startFLOAT_time <= 0f)
            {
                isTimeActive = false;
                yield return null;
                Hide();
                GUI_Resultmenu.instance.Show();
            }
            UpdateTimeBar();
            yield return new WaitForSeconds(.2f);
        }
    }
    #endregion

    public void TimeBar_SetValue(float _nowValue, float _maxValue)
    {
        float _tempValue = _nowValue / _maxValue;
        timebar_Right.value = _tempValue ;
        timebar_Left.value = _tempValue ;
    }

	// Use this for initialization
	void Start () {
        instance = this;
       
        //if (isTimeActive)
        //{
        //    StartCoroutine(TestTimeBar());
        //    StartCoroutine(TestCount());
        //}
	}

//    void Update()
//    {
//        //if (Input.GetKeyDown(KeyCode.Q))
//        //{
//        //    Debug.Log("Q");
//        //    StopAllCoroutines();
//        //    GUI_Pausemenu.instance.Show();
//        //    isTimeActive = false;
//        //}
//
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            SetBulletAmount(1);
//        }
//        else if (Input.GetKeyDown(KeyCode.S))
//        {
//            SetBulletAmount(2);
//        }
//        else if (Input.GetKeyDown(KeyCode.D))
//        {
//            SetBulletAmount(3);
//        }
//        else if (Input.GetKeyDown(KeyCode.F))
//        {
//            SetBulletAmount(0);
//        }
//    }
}
