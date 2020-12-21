using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    [SerializeField] VisualController startBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.OnPressed += _ => LoadGame();
    }

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
