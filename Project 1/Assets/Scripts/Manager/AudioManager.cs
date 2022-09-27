using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [SerializeField] public AudioSource _audioBackground;
    [SerializeField] public AudioSource _audioInteract;
    public void Start()
    {
        _audioBackground = GetComponent<AudioSource>();
        _audioInteract = GetComponent<AudioSource>();
    }

    public void AudioBackground(AudioClip audio)
    {
        if (_audioBackground.isPlaying)
        {
            _audioBackground.Stop();
        }
        _audioBackground.clip = audio;
        _audioBackground.Play();
    }
    public void AudioInteract(AudioClip audio)
    {
        _audioInteract.PlayOneShot(audio);
    }
}