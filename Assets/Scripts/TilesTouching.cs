using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TilesTouching : MonoBehaviour
{
    bool isKey;
    int level=0;


    public void OnMouseDown()
    {
        Sprite greenSprite = Resources.Load("locked", typeof(Sprite)) as Sprite;
        Sprite sand = Resources.Load("opened", typeof(Sprite)) as Sprite;
        Sprite keySprite = Resources.Load("key", typeof(Sprite)) as Sprite;
        Sprite stone = Resources.Load("block", typeof(Sprite)) as Sprite;

        Sprite coinSprite = Resources.Load("coin", typeof(Sprite)) as Sprite;
        Sprite openDoor = Resources.Load("doorOpened", typeof(Sprite)) as Sprite; 
        Sprite civilianSprite = Resources.Load("civilian", typeof(Sprite)) as Sprite;

        string name = gameObject.name;

        GameObject go = GameObject.Find("TilesController");
        TilesController tilesController = go.GetComponent<TilesController>();
        List<int> disableList = tilesController.disableList;
        Debug.Log("Name.........." + name);

        int key = PlayerPrefs.GetInt("Key");
        int Monster1 = PlayerPrefs.GetInt("Monster1");
        int Monster2 = PlayerPrefs.GetInt("Monster2");

        int coin = PlayerPrefs.GetInt("Coin");
        int civilian1 = PlayerPrefs.GetInt("Civilian1");
        int civilian2 = PlayerPrefs.GetInt("Civilian2");


        Debug.Log("Key............" + key + ".....monster1........" + Monster1 + "...........monster2..........." + Monster2);

        if (name.Equals("32") && disableList.Contains(32))
        {
            Debug.Log("Door Pressed");
            disableList.Remove(32);
            Application.LoadLevel(0);
         
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
        Debug.Log(isKey);
        if (!disableList.Contains(key) && name.Equals(key.ToString()))
        {
            isKey = true;
            disableList.Add(32);
            GameObject.Find(32.ToString()).GetComponent<SpriteRenderer>().sprite = openDoor;
            Debug.Log("Key Success");
           
            
        } else if (name.Equals(Monster1.ToString()) || (name.Equals(Monster2.ToString())) || name.Equals("32")) {

            Debug.Log("Monster Pressed");
        }
        else
        {

            if (!disableList.Contains(int.Parse(name)))
            {
                int c1 = int.Parse(name) - 5;
                int c2 = int.Parse(name) - 1;
                int c3 = int.Parse(name) + 1;
                int c4 = int.Parse(name) + 5;


                GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = sand;


                if (disableList.Contains(coin) && (c1.Equals(coin) || (c2.Equals(coin) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(coin) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(coin)))
                {

                    GameObject.Find(coin.ToString()).GetComponent<SpriteRenderer>().sprite = coinSprite;
                    disableList.Remove(coin);

                }
                if (disableList.Contains(civilian1) &&  (c1.Equals(civilian1) || (c2.Equals(civilian1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian1)))
                {

                    GameObject.Find(civilian1.ToString()).GetComponent<SpriteRenderer>().sprite = civilianSprite;
                    disableList.Remove(civilian1);

                }

                if (disableList.Contains(civilian2) &&  (c1.Equals(civilian2) || (c2.Equals(civilian2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(civilian2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(civilian2)))
                {

                    GameObject.Find(civilian2.ToString()).GetComponent<SpriteRenderer>().sprite = civilianSprite;
                    disableList.Remove(civilian2);

                }


                if (c1.Equals(key) || (c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(key))
                {

                    GameObject.Find(key.ToString()).GetComponent<SpriteRenderer>().sprite = keySprite;
                    disableList.Remove(key);

                }
                if (c1.Equals(Monster1) || (c2.Equals(Monster1) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster1) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster1))
                {

                    GameObject.Find(Monster1.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
                    disableList.Remove(Monster1);

                }

                if (c1.Equals(Monster2) || (c2.Equals(Monster2) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30")) || (c3.Equals(Monster2) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29")) || c4.Equals(Monster2))
                {

                    GameObject.Find(Monster2.ToString()).GetComponent<SpriteRenderer>().sprite = stone;
                    disableList.Remove(Monster2);

                }



                if (!c1.Equals(32) && c1 <= 34 && c1 >= 0 && disableList.Contains(c1) && !c1.Equals(key) && !c1.Equals(Monster1) && !c1.Equals(Monster2))
                {
                    GameObject.Find(c1.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    disableList.Remove(c1);
                }

                if (!c2.Equals(32) && c2 <= 34 && c2 >= 0 && disableList.Contains(c2) && !c2.Equals(key)  && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20") && !name.Equals("25") && !name.Equals("30") && !c2.Equals(Monster1) && !c2.Equals(Monster2))
                {
                    GameObject.Find(c2.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    disableList.Remove(c2);
                }

                if (!c3.Equals(32) && c3 <= 34 && c3 >= 0 && disableList.Contains(c3) && !c3.Equals(key)  && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19") && !name.Equals("24") && !name.Equals("29") && !c3.Equals(Monster1) && !c3.Equals(Monster2))
                {
                    GameObject.Find(c3.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    disableList.Remove(c3);
                }

                if (!c4.Equals(32) && c4 <= 34 && c4 >= 0 && disableList.Contains(c4) && !c4.Equals(key) && !c4.Equals(Monster1) && !c4.Equals(Monster2))
                {
                    GameObject.Find(c4.ToString()).GetComponent<SpriteRenderer>().sprite = greenSprite;
                    disableList.Remove(c4);
                }
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        isKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
