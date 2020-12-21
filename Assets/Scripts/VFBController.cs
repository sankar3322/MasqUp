using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VFBController : MonoBehaviour
{
    public static VFBController VFB;

    private void Awake()
    {
        if (VFBController.VFB == null) VFBController.VFB = this;
        else
        {
            if (VFBController.VFB != this) Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PunchScaleNow(Transform _transform, float _delay, float _duration)
    {
        _transform.DOPunchScale(Vector3.one, 1f);
    }
}
