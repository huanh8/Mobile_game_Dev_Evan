
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject storyManager;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;

    void OnDisable()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayMusic(AudioManager.instance.exploreClip);
        if (storyManager != null)
            storyManager.SetActive(true);
        if (gameOverMenu != null)
            gameOverMenu.SetActive(false);
        if (victoryMenu != null)
            victoryMenu.SetActive(false);
    }
    void OnEnable()
    {
        if (storyManager != null)
            storyManager.SetActive(false);
        if (gameOverMenu != null)
            gameOverMenu.SetActive(false);
        if (victoryMenu != null)
            victoryMenu.SetActive(false);
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}