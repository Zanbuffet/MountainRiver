using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioClip : MonoBehaviour
{
    public AudioClip[] auidiosss;

    // Start is called before the first frame update
    void Start()
    {
        var audioManager = GameObject.Find("AudioManager");
        //audioManager.GetComponent<AudioSource>().clip = audioManager.GetComponent<AudioManager>().audios[1];
        if(audioManager.GetComponent<AudioSource>().clip != audioManager.GetComponent<AudioManager>().audios[1] || audioManager.GetComponent<AudioSource>().clip == null){
        audioManager.GetComponent<AudioSource>().clip = Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "局内音乐终版")));
        audioManager.GetComponent<AudioSource>().Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSFX(){
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "UI按钮"))), transform.localPosition);
    }
}
