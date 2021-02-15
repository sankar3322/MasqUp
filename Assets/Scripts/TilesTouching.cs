using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class TilesTouching : MonoBehaviour
{


    bool isKey;
    int level=0;
   
    VisualController vc;
    public GameObject popUp;

    GameObject doorPos, centerPos, keyPos, KeyAnimObj;
    List<int> disableList, firstDisbleList;

    List<int> unBlockList1;
    List<int> unBlockList2;

    int civilian1, civilian2, key;
    int Monster1, Monster2;

    ParticleSystem particleSystem;

    int CoinCount = 0, MedicineCount = 0, MonsterHitValue;

    HealthSystem healthSystem;

  


    // Start is called before the first frame update
    void Start()
    {
        isKey = false;
        healthSystem.coinTextChange();
        MonsterHitValue = 10;
        

    }



    private void Awake()
    {
       
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        vc = GetComponent<VisualController>();
        vc.OnPressed += _ => OnClicked();
        CoinCount = PlayerPrefs.GetInt("CoinCount");
        healthSystem = GameObject.Find("HealthManager").GetComponent<HealthSystem>();
        healthSystem.initHealth();
        
    }

    void OnClicked()
    {
        Sprite greenSprite = Resources.Load("locked", typeof(Sprite)) as Sprite;
        Sprite sand = Resources.Load("opened", typeof(Sprite)) as Sprite;
        Sprite keySprite = Resources.Load("key", typeof(Sprite)) as Sprite;
        Sprite stone = Resources.Load("virus", typeof(Sprite)) as Sprite;

        Sprite coinSprite = Resources.Load("coin", typeof(Sprite)) as Sprite;
      
        Sprite civilianSprite = Resources.Load("civilian", typeof(Sprite)) as Sprite;

        Sprite civilianMaskedSprite = Resources.Load("MaskedCivilian", typeof(Sprite)) as Sprite;

        Sprite virus_block = Resources.Load("Virus_Block", typeof(Sprite)) as Sprite;

        string name = gameObject.name;

        GameObject go = GameObject.Find("TilesController");
        

        TilesController tilesController = go.GetComponent<TilesController>();
        disableList = tilesController.disableList;

        firstDisbleList = tilesController.firstDisbleList;
        unBlockList1 = tilesController.unBlockList1;
        unBlockList2 = tilesController.unBlockList2;
     

         key = PlayerPrefs.GetInt("Key");
         Monster1 = PlayerPrefs.GetInt("Monster1");
         Monster2 = PlayerPrefs.GetInt("Monster2");
      

         int coin = PlayerPrefs.GetInt("Coin");
         civilian1 = PlayerPrefs.GetInt("Civilian1");
         civilian2 = PlayerPrefs.GetInt("Civilian2");

        if (name.Equals("32") && disableList.Contains(32))
        {
         
            vc.audioSource.clip = vc.audioClips[2];
            vc.audioSource.Play();
            disableList.Remove(32);
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
      

        if (!disableList.Contains(key) && name.Equals(key.ToString()) && !isKey)
        {
            isKey = true;
            vc.audioSource.clip = vc.audioClips[3];
            vc.audioSource.Play();

            disableList.Add(32);
            
            GameObject.Find(key.ToString()).GetComponent<SpriteRenderer>().sprite = sand;
            GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = true;
            GameObject.Find(key.ToString()).GetComponent<VisualController>().isUnlocked = false;
     
            PlayerPrefs.SetInt("Key",1000);
            doorPos = GameObject.Find("32");

            centerPos = GameObject.Find("17");
            
            keyPos = GameObject.Find(key.ToString());

            KeyAnimObj = (GameObject)Instantiate(Resources.Load("KeyObj"));

            var KeyPanel = GameObject.Find("AnimKey");
            KeyAnimObj.transform.SetParent(KeyPanel.transform, false);

            KeyAnimObj.transform.position = new Vector3(keyPos.transform.position.x, keyPos.transform.position.y, -0.4f);

            StartCoroutine(keyAnimation());

        } else if (key==1000 && isKey) {

            Debug.Log("Key False");

        }
        else if (name.Equals(Monster1.ToString()) || (name.Equals(Monster2.ToString())) || name.Equals("32"))
        {

            if (name.Equals("32"))
            {
                vc.audioSource.clip = vc.audioClips[2];
                vc.audioSource.Play();
                GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
            }
            else
            {
                /*vc.audioSource.clip = vc.audioClips[7];
                vc.audioSource.Play();*/

                healthSystem.virusValidation();
                GameObject virAttackText = GameObject.Find(name.ToString()).transform.Find("VirusAttack").gameObject;

                GameObject virDefenceText = GameObject.Find(name.ToString()).transform.Find("VirusDefence").gameObject;

                MonsterHitValue = MonsterHitValue - 5;

                virAttackText.GetComponent<TextMesh>().text = "A-5";
                virDefenceText.GetComponent<TextMesh>().text = "H -"+MonsterHitValue.ToString();

                if (MonsterHitValue == 0)
                {

                

                    virAttackText.SetActive(false);
                    virDefenceText.SetActive(false);

                   

                    if (name.Equals(Monster1.ToString()))
                    {
                        for (int i = 0; i < unBlockList1.Count; i++)
                        {
                            GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                            if (unBlockList1[i] == key)
                            {

                                GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                            }
                            else
                            {

                                GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                            }
                            Debug.Log("monster1 Block");
                        }

                    }
                    if (name.Equals(Monster2.ToString()))
                    {

                        for (int i = 0; i < unBlockList2.Count; i++)
                        {

                            GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                            if (unBlockList2[i] == key)
                            {

                                GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                            }
                            else
                            {

                                GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                            }
                            Debug.Log("monster2 Block");

                        }
                    }


                    Debug.Log("Monster Pressed" + unBlockList1.Count);


                    GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = sand;
                    GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;

                }
                

               
                healthSystem.showVirusFirst();
               


            }
           
        //    Debug.Log("Monster Pressed");
          
        }
        else
        {
            firstDisbleList.Remove(int.Parse(name));

            if (!disableList.Contains(int.Parse(name)))
            {
                int c1 = int.Parse(name) - 5;
                int c2 = int.Parse(name) - 1;
                int c3 = int.Parse(name) + 1;
                int c4 = int.Parse(name) + 5;

            

                if (name.Equals(coin.ToString()))
                {
                    vc.audioSource.clip = vc.audioClips[1];
                    vc.audioSource.Play();
                    CoinCount = CoinCount + 1;

                    PlayerPrefs.SetInt("CoinCount", CoinCount);

                    healthSystem.coinTextChange();

                }
                else if (name.Equals(civilian1.ToString()) || name.Equals(civilian2.ToString()))
                {

                  /*  vc.audioSource.clip = vc.audioClips[5];
                    vc.audioSource.Play();*/

                    GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;


                    if (name.Equals(civilian1.ToString())) {

                        healthSystem.showCivilianFirst("C1");
                    }
                    else {
                        healthSystem.showCivilianFirst("C2");

                    }          
                }
                else
                {
                    vc.audioSource.clip = vc.audioClips[6];
                    vc.audioSource.Play();
                }

                GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = sand;

                if (disableList.Contains(coin) && (c1.Equals(coin) || (c2.Equals(coin) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(coin) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(coin)))
                {

                    GameObject.Find(coin.ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    GameObject.Find(coin.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    firstDisbleList.Add(coin);
                    disableList.Remove(coin);

                }
                if (disableList.Contains(civilian1) && (c1.Equals(civilian1) || (c2.Equals(civilian1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian1)))
                {

                    GameObject.Find(civilian1.ToString()).GetComponent<SpriteRenderer>().sprite = civilianSprite;
                    GameObject.Find(civilian1.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(civilian1);

                }

                if (disableList.Contains(civilian2) && (c1.Equals(civilian2) || (c2.Equals(civilian2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian2)))
                {

                    GameObject.Find(civilian2.ToString()).GetComponent<SpriteRenderer>().sprite = civilianMaskedSprite;
                    GameObject.Find(civilian2.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(civilian2);

                }


                if (c1.Equals(key) || (c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(key))
                {

                    GameObject.Find(key.ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                    GameObject.Find(key.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(key);
                    firstDisbleList.Add(key);

                }
          

                if (!c1.Equals(32) && c1 <= 34 && c1 >= 0 && disableList.Contains(c1) && !c1.Equals(key) && !c1.Equals(Monster1) && !c1.Equals(Monster2))
                {
                    GameObject.Find(c1.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c1.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c1);
                    firstDisbleList.Add(c1);
                }

                if (!c2.Equals(32) && c2 <= 34 && c2 >= 0 && disableList.Contains(c2) && !c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30") && !c2.Equals(Monster1) && !c2.Equals(Monster2))
                {
                    GameObject.Find(c2.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c2.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c2);
                    firstDisbleList.Add(c2);
                }

                if (!c3.Equals(32) && c3 <= 34 && c3 >= 0 && disableList.Contains(c3) && !c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29") && !c3.Equals(Monster1) && !c3.Equals(Monster2))
                {
                    GameObject.Find(c3.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c3.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c3);
                    firstDisbleList.Add(c3);
                }

                if (!c4.Equals(32) && c4 <= 34 && c4 >= 0 && disableList.Contains(c4) && !c4.Equals(key) && !c4.Equals(Monster1) && !c4.Equals(Monster2))
                {
                    GameObject.Find(c4.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c4.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c4);
                    firstDisbleList.Add(c4);
                }
                if (disableList.Contains(Monster1) && (c1.Equals(Monster1) || (c2.Equals(Monster1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster1)))
                {

                    GameObject.Find(Monster1.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
                    GameObject.Find(Monster1.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    /*  virusAttackText.SetActive(true);
                      virusDefenceText.SetActive(true);*/


                    GameObject virAttackText = GameObject.Find(Monster1.ToString()).transform.Find("VirusAttack").gameObject;

                    GameObject virDefenceText = GameObject.Find(Monster1.ToString()).transform.Find("VirusDefence").gameObject;

                    virAttackText.SetActive(true);
                    virDefenceText.SetActive(true);

                    virAttackText.GetComponent<TextMesh>().text="A-5".ToString();
                    virDefenceText.GetComponent<TextMesh>().text = "H-"+MonsterHitValue.ToString();



                    List<int> mList = new List<int>();
                    int m1 = Monster1 - 1;
                    int m2 = Monster1 + 1;
                    int m3 = Monster1 - 6;
                    int m4 = Monster1 - 5;
                    int m5 = Monster1 - 4;
                    int m6 = Monster1 + 4;
                    int m7 = Monster1 + 5;
                    int m8 = Monster1 + 6;

                    mList.Add(m1);
                    mList.Add(m2);
                    mList.Add(m3);
                    mList.Add(m4);
                    mList.Add(m5);
                    mList.Add(m6);
                    mList.Add(m7);
                    mList.Add(m8);

                    for (int i = 0; i < mList.Count; i++)
                    {

                        if (firstDisbleList.Contains(mList[i]))
                        {
                            if (!mList[i].Equals(32) && !mList[i].Equals(Monster1) && !mList[i].Equals(coin) && !mList[i].Equals(civilian1) && !mList[i].Equals(civilian2))
                            {

                                if (Monster1.ToString().Equals("5") || Monster1.ToString().Equals("10") || Monster1.ToString().Equals("15") || Monster1.ToString().Equals("20")
                                    || Monster1.ToString().Equals("25") || Monster1.ToString().Equals("30"))
                                {

                                    if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("4") && !mList[i].ToString().Equals("9") && !mList[i].ToString().Equals("14")
                                    && !mList[i].ToString().Equals("19") && !mList[i].ToString().Equals("24") && !mList[i].ToString().Equals("29"))
                                    {


                                        Debug.Log(mList[i]);
                                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                        unBlockList1.Add(mList[i]);

                                    }
                                }
                                else if (Monster1.ToString().Equals("4") || Monster1.ToString().Equals("9") || Monster1.ToString().Equals("14")
                                  || Monster1.ToString().Equals("19") || Monster1.ToString().Equals("24") || Monster1.ToString().Equals("29"))
                                {

                                    if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("5") && !mList[i].ToString().Equals("10") && !mList[i].ToString().Equals("15") && !mList[i].ToString().Equals("20")
                                    && !mList[i].ToString().Equals("25") && !mList[i].ToString().Equals("30"))
                                    {


                                        Debug.Log(mList[i]);
                                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                        unBlockList1.Add(mList[i]);

                                    }
                                }
                                else
                                {

                                    Debug.Log(mList[i]);
                                    GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                    GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                    unBlockList1.Add(mList[i]);

                                }
                            }
                        }


                    }
                    disableList.Remove(Monster1);
                }

                if (disableList.Contains(Monster2) && (c1.Equals(Monster2) || (c2.Equals(Monster2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster2)))
                {

                    GameObject.Find(Monster2.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
                    GameObject.Find(Monster2.ToString()).GetComponent<VisualController>().isUnlocked = true;

                    GameObject virAttackText = GameObject.Find(Monster2.ToString()).transform.Find("VirusAttack").gameObject;

                    GameObject virDefenceText = GameObject.Find(Monster2.ToString()).transform.Find("VirusDefence").gameObject;

                    virAttackText.SetActive(true);
                    virDefenceText.SetActive(true);

                    virAttackText.GetComponent<TextMesh>().text = "A-5".ToString();
                    virDefenceText.GetComponent<TextMesh>().text = "H-" + MonsterHitValue.ToString();


                    List<int> mList = new List<int>();
                  
                    int m1 = Monster2 - 1;
                    int m2 = Monster2 + 1;
                    int m3 = Monster2 - 6;
                    int m4 = Monster2 - 5;
                    int m5 = Monster2 - 4;
                    int m6 = Monster2 + 4;
                    int m7 = Monster2 + 5;
                    int m8 = Monster2 + 6;

                    mList.Add(m1);
                    mList.Add(m2);
                    mList.Add(m3);
                    mList.Add(m4);
                    mList.Add(m5);
                    mList.Add(m6);
                    mList.Add(m7);
                    mList.Add(m8);

                    for (int i = 0; i < mList.Count; i++)
                    {
                        if (firstDisbleList.Contains(mList[i]))
                        {
                            if (!mList[i].Equals(32) && !mList[i].Equals(Monster1) && !mList[i].Equals(coin) && !mList[i].Equals(civilian1) && !mList[i].Equals(civilian2))
                            {

                                if (Monster2.ToString().Equals("5") || Monster2.ToString().Equals("10") || Monster2.ToString().Equals("15") || Monster2.ToString().Equals("20")
                                    || Monster2.ToString().Equals("25") || Monster2.ToString().Equals("30"))
                                {

                                    if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("4") && !mList[i].ToString().Equals("9") && !mList[i].ToString().Equals("14")
                                    && !mList[i].ToString().Equals("19") && !mList[i].ToString().Equals("24") && !mList[i].ToString().Equals("29"))
                                    {
                                        Debug.Log(mList[i]);
                                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                        unBlockList2.Add(mList[i]);


                                    }

                                }
                                else if (Monster2.ToString().Equals("4") || Monster2.ToString().Equals("9") || Monster2.ToString().Equals("14")
                                  || Monster2.ToString().Equals("19") || Monster2.ToString().Equals("24") || Monster2.ToString().Equals("29"))
                                {

                                    if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("5") && !mList[i].ToString().Equals("10") && !mList[i].ToString().Equals("15") && !mList[i].ToString().Equals("20")
                                    && !mList[i].ToString().Equals("25") && !mList[i].ToString().Equals("30"))
                                    {


                                        Debug.Log(mList[i]);
                                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                        unBlockList2.Add(mList[i]);

                                    }


                                }
                                else
                                {

                                    Debug.Log(mList[i]);
                                    GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                                    GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                                    unBlockList2.Add(mList[i]);
                                }
                            }
                        }
                    }
                    disableList.Remove(Monster2);


                }

            }

        }
    }






    IEnumerator delayPopup(string name) {

        var panel = GameObject.Find("Canvas_Popup");


       Sprite civilianPopup = Resources.Load("Saved Popup", typeof(Sprite)) as Sprite;
        Sprite virusPopup = Resources.Load("Virus Destroyed", typeof(Sprite)) as Sprite;

        GameObject a = (GameObject)Instantiate(popUp);

        a.name = "a";
        a.transform.SetParent(panel.transform, false);


        if (name.Equals(civilian1.ToString()) || name.Equals(civilian2.ToString()))
        {
            a.GetComponent<Image>().sprite = civilianPopup;

        }
        else {

            a.GetComponent<Image>().sprite = virusPopup;
        }

        GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = false;


        for (int i = 0; i <= 34; i++) {


            if (firstDisbleList.Contains(i))
            {
                if (i != civilian1 && i != civilian2 && i !=Monster1  && i != Monster2)
                { 

                GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;

                }

            }
        }

        DOTween.Sequence()
            .Append(a.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
            .AppendInterval(0.1f).SetLoops(-1);

        yield return new WaitForSeconds(1.5f);

        if (key==1000)
        {
            GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = true;

        }
        else {
            GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = false;

         

        }
        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != civilian1 && i != civilian2 && i != Monster1 && i != Monster2)
                {
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = true;
                }
            }
        }

        Destroy(a);
       

    }


    IEnumerator keyAnimation()
    {

        Sprite openDoor = Resources.Load("doorOpened", typeof(Sprite)) as Sprite;
        KeyAnimObj.transform.DOScale(2f, 1);

        DOTween.Sequence()
           .Append(KeyAnimObj.transform.DOMove(new Vector3(centerPos.transform.position.x, centerPos.transform.position.y, -0.4f), 0.4f))
           .AppendInterval(0.5f).SetLoops(1);
        yield return new WaitForSeconds(0.4f);

        KeyAnimObj.transform.DOScale(0f, 1);

        KeyAnimObj.transform.DOMove(new Vector3(doorPos.transform.position.x, doorPos.transform.position.y, -0.4f), 0.4f);

        yield return new WaitForSeconds(0.5f);

        GameObject.Find(32.ToString()).GetComponent<SpriteRenderer>().sprite = openDoor;

        vc.audioSource.clip = vc.audioClips[2];
        vc.audioSource.Play();

        Destroy(KeyAnimObj);

    }
}