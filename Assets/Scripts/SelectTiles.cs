using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectTiles : MonoBehaviour
{
    // Start is called before the first frame update
    bool isKey = false;


    void Start()
    {

       

    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public void ifClick()
    {

        // string name = GetComponent<Button>().name;
        string name = EventSystem.current.currentSelectedGameObject.name;
        GameObject go = GameObject.Find("GameController");
        GridTiles gridTiles = go.GetComponent<GridTiles>();
        List<int> disableList = gridTiles.disableList;
        Debug.Log("Name.........."+ name);
        int key = PlayerPrefs.GetInt("Key");

        Debug.Log("Key........" + key);

        if (name.Equals("22") && isKey) {

            Application.LoadLevel(0);
            int level;
            if (PlayerPrefs.GetInt("Level").Equals(0))
            {
                level = 1;
            }
            else {
                level = PlayerPrefs.GetInt("Level") + 1;
            }
            

            PlayerPrefs.SetInt("Level", level);
        }

        if (!disableList.Contains(key) && name.Equals(key.ToString()))
        {
            GameObject.Find(22.ToString()).GetComponent<Image>().color = Color.green;
            Debug.Log("Key Success");
            isKey = true;
        }
        else
        {

            if (!disableList.Contains(int.Parse(name)))
            {
                int c1 = int.Parse(name) - 5;
                int c2 = int.Parse(name) - 1;
                int c3 = int.Parse(name) + 1;
                int c4 = int.Parse(name) + 5;

             
                GameObject.Find(name).GetComponent<Image>().color = Color.gray;

                if (c1.Equals(key) || c2.Equals(key) || c3.Equals(key) || c4.Equals(key))
                {

                    GameObject.Find(key.ToString()).GetComponent<Image>().color = Color.yellow;
                    disableList.Remove(key);

                }

                if (!c1.Equals(22) && c1 <= 24 && c1 >= 0 && disableList.Contains(c1) && !c1.Equals(key))
                {
                    GameObject.Find(c1.ToString()).GetComponent<Image>().color = Color.blue;
                    disableList.Remove(c1);
                }

                if (!c2.Equals(22) && c2 <= 24 && c2 >= 0 && disableList.Contains(c2) && !c2.Equals(key) && !name.Equals("5") && !name.Equals("10") && !name.Equals("15") && !name.Equals("20"))
                {
                    GameObject.Find(c2.ToString()).GetComponent<Image>().color = Color.blue;
                    disableList.Remove(c2);
                }

                if (!c3.Equals(22) && c3 <= 24 && c3 >= 0 && disableList.Contains(c3) && !c3.Equals(key) && !name.Equals("4") && !name.Equals("9") && !name.Equals("14") && !name.Equals("19"))
                {
                    GameObject.Find(c3.ToString()).GetComponent<Image>().color = Color.blue;
                    disableList.Remove(c3);
                }

                if (!c4.Equals(22) && c4 <= 24 && c4 >= 0 && disableList.Contains(c4) && !c4.Equals(key))
                {
                    GameObject.Find(c4.ToString()).GetComponent<Image>().color = Color.blue;
                    disableList.Remove(c4);
                }
            }
        }
    }
}
