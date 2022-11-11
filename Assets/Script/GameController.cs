using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    public GameObject gameOverMenu;
    public GameObject victoryMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Update()
    {
        //tab to reload scene
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReloadScene();
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);
        AudioManager.instance.PlaySound(AudioManager.instance.SadClip);
    }
    public void GameWin()
    {
        if (victoryMenu != null)
            victoryMenu.SetActive(true);
        AudioManager.instance.PlaySound(AudioManager.instance.VictoryClip);
    }
}
