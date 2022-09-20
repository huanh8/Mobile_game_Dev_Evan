using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManagerTech : MonoBehaviour
{

    public GameObject homeScreen;
    [Tooltip("The UI Panel holding the credits")]
    public GameObject creditsScreen;
    [Tooltip("The UI Panel holding the settings")]

    public GameObject loadGameScreen;
    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;

    [Header("COLORS - Tint")]
    public Image[] panelGraphics;
    public Image[] blurs;
    public Color tint;


    [Header("Loading Screen Elements")]
    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public string newSceneName;
    [Tooltip("The loading bar Slider UI element in the Loading Screen")]
    public Slider loadingBar;
    private string loadSceneName; // scene name is defined when the load game data is retrieved



    [Header("Starting Options Values")]
    public int speakersDefault = 0;
    public int subtitleLanguageDefault = 0;

    [Header("List Indexing")]
    int speakersIndex = 0;
    int subtitleLanguageIndex = 0;

    [Header("Debug")]
    [Tooltip("If this is true, pressing 'R' will reload the scene.")]
    public bool reloadSceneButton = true;
    Transform tempParent;

    public void MoveToFront(GameObject currentObj)
    {
        //tempParent = currentObj.transform.parent;
        tempParent = currentObj.transform;
        tempParent.SetAsLastSibling();
    }

    void Start()
    {
        // By default, starts on the home screen, disables others
        homeScreen.SetActive(true);


        if (creditsScreen != null)
            creditsScreen.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
        if (loadGameScreen != null)
            loadGameScreen.SetActive(false);


        // Set Colors if the user didn't before play
        for (int i = 0; i < panelGraphics.Length; i++)
        {
            panelGraphics[i].color = tint;
        }
        for (int i = 0; i < blurs.Length; i++)
        {
            blurs[i].material.SetColor("_Color", tint);
        }


        // Check if first time so the volume can be set to MAX
        if (PlayerPrefs.GetInt("firsttime") == 0)
        {
            // it's the player's first time. Set to false now...
            PlayerPrefs.SetInt("firsttime", 1);
            PlayerPrefs.SetFloat("volume", 1);
        }


        // Settings screen
        speakersIndex = speakersDefault;
        subtitleLanguageIndex = subtitleLanguageDefault;

    }



    public void SetTint()
    {
        for (int i = 0; i < panelGraphics.Length; i++)
        {
            panelGraphics[i].color = tint;
        }
        for (int i = 0; i < blurs.Length; i++)
        {
            blurs[i].material.SetColor("_Color", tint);
        }
    }

    // Just for reloading the scene! You can delete this function entirely if you want to
    void Update()
    {
        if (reloadSceneButton)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                SceneManager.LoadScene("TestScene");
            }
        }

        SetTint();
    }




    // Converts the resolution into a string form that is then used in the dropdown list as the options
    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }


    // When accepting the QUIT question, the application will close 
    // (Only works in Executable. Disabled in Editor)
    public void Quit()
    {
        Application.Quit();
    }

    // Called when loading new game scene
    public void LoadNewLevel()
    {
        if (newSceneName != "")
        {
            StartCoroutine(LoadAsynchronously(newSceneName));
        }
    }

    // Called when loading saved scene
    // Add the save code in this function!
    public void LoadSavedLevel()
    {
        if (loadSceneName != "")
        {
            StartCoroutine(LoadAsynchronously(newSceneName)); // temporarily uses New Scene Name. Change this to 'loadSceneName' when you program the save data
        }
    }

    // Load Bar synching animation
    IEnumerator LoadAsynchronously(string sceneName)
    { // scene name is just the name of the current scene being loaded
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            yield return null;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
