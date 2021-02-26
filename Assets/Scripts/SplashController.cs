using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    [SerializeField] VisualController continueBtn, newGameBtn;
    [SerializeField] private GameObject contineButton;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("CoinCount") == 0 && PlayerPrefs.GetInt("Infection")==0)
        {
            contineButton.SetActive(false);

        }
        else {

            contineButton.SetActive(true);
        }
        continueBtn.OnPressed += _ => LoadContinueGame();
        newGameBtn.OnPressed += _ => LoadNewGame();

    }

    void LoadContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    void LoadNewGame()
    {

        PlayerPrefs.SetInt("CoinCount",0);
        PlayerPrefs.SetInt("Infection", 0);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("CIVILIAN_COUNT", 0);
        PlayerPrefs.SetInt("MEDICINE", 2);
        SceneManager.LoadScene(1);
    }
}
