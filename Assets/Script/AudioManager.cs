using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public bool isOn = true;

    //private AudioSource _audioSource;
    [SerializeField] private AudioMixer _mixer;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            // _audioSource = GetComponent<AudioSource>();

        }
    }

    public void ToggleAudio(bool newState)
    {
        isOn = newState;
        _mixer.SetFloat("Volume", isOn ? 0 : -80);
    }
}
