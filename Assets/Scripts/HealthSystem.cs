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
        chanceOfInfectionText, infectedOrNotText, civilianStatusOneText, civilianStatusSecondText, civilianTopText, virusInfectionText, medicineText;

    public GameObject civilianFirstPopup, civiliainSecondPopup, virusFirstPopup, virusSecondPopup, deadPopup, civilianFirstImage, civilianSecondImage, continueBtn, carryOnBtn;


    List<int>  firstDisbleList;
    string civilianType;
    Sprite civilianMaskedSprite, civilianNoMaskSprite;
    public int chanceInfection, civilianInfectedStatus, virusChanceHit, maskValue, hitValue;
    bool isInfected;
    

    

    private void Start()
    {
        
        if(PlayerPrefs.GetInt("MEDICINE")==0 || PlayerPrefs.GetInt("MEDICINE") == null)
        {
            PlayerPrefs.SetInt("MEDICINE", 100);
        }
        
        medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        isInfected = false;
        maskValue = 5;
        PlayerPrefs.SetInt("PLAYER_DEFENCE",5);
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
           
            PlayerPrefs.SetInt("Infection", 0);
        }
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
        GameObject go = GameObject.Find("TilesController");
        TilesController tilesController = go.GetComponent<TilesController>();
        firstDisbleList = tilesController.firstDisbleList;
        firstDisbleList.Add(32);
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


    public void giveMedsValidation() {
       
        if (isInfected) {
            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
            civilianStatusSecondText.text = "HEALTHY";
            civilianTopText.text = "CIVILIAN WERE NOT INFECTED";
            Debug.Log("GiveMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
            isInfected = false;
        }
        }

    // Its is Used to Show the Civilian Second Popup
    public void showCivilianSecond()
    {
        civilianPopupStatus(civilianSecondStatus);
        DOTween.Sequence()
           .Append(carryOnBtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);
        if (civilianInfectedStatus == 0)
        {

            if (PlayerPrefs.GetInt("Infection") >=5)
            {
                civilianStatusSecondText.text = "INFECTED";
                civilianTopText.text = "CIVILIAN WERE INFECTED";
                isInfected = true;
            }
            else
            {
                civilianStatusSecondText.text = "HEALTHY";
                civilianTopText.text = "CIVILIAN WERE NOT INFECTED";
               
            }
        }
        else
        {

            civilianStatusSecondText.text = "INFECTED";
            civilianTopText.text = "CIVILIAN WERE INFECTED";
            isInfected = true;
        }
        int infectedOrNot = Random.Range(0,100);
        Debug.Log("infectedOrNot"+ infectedOrNot);

        if (infectedOrNot > chanceInfection)
        {
            infectedOrNotText.text = "YOU WERE NOT INFECTED";

        }
        else {


            if (civilianType.Equals("C2") && civilianInfectedStatus == 0) {
                infectedOrNotText.text = "YOU WERE NOT INFECTED";

            }
            else { 
            PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection")+10);
             infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");
            infectedOrNotText.text = "YOU WERE INFECTED";
            }
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
       
        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(true);
    }


    // Its is Used to Show the Virus First Popup
    public void showVirusFirst() {


        StartCoroutine(virusDelayPopup());
      
    }

    IEnumerator virusDelayPopup()
    {
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

        yield return new WaitForSeconds(1f);

        virusFirstPopup.SetActive(false);
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


        // Its is Used to Show the Virus Second Popup
        public void virusValidation() {
        hitValue = Random.Range(0,100);

        virusChanceHit = 100 - maskValue;


        Debug.Log("hitValue   "+hitValue+"virusChanceHit   "+virusChanceHit);
        if (hitValue < virusChanceHit)
        {
            virusInfectionText.text = "INFECTED +5";
            Damage(5);
           
        }
        else {
            virusInfectionText.text = "PROTECTED BY MASK ("+maskValue+"%"+")";

        }
        }



    public void takeMedsValidation() {

       
        if (PlayerPrefs.GetInt("Infection") >=10  && PlayerPrefs.GetInt("Infection") <= 100) {

            if(PlayerPrefs.GetInt("MEDICINE")>0 && PlayerPrefs.GetInt("MEDICINE")<=100)

            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
            PlayerPrefs.SetInt("Infection",  PlayerPrefs.GetInt("Infection") - 10);
            Debug.Log("TakeMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();

        }

        civilianPopupStatus(civilianSecondStatus);
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection");

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

        if (PlayerPrefs.GetInt("Infection") >= 75)
        {
            civilianStatus.text = "CRITICAL";
        }
        else if (PlayerPrefs.GetInt("Infection") >= 50 && PlayerPrefs.GetInt("Infection") <= 75)
        {
            civilianStatus.text = "HIGH";
        }
        else if (PlayerPrefs.GetInt("Infection") >= 25 && PlayerPrefs.GetInt("Infection") <= 50)
        {
            civilianStatus.text = "SERIOUS";
        }
        else if (PlayerPrefs.GetInt("Infection") >= 0 && PlayerPrefs.GetInt("Infection") <= 25)
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
