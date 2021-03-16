using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private string infectionInit = "INFECTION LEVEL ";
    public Text coinText, infectionText, attackText, defenceText, civilianSecondStatus, civilianFirstStatus, civilianFirstMaskProText, civilianSecondMaskProText, 
        youChanceOfInfectionText, civilianChanceOfInfectionText, infectedOrNotText, civilianStatusOneText, civilianStatusSecondText, civilianTopText, medicineText, maskProtectionYouValue, civilianCountText;

    public GameObject civilianFirstPopup, civiliainSecondPopup, virusFirstPopup, virusSecondPopup, deadPopup, civilianFirstImage, civilianSecondImage, continueBtn, carryOnBtn, virusInfectionText, ignoreBtn;

    List<int>  firstDisbleList, unblockList1, unblockList2;
    string civilianType;
    Sprite civilianMaskedSprite, civilianNoMaskSprite, civilianNoMaskHappySprite;
    public int youChanceInfection, civilianChanceInfection, civilianInfectedStatus, virusChanceHit, hitValue;
    bool isInfected;

    public Text infectionNLText, coinNLText, medsNLText, attackNLText, defenceNLText, civilianCountNLText;

    public ParticleSystem takeMedsEffects, virusEffect;

    public GameObject nextLevelPopup, nextLevelbtn;
    int level = 0;
    TilesController tilesController;
    Sprite m1, m2, m3, m4, m5, m6;

    public Image maskImage;

    VisualController vc;


    public Sprite[] sadCivilianSprites, happyCivilianSprites, maskPopupSprites;



    private void Start()
    {
        

      /*  DOTween.Sequence()
           .Append(coinText.transform.DOPunchRotation(Vector3.one * 0.5f * 3, 1f, vibrato: 2, elasticity: 3f))
           .AppendInterval(0.1f).SetLoops(-1);*/
            

        //coinText.transform.DOShakeScale(0.60f, new Vector3(0.05f, 0f, 0.05f), 10, 0);



        vc = GetComponent<VisualController>();
      
 
        m1 = Resources.Load("m1", typeof(Sprite)) as Sprite;
        m2 = Resources.Load("m2", typeof(Sprite)) as Sprite;
        m3 = Resources.Load("m3", typeof(Sprite)) as Sprite;
        m4 = Resources.Load("m4", typeof(Sprite)) as Sprite;
        m5 = Resources.Load("m5", typeof(Sprite)) as Sprite;
        m6 = Resources.Load("m6", typeof(Sprite)) as Sprite;

        if (PlayerPrefs.GetInt("DEFENCE") == 5)
        {
            maskImage.sprite = m1;


        }
        else if (PlayerPrefs.GetInt("DEFENCE") == 10)
        {
            maskImage.sprite = m2;

        }
        else if (PlayerPrefs.GetInt("DEFENCE") == 25)
        {
            maskImage.sprite = m3;

        }
        else if (PlayerPrefs.GetInt("DEFENCE") == 30)
        {
            maskImage.sprite = m4;

        }
        else if (PlayerPrefs.GetInt("DEFENCE") == 50)
        {
            maskImage.sprite = m5;

        }
        else if (PlayerPrefs.GetInt("DEFENCE") == 90)
        {
            maskImage.sprite = m6;

        }

        //  PlayerPrefs.SetInt("CoinCount", 200);
        takeMedsEffects.Stop();
        virusEffect.Stop();

     
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
        tilesController.isPopup = false;
        attackText.text = PlayerPrefs.GetInt("ATTACK").ToString();
        defenceText.text = PlayerPrefs.GetInt("DEFENCE").ToString()+"%";
        civilianCountText.text = PlayerPrefs.GetInt("CIVILIAN_COUNT").ToString();
        coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();


        textUpdate(attackText);
        textUpdate(defenceText);
        textUpdate(civilianCountText);
        textUpdate(medicineText);
        textUpdate(coinText);


        civilianMaskedSprite = Resources.Load("Masked", typeof(Sprite)) as Sprite;
        civilianNoMaskSprite = Resources.Load("NoMasked", typeof(Sprite)) as Sprite;
        civilianNoMaskHappySprite = Resources.Load("No Masked Happy", typeof(Sprite)) as Sprite;

        DOTween.Sequence()
         .Append(nextLevelbtn.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
         .AppendInterval(0.1f).SetLoops(-1);
    }

    public void virusParticleEffect() {

        virusEffect.Play();
    }

    // Its is Used to Change the Coni Text Value
    public void coinTextChange()
    {

       
        coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();

        textUpdate(coinText);
    }

    public void medsTextChange()
    {
        PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") + 1);

        medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
        textUpdate(medicineText);
    }

    public void nextLevel() {


           nextLevelPopup.SetActive(true);
      

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
                takeMedsEffects.Play();
                Debug.Log("TakeMeds");
                medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
                textUpdate(medicineText);
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
        maskProtectionYouValue.text = PlayerPrefs.GetString("MASK_TYPE");

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
                civilianFirstImage.GetComponent<Image>().sprite = sadCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")];
                civilianFirstMaskProText.text = "NO MASK";
                youChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 0);

            }
            else {
                civilianFirstImage.GetComponent<Image>().sprite = maskPopupSprites[PlayerPrefs.GetInt("MaskCivilian")];
                civilianFirstMaskProText.text = "MASKED";
                youChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 30);

            }
        }

        if (youChanceInfection<0) {
            youChanceInfection = 0;
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
                civilianFirstImage.GetComponent<Image>().sprite = sadCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")]; ;
                civilianFirstMaskProText.text = "NO MASK";
                civilianChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 0);

            }
            else
            {
                civilianFirstImage.GetComponent<Image>().sprite = maskPopupSprites[PlayerPrefs.GetInt("MaskCivilian")]; ;
                civilianFirstMaskProText.text = "MASKED";
                civilianChanceInfection = 100 - (PlayerPrefs.GetInt("DEFENCE") + 30);

            }
        }
        firstDisbleList.Add(PlayerPrefs.GetInt("Monster1"));
        firstDisbleList.Add(PlayerPrefs.GetInt("Monster2"));
        civilianChanceOfInfectionText.text = "CHANCE OF INFECTION " + civilianChanceInfection.ToString() + "%";
        Debug.Log(civilianChanceInfection);
  
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
        if (civilianType.Equals("C1"))
        {
            civilianSecondImage.GetComponent<Image>().sprite = sadCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")]; ;
            civilianSecondMaskProText.text = "NO MASK";
        }
        else
        {
            civilianSecondImage.GetComponent<Image>().sprite = maskPopupSprites[PlayerPrefs.GetInt("MaskCivilian")]; ;
            civilianSecondMaskProText.text = "MASKED";

        }

        if (infectedOrNot > youChanceInfection)
        {
            infectedOrNotText.text = "YOU WERE NOT INFECTED";

        }
        else
        {


            if (civilianType.Equals("C2") && civilianInfectedStatus == 0)
            {
                infectedOrNotText.text = "YOU WERE NOT INFECTED";
                if (civilianType.Equals("C1"))
                {
                    civilianSecondImage.GetComponent<Image>().sprite = happyCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")]; ;
                }

            }
            else
            {
                PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection") + 5);
                infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
                infectedOrNotText.text = "YOU WERE INFECTED";
            }
        }
       

        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(true);
    }


    public void giveMedsValidation() {
       
        if (isInfected) {
            if (PlayerPrefs.GetInt("MEDICINE") > 0) { 
            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
            civilianStatusSecondText.text = "HEALTHY";
            civilianTopText.text = "CIVILIAN WAS NOT INFECTED";
            Debug.Log("GiveMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
                textUpdate(medicineText);
                if (civilianType.Equals("C1"))
                {
                    civilianSecondImage.GetComponent<Image>().sprite = happyCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")];
                }
                else {
                    civilianSecondImage.GetComponent<Image>().sprite = maskPopupSprites[PlayerPrefs.GetInt("MaskCivilian")];
                }

                 textShowValidation("THANK YOU DOCTOR!!!");
                PlayerPrefs.SetInt("CIVILIAN_COUNT", PlayerPrefs.GetInt("CIVILIAN_COUNT") + 1);
                civilianCountText.text = PlayerPrefs.GetInt("CIVILIAN_COUNT").ToString();
                textUpdate(civilianCountText);
                Debug.Log("Civilian Count" + PlayerPrefs.GetInt("CIVILIAN_COUNT"));
                isInfected = false;
                vc.audioSource.clip = vc.audioClips[1];
                 vc.audioSource.Play();

            
            }
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
           
            Debug.Log("Civilian Count" + PlayerPrefs.GetInt("CIVILIAN_COUNT"));

        }
      
        unblockList1 = tilesController.unBlockList1;
        unblockList2 = tilesController.unBlockList2;


        civilianFirstPopup.SetActive(false);
        civiliainSecondPopup.SetActive(false);

        virusSecondPopup.SetActive(false);
        virusFirstPopup.SetActive(false);

        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);
         
            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
            tilesController.isPopup = true;
            deadPopup.SetActive(true);
        }

    }

  


    // Its is Used to Show the Virus First Popup
    public void showVirusFirst() {


        StartCoroutine(virusDelayPopup());
      
    }

    public void textShowValidation(string textValue) {


        StartCoroutine(textValidation(textValue));

    }


    IEnumerator textValidation(string textValue) {


        Debug.Log(textValue);
   
        virusFirstPopup.SetActive(true);
        virusInfectionText.GetComponent<TextMeshProUGUI>().text = textValue;
        yield return new WaitForSeconds(1f);
        virusFirstPopup.SetActive(false);

    }

    IEnumerator virusDelayPopup()
    {
    
        virusFirstPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        virusFirstPopup.SetActive(false);
       
        if (PlayerPrefs.GetInt("Infection") >= 100)
        {
            PlayerPrefs.SetInt("Infection", 0);
            infectionText.text = infectionInit + PlayerPrefs.GetInt("Infection")+"%";
            tilesController.isPopup = true;
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
            
            if (PlayerPrefs.GetInt("MEDICINE") > 0) { 

            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") - 1);
                if (PlayerPrefs.GetInt("Infection") <= 10)
                {
                    PlayerPrefs.SetInt("Infection", 0);

                }
                else {

                    PlayerPrefs.SetInt("Infection", PlayerPrefs.GetInt("Infection") - 10);
                }
            
                takeMedsEffects.Play();
                Debug.Log("TakeMeds");
            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
                textUpdate(medicineText);
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
        //textUpdate(defenceText);
        return health;
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

        if (PlayerPrefs.GetInt("CoinCount") >= 2)
        {

            PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
            PlayerPrefs.SetInt("MEDICINE", PlayerPrefs.GetInt("MEDICINE") + 1);
            coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
            coinNLText.text = PlayerPrefs.GetInt("CoinCount").ToString();

            medicineText.text = PlayerPrefs.GetInt("MEDICINE").ToString();
            medsNLText.text = PlayerPrefs.GetInt("MEDICINE").ToString();

            textUpdate(coinText);
            textUpdate(medicineText);
        } 
    
    }


    public void BuyMask() {
        if (PlayerPrefs.GetInt("CoinCount") >= 2)
        {
          
            

            if (PlayerPrefs.GetInt("DEFENCE") == 5) {

                PlayerPrefs.SetInt("DEFENCE", 10);
                PlayerPrefs.SetString("MASK_TYPE", "Homemade Mask");
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
                maskImage.sprite = m2;

            } else if (PlayerPrefs.GetInt("DEFENCE") == 10) {

                PlayerPrefs.SetInt("DEFENCE", 25);
                PlayerPrefs.SetString("MASK_TYPE", "Mask name 3");
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
                maskImage.sprite = m3;
            }
            else if (PlayerPrefs.GetInt("DEFENCE") == 25)
            {

                PlayerPrefs.SetInt("DEFENCE", 30);
                PlayerPrefs.SetString("MASK_TYPE", "Mask 4");
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
                maskImage.sprite = m4;
            }
            else if (PlayerPrefs.GetInt("DEFENCE") == 30)
            {
                PlayerPrefs.SetInt("DEFENCE", 50);
                PlayerPrefs.SetString("MASK_TYPE", "Mask 5");
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
                maskImage.sprite = m5;

            }
            else if (PlayerPrefs.GetInt("DEFENCE") == 50)
            {

                PlayerPrefs.SetInt("DEFENCE", 90);
                PlayerPrefs.SetString("MASK_TYPE", "Mask 6");
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") - 2);
                maskImage.sprite = m6;
            }

            defenceText.text = PlayerPrefs.GetInt("DEFENCE").ToString() + "%";

            textUpdate(defenceText);

            coinText.text = PlayerPrefs.GetInt("CoinCount").ToString();
            coinNLText.text = PlayerPrefs.GetInt("CoinCount").ToString();

            textUpdate(coinText);

        }


    }


    public int Heal(int healAmount) {

        health -= healAmount;
        if (health < 0) health = 0;
        return health;
    }




    public void textUpdate(Text text)
    {

        StartCoroutine(UpdateScoreText(text));
    }


    protected virtual IEnumerator UpdateScoreText(Text text)
    {
        text.transform.DOScale(new Vector3(0.8f, 1f, 0.8f), .2f);

        yield return new WaitForSeconds(0.3f);


        text.transform.DOPunchRotation(new Vector3(180, 0), 1.0f, 8, 0.5f);

        yield return new WaitForSeconds(0.6f);

        text.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), .2f);

       


    }
}
