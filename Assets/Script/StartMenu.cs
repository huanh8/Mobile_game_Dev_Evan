
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject playerObject;
    void OnDisable()
    {
        if (playerObject != null)
            playerObject.SetActive(true);
    }
    void OnEnable()
    {
        if (playerObject != null)
            playerObject.SetActive(false);
    }
}