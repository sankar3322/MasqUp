using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private int healthMax;
    private int attackValue=0;
    private int defenceValue=0;
    private string infectionInit = "INFECTION LEVEL ", attackInit = "ATTACK - ", defenceInit = "DEFENCE - ";
    public Text coinText, infectionText, attackText, defenceText, civilianSecondStatus, civilianFirstStatus, civilianFirstMaskProText, civilianSecondMaskProText, 
        youChanceOfInfectionText, civilianChanceOfInfectionText, infectedOrNotText, civilianStatusOneText, civilianStatusSecondText, civilianTopText, medicineText;

    public GameObject civilianFirstPopup, civiliainSecondPopup, virusFirstPopup, virusSecondPopup, deadPopup, civilianFirstImage, civilianSecondImage, continueBtn, carryOnBtn, virusInfectionText, ignoreBtn;

    List<int>  firstDisbleList, unblockList1, unblockList2;
    string civilianType;
    Sprite civilianMaskedSprite, civilianNoMaskSprite;
    public int youChanceInfection, civilianChanceInfection, civilianInfectedStatus, virusChanceHit, hitValue;
    bool isInfected;

    public Text infectionNLText, coinNLText, medsNLText, attackNLText, defenceNLText, civilianCountNLText;

    public GameObject nextLevelPopup, nextLevelbtn;
    int level = 0;
    TilesController tilesController;



    private void Start()
    {

     
        PlayerPrefs.SetInt("ATTACK",5);
        PlayerPrefs.SetInt("DEFENCE", 5);
        if (PlayerPrefs.GetInt("CIVILIAN_COUNT") == null)
        {
            PlayerPrefs.SetInt("CIVILIAN_COUNT", 0);
        }

       
        if(PlayerPrefs.GetInt("MEDICINE")==0 || PlayerPrefs.GetInt("MEDICINE") == null)
        {
            PlayerPrefs.SetInt("MEDICINE", 2);
        }
        
        medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        isInfected = false;
       
      
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);
        }
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
        GameObject go = GameObject.Find("TilesController");
        tilesController = go.GetComponent<TilesController>();
        firstDisbleList = tilesController.firstDisbleList;
        firstDisbleList.Add(32);

        attackText.text = PlayerPrefs.GetInt("ATTACK").ToString();
        defenceText.text = PlayerPrefs.GetInt("DEFENCE").ToString()+"%";




        civilianMaskedSprite = Resources.Load("Masked", typeof(Sprite)) as Sprite;
        civilianNoMaskSprite = Resources.Load("NoMasked", typeof(Sprite)) as Sprite;
        DOTween.Sequence()
         .Append(nextLevelbtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
         .AppendInterval(0.1f).SetLoops(-1);
    }

    // Its is Used to Change the Coni Text Value
    public void coinTextChange()
    {
        coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
    }

    public void medsTextChange()
    {
        PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") + 1);

        medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
    }

    public void nextLevel() {


           nextLevelPopup.SetActive(true);
        /*   for (int i = 0; i <= 34; i++)
          {
              if (firstDisbleList.Contains(i))
              {
                      GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false; 
              }
          }*/

        tilesController.isPopup = true;
       
        infectionNLText.text = PlayerPrefs.GetInt("Infection")+"%";

        coinNLText.text = PlayerPrefs.GetInt("CoinCount").ToString();
        medsNLText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        attackNLText.text = PlayerPrefs.GetInt("ATTACK").ToString();
        defenceNLText.text = PlayerPrefs.GetInt("DEFENCE").ToString();
        civilianCountNLText.text = PlayerPrefs.GetInt("CIVILIAN_COUNT").ToString();
        
    }
    public void nextLevelBtn()
    {

        SceneManager.LoadScene(1);

        if (PlayerPrefs.GetInt("Level").Equals(0))
        {
            level = 1;
        }
        else
        {
            level = PlayerPrefs.GetInt("Level") + 1;
        }
        PlayerPrefs.SetInt("Level", level);
    }


    public void takeMedsValidationNLPopup()
    {
        if (PlayerPrefs.GetInt("Infection") > 0 && PlayerPrefs.GetInt("Infection") <= 100)
        {

            if (PlayerPrefs.GetInt("MEDICINE") > 0 && PlayerPrefs.GetInt("MEDICINE") <= 100)
            {

                PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
                if (PlayerPrefs.GetInt("Infection") <= 10)
                {
                    PlayerPrefs.SetInt("Infection", 0);

                }
                else
                {

                    PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection") - 10);
                }

                Debug.Log("TakeMeds");
                medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
            }

        }
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection") +"%";
        nextLevel();
    }




    // Its is Used to Show the Civilian First Popup
    public void showCivilianFirst(string CivilianName) {



        tilesController.isPopup = true;
      /*  DOTween.Sequence()
           .Append(continueBtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);

        DOTween.Sequence()
         .Append(ignoreBtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
         .AppendInterval(0.1f).SetLoops(-1);*/

        civilianType = CivilianName;
        civilianPopupStatus(civilianFirstStatus);

        civilianInfectedStatus = 1;

        if (civilianInfectedStatus == 0)
        {
            civilianStatusOneText.text = "HEALTHY";

        }
        else {
            isInfected = true;
            civilianStatusOneText.text = "INFECTED";
        }
        Debug.Log("civilianInfectedStatus" + civilianInfectedStatus);

        if (civilianInfectedStatus == 0) {

            youChanceInfection = 0;

        } else { 

            if (civilianType.Equals("C1"))
            {
                civilianFirstImage.GetComponent<Image>().sprite = civilianNoMaskSprite;
                civilianFirstMaskProText.text = "NO MASK";
                youChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 0);

            }
            else {
                civilianFirstImage.GetComponent<Image>().sprite = civilianMaskedSprite;
                civilianFirstMaskProText.text = "MASKED";
                youChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 30);

            }
        }


        youChanceOfInfectionText.text = "CHANCE OF INFECTION "+ youChanceInfection.ToString()+ "%";

        if (PlayerPrefs.GetInt("Infection") == 0 )
        {

            civilianChanceInfection = 0;

        }
        else
        {

            if (civilianType.Equals("C1"))
            {
                civilianFirstImage.GetComponent<Image>().sprite = civilianNoMaskSprite;
                civilianFirstMaskProText.text = "NO MASK";
                civilianChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 0);

            }
            else
            {
                civilianFirstImage.GetComponent<Image>().sprite = civilianMaskedSprite;
                civilianFirstMaskProText.text = "MASKED";
                civilianChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 30);

            }
        }
        firstDisbleList.Add(PlayerPrefs.GetInt("Monster1"));
        firstDisbleList.Add(PlayerPrefs.GetInt("Monster2"));
        civilianChanceOfInfectionText.text = "CHANCE OF INFECTION " + civilianChanceInfection.ToString() + "%";
        Debug.Log(civilianChanceInfection);
      /*  for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") *//*&& i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2")*//*)
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                }
            }
        }*/
        civilianFirstPopup.SetActive(true);
      
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

            if (PlayerPrefs.GetInt("Infection") >= 5)
            {
                civilianStatusSecondText.text = "INFECTED";
                civilianTopText.text = "CIVILIAN WAS INFECTED";
                isInfected = true;
            }
            else
            {
                civilianStatusSecondText.text = "HEALTHY";
                civilianTopText.text = "CIVILIAN WAS NOT INFECTED";

            }
        }
        else
        {

            civilianStatusSecondText.text = "INFECTED";
            civilianTopText.text = "CIVILIAN WAS INFECTED";
            isInfected = true;
        }
        int infectedOrNot = Random.Range(0, 100);
        Debug.Log("infectedOrNot" + infectedOrNot);

        if (infectedOrNot > youChanceInfection)
        {
            infectedOrNotText.text = "YOU WERE NOT INFECTED";

        }
        else
        {


            if (civilianType.Equals("C2") && civilianInfectedStatus == 0)
            {
                infectedOrNotText.text = "YOU WERE NOT INFECTED";

            }
            else
            {
                PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection") + 5);
                infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
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


    public void giveMedsValidation() {
       
        if (isInfected) {
            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
            civilianStatusSecondText.text = "HEALTHY";
            civilianTopText.text = "CIVILIAN WAS NOT INFECTED";
            Debug.Log("GiveMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
            isInfected = false;
        }
        }


    // Its is Used to Hide the all Popup
    public void hidePopup()
    {

        tilesController.isPopup = false;
        Debug.Log("Civilian Count" + PlayerPrefs.GetInt("CIVILIAN_COUNT"));
        Debug.Log(isInfected);
        if (!isInfected)
        {
            PlayerPrefs.SetInt("CIVILIAN_COUNT", PlayerPrefs.GetInt("CIVILIAN_COUNT") + 1);
            Debug.Log("Civilian Count" + PlayerPrefs.GetInt("CIVILIAN_COUNT"));

        }
       /* for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") *//*&& i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2")*//*)
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = true;

                }
            }
        }*/
        unblockList1 = tilesController.unBlockList1;
        unblockList2 = tilesController.unBlockList2;

 /*       if (unblockList1.Count != 0) { 
        for (int i = 0; i < unblockList1.Count; i++) {

            GameObject.Find(unblockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                Debug.Log(unblockList1.Count);
            }
    }
        if (unblockList2.Count != 0)
        {
            for (int i = 0; i < unblockList2.Count; i++)
            {

                GameObject.Find(unblockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                
                Debug.Log(unblockList2.Count);
            }
        }
       */
        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(false);

        virusSecondPopup.SetActive(false);
        virusFirstPopup.SetActive(false);

        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);


        /*    for (int i = 0; i <= 34; i++)
            {
                if (firstDisbleList.Contains(i))
                {
                    if (i != PlayerPrefs.GetInt("Civilian1") && i != PlayerPrefs.GetInt("Civilian2") *//*&& i != PlayerPrefs.GetInt("Monster1") && i != PlayerPrefs.GetInt("Monster2")*//*)
                    {
                        GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                    }
                }
            }*/
         
            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
            deadPopup.SetActive(true);
        }

    }

  


    // Its is Used to Show the Virus First Popup
    public void showVirusFirst() {


        StartCoroutine(virusDelayPopup());
      
    }

    IEnumerator virusDelayPopup()
    {
    
        virusFirstPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        virusFirstPopup.SetActive(false);
       
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);
/*

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
*/
            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
            deadPopup.SetActive(true);
        }
    }


        // Its is Used to Show the Virus Second Popup
        public void virusValidation() {
        hitValue = Random.Range(0,100);

        virusChanceHit = 100 - PlayerPrefs.GetInt("DEFENCE");
        Debug.Log("hitValue   "+hitValue+"virusChanceHit   "+virusChanceHit);
        if (hitValue < virusChanceHit)
        {
            virusInfectionText.GetComponent<TextMeshProUGUI>().text = "INFECTED +5";
            Damage(5);
           
        }
        else {
            virusInfectionText.GetComponent<TextMeshProUGUI>().text = "PROTECTED BY MASK (" + PlayerPrefs.GetInt("DEFENCE") + "%"+")";

        }
        }


    public void takeMedsValidation() {


        if (PlayerPrefs.GetInt("Infection") > 0 && PlayerPrefs.GetInt("Infection") <= 100)
        {

            if (PlayerPrefs.GetInt("MEDICINE") > 0 && PlayerPrefs.GetInt("MEDICINE") <= 100) { 

            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
                if (PlayerPrefs.GetInt("Infection") <= 10)
                {
                    PlayerPrefs.SetInt("Infection", 0);

                }
                else {

                    PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection") - 10);
                }
           
            Debug.Log("TakeMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        }
        }
        civilianPopupStatus(civilianSecondStatus);
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";

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
        if (health > 100)
        {
            health = 100;
        }
        PlayerPrefs.SetInt("Infection", health);
        infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
        attackText.text = PlayerPrefs.GetInt("ATTACK").ToString();
        defenceText.text = PlayerPrefs.GetInt("DEFENCE").ToString()+"%";
       
        return health;
     }



    public float GetHealthPercent() {
        return (float)health / healthMax;
    
    }

    // Its is Used to Calculate the civilian status Values
    public void civilianPopupStatus(Text civilianStatus) {

      /*  if (PlayerPrefs.GetInt("Infection") >= 75)
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
        else if (PlayerPrefs.GetInt("Infection") >= 1 && PlayerPrefs.GetInt("Infection") <= 25)
        {
            civilianStatus.text = "MILD";
        }
        else {

            civilianStatus.text = "HEALTHY";
        }*/
        civilianStatus.text = PlayerPrefs.GetInt("Infection") + "%";

    }


    public void BuyMedicine() {

        if (PlayerPrefs.GetInt("CoinCount") >= 5)
        {

            PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 5);
            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") + 1);
            coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
            coinNLText.text = PlayerPrefs.GetInt("CoinCount").ToString();

            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
            medsNLText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        } 
    
    }


    public int Heal(int healAmount) {

        health -= healAmount;
        if (health < 0) health = 0;
        return health;
    }
}
