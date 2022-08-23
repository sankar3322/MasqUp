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
    VisualController vc;
    public GameObject popUp;
    GameObject doorPos, centerPos, keyPos, KeyAnimObj, CoinAnimObj, bottomCoinPos, coinPos, MedsAnimObj, medsPos;
    List<int> disableList, firstDisbleList;
    public List<int> unBlockList1;
    public List<int> unBlockList2;
    int civilian1, civilian2, key, coin, coin2, coin3;
    int Monster1, Monster2, medsPosition;
    ParticleSystem particleSystem;
    int MonsterHitValue;
    HealthSystem healthSystem;
    Sprite greenSprite, sand, keySprite, stone, coinSprite, civilianSprite, civilianMaskedSprite, virus_block, medSprite;
    public Sprite[] maskCivilianSprites;
    public Sprite[] unMaskCivilianSprites;
    public Sprite[] MonsterSprites;
    // Start is called before the first frame update
    void Start()
    {
        isKey = false;
        //healthSystem.coinTextChange();
        MonsterHitValue = 10;
    }
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        vc = GetComponent<VisualController>();
        vc.OnPressed += _ => OnClicked();
        healthSystem = GameObject.Find("HealthManager").GetComponent<HealthSystem>();
        healthSystem.initHealth();
    }

    void OnClicked()
    {
        greenSprite = Resources.Load("locked", typeof(Sprite)) as Sprite;
        sand = Resources.Load("opened", typeof(Sprite)) as Sprite;
        keySprite = Resources.Load("key", typeof(Sprite)) as Sprite;
        stone = Resources.Load("virus", typeof(Sprite)) as Sprite;
        coinSprite = Resources.Load("coin", typeof(Sprite)) as Sprite;
        civilianSprite = Resources.Load("civilian", typeof(Sprite)) as Sprite;
        civilianMaskedSprite = Resources.Load("MaskedCivilian", typeof(Sprite)) as Sprite;
        virus_block = Resources.Load("Virus_Block", typeof(Sprite)) as Sprite;
        medSprite = Resources.Load("meds", typeof(Sprite)) as Sprite;
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
        medsPosition = PlayerPrefs.GetInt("SpawnMedicine");
        coin = PlayerPrefs.GetInt("Coin");
        coin2 = PlayerPrefs.GetInt("Coin2");
        coin3 = PlayerPrefs.GetInt("Coin3");
        civilian1 = PlayerPrefs.GetInt("Civilian1");
        civilian2 = PlayerPrefs.GetInt("Civilian2");
        if (name.Equals("32") && disableList.Contains(32))
        {
            vc.audioSource.clip = vc.audioClips[2];
            vc.audioSource.Play();
            disableList.Remove(32);
            healthSystem.nextLevel();
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
            PlayerPrefs.SetInt("Key", 1000);
            doorPos = GameObject.Find("32");
            centerPos = GameObject.Find("17");
            keyPos = GameObject.Find(key.ToString());
            KeyAnimObj = (GameObject)Instantiate(Resources.Load("KeyObj"));
            var KeyPanel = GameObject.Find("AnimKey");
            KeyAnimObj.transform.SetParent(KeyPanel.transform, false);
            KeyAnimObj.transform.position = new Vector3(keyPos.transform.position.x, keyPos.transform.position.y, -0.4f);
            firstDisbleList.Remove(key);
            StartCoroutine(keyAnimation());
        }
        else if (key == 1000 && isKey)
            Debug.Log("Key False");
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
                vc.audioSource.clip = vc.audioClips[4];
                vc.audioSource.Play();
                healthSystem.virusValidation();
                virusAnimation(name);
                // healthSystem.showVirusFirst();
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
                if (name.Equals(coin.ToString()) || name.Equals(coin2.ToString()) || name.Equals(coin3.ToString()))
                {
                    vc.audioSource.clip = vc.audioClips[1];
                    vc.audioSource.Play();
                    centerPos = GameObject.Find("17");
                    if (name.Equals(coin.ToString()))
                        coinPos = GameObject.Find(coin.ToString());
                    else if (name.Equals(coin2.ToString()))
                        coinPos = GameObject.Find(coin2.ToString());
                    else if (name.Equals(coin3.ToString()))
                        coinPos = GameObject.Find(coin3.ToString());

                    bottomCoinPos = GameObject.Find("34");
                    CoinAnimObj = (GameObject)Instantiate(Resources.Load("CoinObj"));
                    var KeyPanel = GameObject.Find("AnimKey");
                    CoinAnimObj.transform.SetParent(KeyPanel.transform, false);
                    CoinAnimObj.transform.position = new Vector3(coinPos.transform.position.x, coinPos.transform.position.y, -0.4f);
                    StartCoroutine(coinAnimation());
                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + 1);
                    Debug.Log("CoinCount............" + PlayerPrefs.GetInt("CoinCount"));
                }
                else if (name.Equals(medsPosition.ToString()))
                {
                    centerPos = GameObject.Find("17");
                    medsPos = GameObject.Find(medsPosition.ToString());
                    bottomCoinPos = GameObject.Find("34");
                    MedsAnimObj = (GameObject)Instantiate(Resources.Load("MedsObj"));
                    var KeyPanel = GameObject.Find("AnimKey");
                    MedsAnimObj.transform.SetParent(KeyPanel.transform, false);
                    MedsAnimObj.transform.position = new Vector3(medsPos.transform.position.x, medsPos.transform.position.y, -0.4f);
                    StartCoroutine(medsAnimation());
                }
                else if (name.Equals(civilian1.ToString()) || name.Equals(civilian2.ToString()))
                {
                    /*  vc.audioSource.clip = vc.audioClips[5];
                      vc.audioSource.Play();*/
                    GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
                    if (name.Equals(civilian1.ToString()))
                        healthSystem.showCivilianFirst("C1");
                    else
                        healthSystem.showCivilianFirst("C2");
                }
                else
                {
                    vc.audioSource.clip = vc.audioClips[6];
                    vc.audioSource.Play();
                }
                if (unBlockList1.Contains(int.Parse(name)) || unBlockList2.Contains(int.Parse(name)))
                {
                    vc.audioSource.clip = vc.audioClips[4];
                    vc.audioSource.Play();
                }
                GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = sand;
                if (disableList.Contains(Monster1) && (c1.Equals(Monster1) || (c2.Equals(Monster1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster1)))
                    monster1Validation();
                if (disableList.Contains(Monster2) && (c1.Equals(Monster2) || (c2.Equals(Monster2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster2)))
                    monster2Validation();
                if (!unBlockList1.Contains(coin) && !unBlockList2.Contains(coin) && disableList.Contains(coin) && (c1.Equals(coin) || (c2.Equals(coin) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(coin) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(coin)))
                {
                    GameObject.Find(coin.ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    GameObject.Find(coin.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    firstDisbleList.Add(coin);
                    disableList.Remove(coin);
                }
                if (!unBlockList1.Contains(coin2) && !unBlockList2.Contains(coin2) && disableList.Contains(coin2) && (c1.Equals(coin2) || (c2.Equals(coin2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(coin2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(coin2)))
                {
                    GameObject.Find(coin2.ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    GameObject.Find(coin2.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    firstDisbleList.Add(coin2);
                    disableList.Remove(coin2);
                }
                if (!unBlockList1.Contains(coin3) && !unBlockList2.Contains(coin3) && disableList.Contains(coin3) && (c1.Equals(coin3) || (c2.Equals(coin3) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(coin3) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(coin3)))
                {
                    GameObject.Find(coin3.ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    GameObject.Find(coin3.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    firstDisbleList.Add(coin3);
                    disableList.Remove(coin3);
                }
                if (!unBlockList1.Contains(medsPosition) && !unBlockList2.Contains(medsPosition) && disableList.Contains(medsPosition) && (c1.Equals(medsPosition) || (c2.Equals(medsPosition) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(medsPosition) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(medsPosition)))
                {
                    GameObject.Find(medsPosition.ToString()).GetComponent<SpriteRenderer>().sprite = medSprite;
                    GameObject.Find(medsPosition.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    firstDisbleList.Add(medsPosition);
                    disableList.Remove(medsPosition);
                }
                if (!unBlockList1.Contains(civilian1) && !unBlockList2.Contains(civilian1) && disableList.Contains(civilian1) && (c1.Equals(civilian1) || (c2.Equals(civilian1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian1)))
                {
                    healthSystem.helpAudio("C1");
                    textShowValidation("C1");
                    GameObject.Find(civilian1.ToString()).GetComponent<SpriteRenderer>().sprite = unMaskCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")];
                    GameObject.Find(civilian1.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(civilian1);
                    firstDisbleList.Add(civilian1);
                }
                if (!unBlockList1.Contains(civilian2) && !unBlockList2.Contains(civilian2) && disableList.Contains(civilian2) && (c1.Equals(civilian2) || (c2.Equals(civilian2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian2)))
                {
                    healthSystem.helpAudio("C2");
                    textShowValidation("C2");
                    GameObject.Find(civilian2.ToString()).GetComponent<SpriteRenderer>().sprite = maskCivilianSprites[PlayerPrefs.GetInt("MaskCivilian")];
                    GameObject.Find(civilian2.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(civilian2);
                    firstDisbleList.Add(civilian2);
                }
                if (!unBlockList1.Contains(key) && !unBlockList2.Contains(key) && (c1.Equals(key) || (c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(key)))
                {
                    GameObject.Find(key.ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                    GameObject.Find(key.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(key);
                    firstDisbleList.Add(key);
                }
                if (!c1.Equals(32) && c1 <= 34 && c1 >= 0 && disableList.Contains(c1) && !c1.Equals(key) && !c1.Equals(Monster1) && !c1.Equals(Monster2) && !unBlockList1.Contains(c1) && !unBlockList2.Contains(c1))
                {
                    GameObject.Find(c1.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c1.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c1);
                    firstDisbleList.Add(c1);
                }
                if (!c2.Equals(32) && c2 <= 34 && c2 >= 0 && disableList.Contains(c2) && !c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30") && !c2.Equals(Monster1) && !c2.Equals(Monster2) && !unBlockList1.Contains(c2) && !unBlockList2.Contains(c2))
                {
                    GameObject.Find(c2.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c2.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c2);
                    firstDisbleList.Add(c2);
                }
                if (!c3.Equals(32) && c3 <= 34 && c3 >= 0 && disableList.Contains(c3) && !c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29") && !c3.Equals(Monster1) && !c3.Equals(Monster2) && !unBlockList1.Contains(c3) && !unBlockList2.Contains(c3))
                {
                    GameObject.Find(c3.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c3.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c3);
                    firstDisbleList.Add(c3);
                }
                if (!c4.Equals(32) && c4 <= 34 && c4 >= 0 && disableList.Contains(c4) && !c4.Equals(key) && !c4.Equals(Monster1) && !c4.Equals(Monster2) && !unBlockList1.Contains(c4) && !unBlockList2.Contains(c4))
                {
                    GameObject.Find(c4.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    GameObject.Find(c4.ToString()).GetComponent<VisualController>().isUnlocked = true;
                    disableList.Remove(c4);
                    firstDisbleList.Add(c4);
                }
            }
        }
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

    IEnumerator coinAnimation()
    {
        CoinAnimObj.transform.DOScale(2f, 1);
        DOTween.Sequence()
           .Append(CoinAnimObj.transform.DOMove(new Vector3(centerPos.transform.position.x, centerPos.transform.position.y, -0.4f), 0.4f))
           .AppendInterval(0.5f).SetLoops(1);
        yield return new WaitForSeconds(0.4f);
        CoinAnimObj.transform.DOScale(0f, 1);
        CoinAnimObj.transform.DOMove(new Vector3(bottomCoinPos.transform.position.x, bottomCoinPos.transform.position.y - 2.0f, -0.4f), 0.4f);
        yield return new WaitForSeconds(0.5f);
        healthSystem.coinTextChange();
        Destroy(CoinAnimObj);
    }


    IEnumerator medsAnimation()
    {
        MedsAnimObj.transform.DOScale(4f, 1);
        DOTween.Sequence()
           .Append(MedsAnimObj.transform.DOMove(new Vector3(centerPos.transform.position.x, centerPos.transform.position.y, -0.4f), 0.4f))
           .AppendInterval(0.5f).SetLoops(1);
        yield return new WaitForSeconds(0.4f);
        MedsAnimObj.transform.DOScale(0f, 1);
        MedsAnimObj.transform.DOMove(new Vector3(bottomCoinPos.transform.position.x, bottomCoinPos.transform.position.y - 3.0f, -0.4f), 0.4f);
        yield return new WaitForSeconds(0.5f);
        healthSystem.medsTextChange();
        Destroy(MedsAnimObj);
    }

    public void virusAnimation(string name)
    {
        StartCoroutine(virusDelay(name));
    }

    IEnumerator virusDelay(string name)
    {
        GameObject VirusAnimObj = new GameObject();
        GameObject virusPos = GameObject.Find(name);
        GameObject target = GameObject.Find("30");
        float xpos = 0f, ypos = 0f;
        for (int i = 0; i <= 0; i++)
        {
            xpos += 0.05f;
            ypos += 0.5f;
            VirusAnimObj = (GameObject)Instantiate(Resources.Load("virusPrefab"));
            VirusAnimObj.name = "virusPrefab" + i.ToString();
            var KeyPanel = GameObject.Find("AnimKey");
            VirusAnimObj.transform.SetParent(KeyPanel.transform, false);
            VirusAnimObj.transform.position = new Vector3(virusPos.transform.position.x + xpos, virusPos.transform.position.y, -0.4f);
            string gameObjectname = "virusPrefab" + i;
            GameObject gameObject = GameObject.Find(gameObjectname);
            gameObject.transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y - 2.5f, -0.4f), 0.5f);
            GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        healthSystem.virusParticleEffect();
        yield return new WaitForSeconds(0.3f);
        GameObject.Find(name).GetComponent<VisualController>().isUnlocked = true;
        GameObject virAttackText = GameObject.Find(name.ToString()).transform.Find("VirusAttack").gameObject;
        GameObject virDefenceText = GameObject.Find(name.ToString()).transform.Find("VirusDefence").gameObject;
        GameObject topSprite = GameObject.Find(name.ToString()).transform.Find("TopSprite").gameObject;
        MonsterHitValue = MonsterHitValue - 5;
        virAttackText.GetComponent<TextMesh>().text = "5";
        virDefenceText.GetComponent<TextMesh>().text = MonsterHitValue.ToString();
        if (MonsterHitValue == 0)
        {
            MonsterHitValue = 10;
            GameObject monster1Pos = GameObject.Find(Monster1.ToString());
            GameObject monster2Pos = GameObject.Find(Monster2.ToString());
            virAttackText.SetActive(false);
            virDefenceText.SetActive(false);
            topSprite.SetActive(false);
            // GameObject.Find(name).GetComponent<Animator>().enabled = false;
            GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = sand;
            GameObject.Find(name).GetComponent<VisualController>().isUnlocked = false;
            if (name.Equals(Monster1.ToString()))
            {
                for (int i = 0; i < unBlockList1.Count; i++)
                {
                    if (unBlockList1[i] == Monster2)
                        monster2Validation();
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == key)
                    {
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == civilian1)
                    {
                        healthSystem.helpAudio("C1");
                        textShowValidation("C1");
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = unMaskCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")];
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == civilian2)
                    {
                        healthSystem.helpAudio("C2");
                        textShowValidation("C2");
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = maskCivilianSprites[PlayerPrefs.GetInt("MaskCivilian")];
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == coin)
                    {
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == coin2)
                    {
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == coin3)
                    {
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList2.Contains(unBlockList1[i]) && unBlockList1[i] == medsPosition)
                    {
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = medSprite;
                    }
                    else
                    {
                        if (!unBlockList2.Contains(unBlockList1[i]))
                        {
                            GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                            GameObject.Find(unBlockList1[i].ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                        }
                        else
                            GameObject.Find(unBlockList1[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                    }
                    Debug.Log("monster1 Block" + unBlockList1[i]);
                }
                GameObject VirusDefectEffect1 = (GameObject)Instantiate(Resources.Load("Virus Effect"));
                VirusDefectEffect1.transform.position = new Vector3(monster1Pos.transform.position.x + xpos, monster1Pos.transform.position.y, -0.4f);
                VirusDefectEffect1.GetComponent<ParticleSystem>().Play();
                unBlockList1.Clear();
                yield return new WaitForSeconds(0.5f);
                Destroy(VirusDefectEffect1);
            }
            if (name.Equals(Monster2.ToString()))
            {
                for (int i = 0; i < unBlockList2.Count; i++)
                {
                    if (unBlockList2[i] == Monster1)
                        monster1Validation();
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == key)
                    {
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == civilian1)
                    {
                        healthSystem.helpAudio("C1");
                        textShowValidation("C1");
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = unMaskCivilianSprites[PlayerPrefs.GetInt("UnMaskCivilian")];
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == civilian2)
                    {
                        healthSystem.helpAudio("C2");
                        textShowValidation("C2");
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = maskCivilianSprites[PlayerPrefs.GetInt("MaskCivilian")];
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == coin)
                    {
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == coin2)
                    {
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == coin3)
                    {
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    }
                    else if (!unBlockList1.Contains(unBlockList2[i]) && unBlockList2[i] == medsPosition)
                    {
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                        GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = medSprite;
                    }
                    else
                    {
                        if (!unBlockList1.Contains(unBlockList2[i]))
                        {
                            GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = true;
                            GameObject.Find(unBlockList2[i].ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                        }
                        else
                            GameObject.Find(unBlockList2[i].ToString()).GetComponent<VisualController>().isUnlocked = false;

                    }
                    Debug.Log("monster2 Block" + unBlockList2[i]);
                }
                GameObject VirusDefectEffect2 = (GameObject)Instantiate(Resources.Load("Virus Effect"));
                VirusDefectEffect2.transform.position = new Vector3(monster2Pos.transform.position.x + xpos, monster2Pos.transform.position.y, -0.4f);
                VirusDefectEffect2.GetComponent<ParticleSystem>().Play();
                unBlockList2.Clear();
                yield return new WaitForSeconds(0.5f);
                Destroy(VirusDefectEffect2);
            }
            Debug.Log("Monster Pressed" + unBlockList1.Count);
        }
        Debug.Log("TargetPos........" + target.transform.position);
    }

    public void monster2Validation()
    {
        unBlockList2.Clear();
        GameObject.Find(Monster2.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
        GameObject.Find(Monster2.ToString()).GetComponent<VisualController>().isUnlocked = true;
        // GameObject.Find(Monster2.ToString()).GetComponent<Animator>().enabled = true;
        GameObject virAttackText = GameObject.Find(Monster2.ToString()).transform.Find("VirusAttack").gameObject;
        GameObject virDefenceText = GameObject.Find(Monster2.ToString()).transform.Find("VirusDefence").gameObject;
        GameObject topSprite = GameObject.Find(Monster2.ToString()).transform.Find("TopSprite").gameObject;
        topSprite.GetComponent<SpriteRenderer>().sprite = MonsterSprites[PlayerPrefs.GetInt("RandomMonster2")];
        topSprite.SetActive(true);
        DOTween.Sequence()
             .Append(topSprite.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
             .AppendInterval(0.1f).SetLoops(-1);
        virAttackText.SetActive(true);
        virDefenceText.SetActive(true);
        virAttackText.GetComponent<TextMesh>().text = "5".ToString();
        virDefenceText.GetComponent<TextMesh>().text = MonsterHitValue.ToString();
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
            if (firstDisbleList.Contains(mList[i]) || disableList.Contains(mList[i]))
            {
                if (!mList[i].Equals(32))
                {
                    if (Monster2.ToString().Equals("0") || Monster2.ToString().Equals("5") || Monster2.ToString().Equals("10") || Monster2.ToString().Equals("15") || Monster2.ToString().Equals("20")
                        || Monster2.ToString().Equals("25") || Monster2.ToString().Equals("30"))
                    {
                        if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("4") && !mList[i].ToString().Equals("9") && !mList[i].ToString().Equals("14")
                        && !mList[i].ToString().Equals("19") && !mList[i].ToString().Equals("24") && !mList[i].ToString().Equals("29") && !mList[i].ToString().Equals("34"))
                        {
                            Debug.Log(mList[i]);
                            GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                            GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                            unBlockList2.Add(mList[i]);
                            if (disableList.Contains(mList[i]))
                                disableList.Remove(mList[i]);
                            if (!firstDisbleList.Contains(mList[i]))
                                firstDisbleList.Add(mList[i]);
                        }
                    }
                    else if (Monster2.ToString().Equals("4") || Monster2.ToString().Equals("9") || Monster2.ToString().Equals("14")
                      || Monster2.ToString().Equals("19") || Monster2.ToString().Equals("24") || Monster2.ToString().Equals("29") || Monster2.ToString().Equals("34"))
                    {
                        if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("0") && !mList[i].ToString().Equals("5") && !mList[i].ToString().Equals("10") && !mList[i].ToString().Equals("15") && !mList[i].ToString().Equals("20")
                        && !mList[i].ToString().Equals("25") && !mList[i].ToString().Equals("30"))
                        {
                            Debug.Log(mList[i]);
                            GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                            GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                            unBlockList2.Add(mList[i]);
                            if (disableList.Contains(mList[i]))
                                disableList.Remove(mList[i]);
                            if (!firstDisbleList.Contains(mList[i]))
                                firstDisbleList.Add(mList[i]);
                        }
                    }
                    else
                    {
                        Debug.Log(mList[i]);
                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                        unBlockList2.Add(mList[i]);
                        if (disableList.Contains(mList[i]))
                            disableList.Remove(mList[i]);
                        if (!firstDisbleList.Contains(mList[i]))
                            firstDisbleList.Add(mList[i]);
                    }
                }
            }
        }
        firstDisbleList.Remove(Monster2);
        disableList.Remove(Monster2);
        Debug.Log("BlockList2.........." + unBlockList2.Count);
    }

    public void monster1Validation()
    {
        unBlockList1.Clear();
        GameObject.Find(Monster1.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
        GameObject.Find(Monster1.ToString()).GetComponent<VisualController>().isUnlocked = true;
        //GameObject.Find(Monster1.ToString()).GetComponent<Animator>().enabled = true;
        GameObject virAttackText = GameObject.Find(Monster1.ToString()).transform.Find("VirusAttack").gameObject;
        GameObject virDefenceText = GameObject.Find(Monster1.ToString()).transform.Find("VirusDefence").gameObject;
        GameObject topSprite = GameObject.Find(Monster1.ToString()).transform.Find("TopSprite").gameObject;
        topSprite.SetActive(true);
        topSprite.GetComponent<SpriteRenderer>().sprite = MonsterSprites[PlayerPrefs.GetInt("RandomMonster1")];
        DOTween.Sequence()
            .Append(topSprite.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
            .AppendInterval(0.1f).SetLoops(-1);
        virAttackText.SetActive(true);
        virDefenceText.SetActive(true);
        virAttackText.GetComponent<TextMesh>().text = "5".ToString();
        virDefenceText.GetComponent<TextMesh>().text = MonsterHitValue.ToString();
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
            if (firstDisbleList.Contains(mList[i]) || disableList.Contains(mList[i]))
            {
                if (!mList[i].Equals(32))
                {
                    if (Monster1.ToString().Equals("0") || Monster1.ToString().Equals("5") || Monster1.ToString().Equals("10") || Monster1.ToString().Equals("15") || Monster1.ToString().Equals("20")
                        || Monster1.ToString().Equals("25") || Monster1.ToString().Equals("30"))
                    {
                        if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("4") && !mList[i].ToString().Equals("9") && !mList[i].ToString().Equals("14")
                        && !mList[i].ToString().Equals("19") && !mList[i].ToString().Equals("24") && !mList[i].ToString().Equals("29") && !mList[i].ToString().Equals("34"))
                        {
                            Debug.Log(mList[i]);
                            GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                            GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                            unBlockList1.Add(mList[i]);
                            if (disableList.Contains(mList[i]))
                                disableList.Remove(mList[i]);
                            if (!firstDisbleList.Contains(mList[i]))
                                firstDisbleList.Add(mList[i]);
                        }
                    }
                    else if (Monster1.ToString().Equals("4") || Monster1.ToString().Equals("9") || Monster1.ToString().Equals("14")
                      || Monster1.ToString().Equals("19") || Monster1.ToString().Equals("24") || Monster1.ToString().Equals("29") || Monster1.ToString().Equals("34"))
                    {
                        if (mList[i] <= 34 && mList[i] >= 0 && !mList[i].ToString().Equals("0") && !mList[i].ToString().Equals("5") && !mList[i].ToString().Equals("10") && !mList[i].ToString().Equals("15") && !mList[i].ToString().Equals("20")
                        && !mList[i].ToString().Equals("25") && !mList[i].ToString().Equals("30"))
                        {
                            Debug.Log(mList[i]);
                            GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                            GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                            unBlockList1.Add(mList[i]);
                            if (disableList.Contains(mList[i]))
                                disableList.Remove(mList[i]);
                            if (!firstDisbleList.Contains(mList[i]))
                                firstDisbleList.Add(mList[i]);
                        }
                    }
                    else
                    {
                        Debug.Log(mList[i]);
                        GameObject.Find(mList[i].ToString()).GetComponent<SpriteRenderer>().sprite = virus_block;
                        GameObject.Find(mList[i].ToString()).GetComponent<VisualController>().isUnlocked = false;
                        unBlockList1.Add(mList[i]);
                        if (disableList.Contains(mList[i]))
                            disableList.Remove(mList[i]);
                        if (!firstDisbleList.Contains(mList[i]))
                            firstDisbleList.Add(mList[i]);
                    }
                }
            }
        }
        Debug.Log("BlockList1.........." + unBlockList1.Count);
        firstDisbleList.Remove(Monster1);
        disableList.Remove(Monster1);
    }

    public void textShowValidation(string civilianType)
    {
        StartCoroutine(textValidation(civilianType));
    }

    IEnumerator textValidation(string civilianType)
    {
        GameObject helpObj;
        if (civilianType.Equals("C1"))
            helpObj = GameObject.Find(civilian1.ToString()).transform.Find("HelpSprite").gameObject;
        else
            helpObj = GameObject.Find(civilian2.ToString()).transform.Find("HelpSprite").gameObject;
        helpObj.SetActive(true);
        DOTween.Sequence()
              .Append(helpObj.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
              .AppendInterval(0.1f).SetLoops(-1);

        yield return new WaitForSeconds(1f);
        helpObj.SetActive(false);
    }

    IEnumerator delayPopup(string name)
    {
        var panel = GameObject.Find("Canvas_Popup");
        Sprite civilianPopup = Resources.Load("Saved Popup", typeof(Sprite)) as Sprite;
        Sprite virusPopup = Resources.Load("Virus Destroyed", typeof(Sprite)) as Sprite;
        GameObject a = (GameObject)Instantiate(popUp);
        a.name = "a";
        a.transform.SetParent(panel.transform, false);
        if (name.Equals(civilian1.ToString()) || name.Equals(civilian2.ToString()))
            a.GetComponent<Image>().sprite = civilianPopup;
        else
            a.GetComponent<Image>().sprite = virusPopup;
        GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = false;
        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != civilian1 && i != civilian2 && i != Monster1 && i != Monster2)
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = false;
            }
        }
        DOTween.Sequence()
            .Append(a.transform.DOPunchScale(Vector3.one * 0.03f * 3, 1f, vibrato: 2, elasticity: 3f))
            .AppendInterval(0.1f).SetLoops(-1);
        yield return new WaitForSeconds(1.5f);
        if (key == 1000)
            GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = true;
        else
            GameObject.Find(32.ToString()).GetComponent<VisualController>().isUnlocked = false;
        for (int i = 0; i <= 34; i++)
        {
            if (firstDisbleList.Contains(i))
            {
                if (i != civilian1 && i != civilian2 && i != Monster1 && i != Monster2)
                    GameObject.Find(i.ToString()).GetComponent<VisualController>().isUnlocked = true;
            }
        }
        Destroy(a);
    }
}