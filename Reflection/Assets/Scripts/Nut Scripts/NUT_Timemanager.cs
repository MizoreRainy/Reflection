using UnityEngine;
using System;
using System.Collections;

public class NUT_Timemanager : MonoBehaviour {

    public float time_Limit;

    public static NUT_Timemanager instance;
    private float max_Time;
    private float permanent_Time;
    private bool activeFromPause = false;
    private bool timeCounting = false;

    public void StartTimeCount()
    {
        if (timeCounting)
        { return; }

        if (activeFromPause)
        { 
            time_Limit = permanent_Time; 
            activeFromPause = false;
        }
        else
        { 
            permanent_Time = time_Limit;
        }
        timeCounting = true;
        StartCoroutine(Timeticking());
        GUI_HUDIngame.instance.OnAnimateTimebar(time_Limit);
    }

    public void PauseTiming()
    {
        permanent_Time = time_Limit;
        activeFromPause = true;
        StopAllCoroutines();
    }

    IEnumerator Timeticking()
    {
        while(timeCounting)
        {
            time_Limit--;
            yield return  new WaitForSeconds(1f);
            if(time_Limit <= 0)
            {
                timeCounting = false;
                Debug.Log("TIME OUT!");
                yield return null;
                time_Limit = max_Time;
                yield return null;
                //Time over!
            }
        }
    }

	// Use this for initialization
	void Awake () {
        instance = this;
        max_Time = time_Limit;
	}
}
