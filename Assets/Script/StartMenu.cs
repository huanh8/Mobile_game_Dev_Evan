
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject storyManager;
    public GameObject HealthBar;
    void OnDisable()
    {
        if (playerObject != null)
            playerObject.SetActive(true);
        if (storyManager != null)
            storyManager.SetActive(true);
        if (HealthBar != null)
            HealthBar.SetActive(true);
    }
    void OnEnable()
    {
        if (playerObject != null)
            playerObject.SetActive(false);
        if (storyManager != null)
            storyManager.SetActive(false);
        if (HealthBar != null)
            HealthBar.SetActive(false);
    }
}