using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public int tileCount;
    public GameObject prefab;
    public Transform panel;
    public List<int> disableList;
    public Text levelText;
    int[] randomNumber = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24 };

    void Start()
    {
        Level1();
    }
    public void Level1()
    {
        levelText.text = "Level " + PlayerPrefs.GetInt("Level").ToString();
        int index = Random.Range(0, randomNumber.Length);
        int key = randomNumber[index];
        PlayerPrefs.SetInt("Key", key);
        int c1 = 22 - 5;
        int c2 = 22 - 1;
        int c3 = 22 + 1;
        int c4 = 22 + 5;
        disableList = new List<int>();
        for (int i = 0; i < tileCount; i++)
        {
            GameObject gameObj = Instantiate(prefab);
            gameObj.name = "" + i;
            disableList.Add(i);
            gameObj.transform.SetParent(panel, false);
            if (gameObj.name.Equals("22"))
                gameObj.GetComponent<Image>().color = Color.red;

            if ((gameObj.name.Equals(c1.ToString()) && c1 <= 24 && c1 >= 0) || (gameObj.name.Equals(c2.ToString()) && c2 <= 24 && c2 >= 0) || (gameObj.name.Equals(c3.ToString()) && c3 <= 24 && c3 >= 0) || (gameObj.name.Equals(c4.ToString()) && c4 <= 24 && c4 >= 0))
            {

                if (gameObj.name.Equals(key.ToString()))
                {
                    gameObj.GetComponent<Image>().color = Color.yellow;
                    disableList.Remove(key);
                }
                else
                    gameObj.GetComponent<Image>().color = Color.blue;
                disableList.Remove(c1);
                disableList.Remove(c2);
                disableList.Remove(c3);
                disableList.Remove(c4);
            }
        }
    }
}
