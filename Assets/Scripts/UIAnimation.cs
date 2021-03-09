using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{


    private void Update()
    {
        // transform.DOPunchScale(new Vector3(2f,2f,0),0.1f,1, 0.1f);

        //transform.DOShakeScale(0.2f, new Vector3(0.05f, 0f, 0.05f), 10, 0);
        

        //transform.DOPunchScale(new Vector2(1.4f, 1f), 0.4f, 10, 1).SetEase(Ease.InCirc);
        //transform.DOShakeScale(0.2f, new Vector3(0.05f, 0f, 0.05f), 10, 0);
       // transform.DOLocalRotate(new Vector3(0,180,0),1f);
    }



    public void textUpdate() {

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
