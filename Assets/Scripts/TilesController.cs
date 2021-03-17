using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilesController : MonoBehaviour
{
    private int rows = 7;
    private int cols = 5;
    private float tileSize = 1.12f;
    int tileName = 0;

    public Text levelText;
    public List<int> disableList;
    public List<int> firstDisbleList;

     public string name;
    public bool isPopup = false;
    public List<int> unBlockList1;
    public List<int> unBlockList2;
    List<int> randomNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,22, 23, 24,25,26,27,28,29,30,31,33,34};

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }
    
    public void GenerateGrid()
    {
        if (PlayerPrefs.GetInt("Level").Equals(0))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        levelText.text = "Level " + PlayerPrefs.GetInt("Level").ToString();

        disableList = new List<int>();
        unBlockList1 = new List<int>();
        unBlockList2 = new List<int>();

        int c1 = 32 - 5;
        int c2 = 32 - 1;
        int c3 = 32 + 1;
        int c4 = 32 + 5;

        if (!firstDisbleList.Contains(c1) && c1 <= 34 && c1 >= 0 && c1 != 32)
            firstDisbleList.Add(c1);
        if (!firstDisbleList.Contains(c2) && c2 <= 34 && c2 >= 0 && c2 != 32)
            firstDisbleList.Add(c2);
        if (!firstDisbleList.Contains(c3) && c3 <= 34 && c3 >= 0 && c3 != 32)
            firstDisbleList.Add(c3);
        if (!firstDisbleList.Contains(c4) && c4 <= 34 && c4 >= 0 && c4 != 32)
            firstDisbleList.Add(c4);


        randomNumber.Remove(c1);
        randomNumber.Remove(c2);
        randomNumber.Remove(c3);
        randomNumber.Remove(c4);

        int index = UnityEngine.Random.Range(0, randomNumber.Count);
        int key = randomNumber[index];

        if (randomNumber.Contains(key)) {
            randomNumber.Remove(key);
        }

        int monster1 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(monster1))
        {
            randomNumber.Remove(monster1);
        }

        int monster2 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(monster2))
        {
            randomNumber.Remove(monster2);
        }
        int coin1 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(coin1))
        {
            randomNumber.Remove(coin1);
        }
        int civilian1 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(civilian1))
        {
            randomNumber.Remove(civilian1);
        }
        int civilian2 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];
        if (randomNumber.Contains(civilian2))
        {
            randomNumber.Remove(civilian2);
        }
        int medicine = 1000;
        if (PlayerPrefs.GetInt("Level")%4==0) {

             medicine = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];
        }
        if (randomNumber.Contains(medicine))
        {
            randomNumber.Remove(medicine);
        }
        int coin2 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(coin2))
        {
            randomNumber.Remove(coin2);
        }
        int coin3 = 1001;
        if (PlayerPrefs.GetInt("Level") % 3 == 0)
        {
            coin3 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];
        }


        PlayerPrefs.SetInt("MaskCivilian", UnityEngine.Random.Range(0,7));

        PlayerPrefs.SetInt("UnMaskCivilian", UnityEngine.Random.Range(0, 7));

        Debug.Log("MaskCivilianValue.............."+PlayerPrefs.GetInt("MaskCivilian")+ "UnMaskCivilianValue.............." + PlayerPrefs.GetInt("UnMaskCivilian"));

        PlayerPrefs.SetInt("Key", key);


        PlayerPrefs.SetInt("Monster1", monster1);
        PlayerPrefs.SetInt("Monster2", monster2);



        PlayerPrefs.SetInt("Coin", coin1);

        PlayerPrefs.SetInt("Coin2", coin2);
        PlayerPrefs.SetInt("Coin3", coin3);

        PlayerPrefs.SetInt("Civilian1", civilian1);
        PlayerPrefs.SetInt("Civilian2", civilian2);
        PlayerPrefs.SetInt("SpawnMedicine", medicine);

        Debug.Log("Key............" + key + ".....monster1........" + monster1 + "...........monster2..........." + monster2+".........Coin.........."+coin1+"........Civilian 1 and 2...."+civilian1+","+civilian2);
        Debug.Log("medicine............."+medicine);


        Debug.Log(randomNumber.Count);
        PlayerPrefs.SetInt("Key", key);

        GameObject referenceFile = (GameObject)Instantiate(Resources.Load("1"));
     

        Sprite greenSprite = Resources.Load("locked", typeof(Sprite)) as Sprite;
        Sprite doorSprite = Resources.Load("door", typeof(Sprite)) as Sprite;
        Sprite keySprite = Resources.Load("key", typeof(Sprite)) as Sprite;
        Sprite stone = Resources.Load("block", typeof(Sprite)) as Sprite;

        


        for (int row =0; row<rows;row++)
        {
            for (int col=0;col<cols;col++) {
                GameObject tile = (GameObject)Instantiate(referenceFile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;
                
                tile.name = tileName .ToString();
               
                    disableList.Add(tileName);
                

                tileName = tileName + 1;

                if (tile.name.Equals("32"))
                {
                    tile.GetComponent<SpriteRenderer>().sprite = doorSprite;
                    disableList.Remove(32);
                }
                //tile.GetComponent<Animator>().enabled = false;

                if ((tile.name.Equals(c1.ToString()) && c1 <= 34 && c1 >= 0) || (tile.name.Equals(c2.ToString()) && c2 <= 34 && c2 >= 0) || (tile.name.Equals(c3.ToString()) && c3 <= 34 && c3 >= 0) || (tile.name.Equals(c4.ToString()) && c4 <= 34 && c4 >= 0))
                {

                    if (tile.name.Equals(key.ToString()))
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = keySprite;
                        disableList.Remove(key);
                    } else if (tile.name.Equals(monster1.ToString()) || tile.name.Equals(monster2.ToString())) {

                        tile.GetComponent<SpriteRenderer>().sprite = stone;
                    }
                    else
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = greenSprite;
                        tile.GetComponent<VisualController>().isUnlocked = true;
                    }

                    disableList.Remove(c1);
                    disableList.Remove(c2);
                    disableList.Remove(c3);
                    disableList.Remove(c4);
                }

                tile.transform.position = new Vector2(posX,posY);
                tile.GetComponent<VisualController>().delay = 0.5f + (tileName * .075f);
                tile.GetComponent<VisualController>().Show();
            }
        }
        Destroy(referenceFile);


      

       // transform.position = new Vector2(-gridW/2+tileSize/2,gridH/2-tileSize/2);

    }




    public void Reload() {


        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("Infection", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("CIVILIAN_COUNT", 0);
        PlayerPrefs.SetInt("MEDICINE", 2);
        PlayerPrefs.SetInt("DEFENCE", 5);
        PlayerPrefs.SetInt("ATTACK", 5);
        PlayerPrefs.SetString("MASK_TYPE", "My first Mask");
        SceneManager.LoadScene(1);
    }


    
}
