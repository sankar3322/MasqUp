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
    public Text coinText, infectionText, attackText, defenceText, civilianSecondStatus, civilianFirstStatus, civilianFirstMaskProText, civilianSecondMaskProText, 
        chanceOfInfectionText, infectedOrNotText, civilianStatusOneText, civilianStatusSecondText, civilianTopText;

    public GameObject civilianFirstPopup, civiliainSecondPopup, virusFirstPopup, virusSecondPopup, deadPopup, civilianFirstImage, civilianSecondImage, continueBtn, carryOnBtn;
    List<int>  firstDisbleList;
    string civilianType;
    Sprite civilianMaskedSprite, civilianNoMaskSprite;
    int chanceInfection, civilianInfectedStatus;
    

    private void Start()
    {
        PlayerPrefs.SetInt("PLAYER_DEFENCE",5);
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
           
            PlayerPrefs.SetInt("Infection", 0);
        }
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
        GameObject go = GameObject.Find("TilesController");
        TilesController tilesController = go.GetComponent<TilesController>();
        firstDisbleList = tilesController.firstDisbleList;
        civilianMaskedSprite = Resources.Load("Masked", typeof(Sprite)) as Sprite;
        civilianNoMaskSprite = Resources.Load("NoMasked", typeof(Sprite)) as Sprite;
    }


    // Its is Used to Change the Coni Text Value
    public void coinTextChange()
    {
        coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
    }

    // Its is Used to Show the Civilian First Popup
    public void showCivilianFirst(string CivilianName) {


        DOTween.Sequence()
           .Append(continueBtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);

        civilianType = CivilianName;
        civilianPopupStatus(civilianFirstStatus);

        civilianInfectedStatus = Random.Range(0, 2);

        if (civilianInfectedStatus == 0)
        {
            civilianStatusOneText.text = "HEALTHY";

        }
        else {

            civilianStatusOneText.text = "INFECTED";
        }
        Debug.Log("civilianInfectedStatus" + civilianInfectedStatus);
        if (civilianType.Equals("C1"))
        {
            civilianFirstImage.GetComponent<Image>().sprite = civilianNoMaskSprite;
            civilianFirstMaskProText.text = "NO MASK";
            chanceInfection = 100 - (PlayerPrefs.GetInt("PLAYER_DEFENCE") + 0);

        }
        else {
            civilianFirstImage.GetComponent<Image>().sprite = civilianMaskedSprite;
            civilianFirstMaskProText.text = "MASKED";
            chanceInfection = 100 - (PlayerPrefs.GetInt("PLAYER_DEFENCE") + 30);

        }
        chanceOfInfectionText.text = "CHANCE OF INFECTION "+chanceInfection.ToString()+"%";
        Debug.Log(chanceInfection);
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
      
    }

    // Its is Used to Show the Civilian Second Popup
    public void showCivilianSecond()
    {

        DOTween.Sequence()
           .Append(carryOnBtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);
        if (civilianInfectedStatus == 0)
        {
            civilianStatusSecondText.text = "HEALTHY";
            civilianTopText.text = "YOUR CIVILIAN WERE NOT INFECTED";

        }
        else
        {

            civilianStatusSecondText.text = "INFECTED";
            civilianTopText.text = "YOUR CIVILIAN WERE INFECTED";
        }
        int infectedOrNot = Random.Range(0,100);
        Debug.Log("infectedOrNot"+ infectedOrNot);
        if (infectedOrNot > chanceInfection)
        {
            infectedOrNotText.text = "YOU WERE NOT INFECTED";

        }
        else {
            PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection")+10);

            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
            infectedOrNotText.text = "YOU WERE INFECTED";
        }
        if (civilianType.Equals("C1"))
        {
            civilianSecondImage.GetComponent<Image>().sprite = civilianNoMaskSprite;
            civilianSecondMaskProText.text = "NO MASK";


        }
        else
        {
            civilianSecondImage.GetComponent<Image>().sprite = civilianMaskedSprite;
            civilianSecondMaskProText.text = "MASKED";

        }
        civilianPopupStatus(civilianSecondStatus);
        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(true);
    }


    // Its is Used to Show the Virus First Popup
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
      
    }


    // Its is Used to Show the Virus Second Popup
    public void showVirusSecond() {

        virusFirstPopup.SetActive(false);
        virusSecondPopup.SetActive(true);
    
    }


    // Its is Used to Hide the all Popup
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

    // Its is Used to Initiate the Player infection Level
    public void initHealth() {
        health = PlayerPrefs.GetInt("Infection");
    }

    // Its is Used to increse the Player infection Level
    public int Damage(int damageAmount) {
        health = PlayerPrefs.GetInt("Infection");
        health += damageAmount;
        
        Debug.Log(infectionInit + health);
        infectionText.text = infectionInit + health;
        attackText.text = attackInit + attackValue;
        defenceText.text = defenceInit + defenceValue;
        PlayerPrefs.SetInt("Infection", health);
   
        return health;
     }



    public float GetHealthPercent() {
        return (float)health / healthMax;
    
    }

    // Its is Used to Calculate the civilian status Values
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



    public int Heal(int healAmount) {

        health -= healAmount;
        if (health < 0) health = 0;
        return health;
    }
}
