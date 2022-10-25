using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public AudioClip[] audios;

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

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("UI"));
            {
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "胜利"))), transform.localPosition);
            }

        }
    }



}
