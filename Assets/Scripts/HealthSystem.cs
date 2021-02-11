using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private int healthMax;

    private int attackValue=0;
    private int defenceValue=0;

    private string infectionInit = "INFECTION - ", attackInit = "ATTACK - ", defenceInit = "DEFENCE - ";

    public Text coinText, infectionText, attackText, defenceText, civilianSecondStatus, civilianFirstStatus;

   
    public GameObject civilianFirstPopup, civiliainSecondPopup, virusFirstPopup, virusSecondPopup, deadPopup;
    List<int>  firstDisbleList;



    private void Start()
    {
       
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
           
            PlayerPrefs.SetInt("Infection", 0);
        }
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
        GameObject go = GameObject.Find("TilesController");
        TilesController tilesController = go.GetComponent<TilesController>();
        firstDisbleList = tilesController.firstDisbleList;
    }


 
    public void coinTextChange()
    {
        coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
    }


    public void showCivilianFirst() {

        civilianPopupStatus(civilianFirstStatus);

        

        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") && i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2"))
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                }
            }
        }
        civilianFirstPopup.SetActive(true);
       /* DOTween.Sequence()
            .Append(civilianFirstPopup.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
            .AppendInterval(0.1f).SetLoops(-1);*/
    }

    public void showCivilianSecond()
    {
        civilianPopupStatus(civilianSecondStatus);
        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(true);
        /*  DOTween.Sequence()
             .Append(civiliainSecondPopup.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
             .AppendInterval(0.1f).SetLoops(-1);*/
    }



    public void showVirusFirst() {

        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") && i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2"))
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                }
            }
        }
        virusFirstPopup.SetActive(true);
       /* DOTween.Sequence()
            .Append(virusFirstPopup.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
            .AppendInterval(0.1f).SetLoops(-1);*/
    }

    public void showVirusSecond() {

        virusFirstPopup.SetActive(false);
        virusSecondPopup.SetActive(true);
     /*   DOTween.Sequence()
           .Append(virusSecondPopup.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);*/
    }

  

    public void hidePopup() {

        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") && i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2"))
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = true;

                }
            }
        }
        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(false);

        virusSecondPopup.SetActive(false);
        virusFirstPopup.SetActive(false);

        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);


            for (int i = 0; i <= 34; i++)
            {
                if (firstDisbleList.Contains(i))
                {
                    if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") && i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2"))
                    {
                        GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                    }
                }
            }

            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
            deadPopup.SetActive(true);
        }

    }


    public void initHealth() {
        health = PlayerPrefs.GetInt("Infection");
    }

    public int GetHealth() {
        return health;
    }

    public int Damage(int damageAmount) {
        health = PlayerPrefs.GetInt("Infection");
        health += damageAmount;
        
        Debug.Log(infectionInit + health);
        infectionText.text = infectionInit + health;
        attackText.text = attackInit + attackValue;
        defenceText.text = defenceInit + defenceValue;
        PlayerPrefs.SetInt("Infection", health);
     /*   if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            //health = 100;
            health = 0;
            PlayerPrefs.SetInt("Infection", 0);
        }*/
        return health;
     }

    public float GetHealthPercent() {
        return (float)health / healthMax;
    
    }


    public void civilianPopupStatus(Text civilianStatus) {
        if (PlayerPrefs.GetInt("Infection") > 75)
        {
            civilianStatus.text = "CRITICAL";
        }
        else if (PlayerPrefs.GetInt("Infection") > 50 && PlayerPrefs.GetInt("Infection") < 75)
        {
            civilianStatus.text = "HIGH";
        }
        else if (PlayerPrefs.GetInt("Infection") > 25 && PlayerPrefs.GetInt("Infection") < 50)
        {
            civilianStatus.text = "SERIOUS";
        }
        else if (PlayerPrefs.GetInt("Infection") > 0 && PlayerPrefs.GetInt("Infection") < 25)
        {
            civilianStatus.text = "MILD";
        }
        else {

            civilianStatus.text = "NONE";
        }

    }

    public void chanceOfInfection() {
    
    
    }


    public int Heal(int healAmount) {

        health -= healAmount;
        if (health < 0) health = 0;
        return health;
    }
}
