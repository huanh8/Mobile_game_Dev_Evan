using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBox : MonoBehaviour
{
    //get textmeshpro text
    public TMPro.TextMeshProUGUI text;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void SetText(string text)
    {
        this.text.text = text;
    }
    public void PlayAudio()
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void DisableTheBox()
    {
        gameObject.SetActive(false);
    }
}
