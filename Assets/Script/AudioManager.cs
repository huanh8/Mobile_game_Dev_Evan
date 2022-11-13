using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource audioSource;

    [Header("Music Audio Clips")]
    public AudioClip menuClip;
    public AudioClip exploreClip;
    public AudioClip battle1Clip;
    public AudioClip battle2Clip;
    public AudioClip battle3Clip;
    public AudioClip SadClip;
    public AudioClip VictoryClip;

    [SerializeField] private AudioSource musicSource;

    [Header("SFX Audio Clips")]
    public AudioClip FireBallClip;
    public AudioClip HurtClip;
    public AudioClip HealClip;
    public AudioClip SwordClip;
    public AudioClip Sword2Clip;
    public AudioClip Sword3Clip;
    public AudioClip JumpClip;
    public AudioClip DeadClip;
    public AudioClip DashClip;
    public AudioClip HeavyAttackClip;
    public AudioClip BlockClip;
    public AudioClip WalkClip;


    void Awake()
    {
        //Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayMusic(menuClip);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayBattleMusic()
    {
        musicSource.clip = battle1Clip;
        musicSource.Play();
    }
}
