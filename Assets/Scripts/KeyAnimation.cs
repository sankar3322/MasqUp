using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyAnimation : MonoBehaviour, IPointerClickHandler
{

   /* public GameObject popup;
    public GameObject button;
*/
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("String");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }
}