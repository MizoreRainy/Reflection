using UnityEngine;
using System.Collections;

public class GUI_ShakingText : MonoBehaviour {

    private Vector3 originPosition;
    private float shake_decay;
    private bool _isEndless;
    private float permanent_intensity;
    [HideInInspector] public float shake_intensity;

    // Use this for initialization
    void Start()
    {
        originPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
            shake_intensity -= shake_decay;
        } 
        else
        {
            if(_isEndless)
            {
                shake_intensity = permanent_intensity;
            }
        }
    }

    public void Shake(float intensity, float decay ,bool _endless)
    {
        _isEndless = _endless;
        if (_isEndless)
        {
            permanent_intensity = intensity;
        }

        shake_intensity = intensity;
        shake_decay = decay;
    }

    public void StopShake()
    {
        _isEndless = false;
        shake_intensity = 0;
    }
}
