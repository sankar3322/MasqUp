using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    public void textUpdate()
    {
        StartCoroutine(UpdateScoreText());
    }

    protected virtual IEnumerator UpdateScoreText()
    {
        transform.DOScale(new Vector2(1f, 2f), .2f);
        yield return new WaitForSeconds(0.3f);
        transform.DOPunchRotation(new Vector3(180, 0), 0.8f, 8, 0.5f);
        yield return new WaitForSeconds(0.6f);
        transform.DOScale(new Vector2(1f, 1f), .2f);
    }
}
