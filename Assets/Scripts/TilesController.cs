using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilesController : MonoBehaviour
{


    private int rows = 7;
    private int cols = 5;
    private float tileSize = 1.15f;
    int tileName = 0;
    public Text levelText;
    public List<int> disableList;
   
    List<int> randomNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,22, 23, 24,25,26,27,28,29,30,31,33,34};

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();

    }

    private void GenerateGrid()
    {

        if (PlayerPrefs.GetInt("Level").Equals(0))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        levelText.text = "Level " + PlayerPrefs.GetInt("Level").ToString();

        disableList = new List<int>();


        int c1 = 32 - 5;
        int c2 = 32 - 1;
        int c3 = 32 + 1;
        int c4 = 32 + 5;

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
        int coin = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(coin))
        {
            randomNumber.Remove(coin);
        }
        int civilian1 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];

        if (randomNumber.Contains(civilian1))
        {
            randomNumber.Remove(civilian1);
        }
        int civilian2 = randomNumber[UnityEngine.Random.Range(0, randomNumber.Count)];


        PlayerPrefs.SetInt("Key", key);
        PlayerPrefs.SetInt("Monster1", monster1);
        PlayerPrefs.SetInt("Monster2", monster2);
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("Civilian1", civilian1);
        PlayerPrefs.SetInt("Civilian2", civilian2);

        Debug.Log("Key............" + key + ".....monster1........" + monster1 + "...........monster2..........." + monster2+".........Coin.........."+coin+"........Civilian 1 and 2...."+civilian1+","+civilian2);



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
                    }

                    disableList.Remove(c1);
                    disableList.Remove(c2);
                    disableList.Remove(c3);
                    disableList.Remove(c4);
                }

                tile.transform.position = new Vector2(posX,posY);
            
            }
        }
        Destroy(referenceFile);


        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

       // transform.position = new Vector2(-gridW/2+tileSize/2,gridH/2-tileSize/2);
    }


    public void Reload() {

        Application.LoadLevel(0);
        PlayerPrefs.SetInt("Level", 1);
    }


     void Update()
    {
      

    }

}
