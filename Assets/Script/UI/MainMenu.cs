using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private Button audioButton;

    [SerializeField] private Sprite[] audioIconSprites;

    private void Awake()
    {
        UpdateAudioIcon();
    }
    public void OnStartGame()
    {
        //load level selection scene
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "UI按钮"))), transform.localPosition);
        SceneManager.LoadScene(1);  //temp
    }

    public void ShowCredits()
    {
        //show credits panel
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "UI按钮"))), transform.localPosition);
        creditsPanel.SetActive(true);
        
    }

    public void CloseCredits()
    {
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "UI按钮"))), transform.localPosition);
        creditsPanel.SetActive(false);
    }

    public void ToggleAudio()
    {
        if (AudioManager.Instance!=null)
        {
            if(AudioManager.Instance.GetComponent<AudioSource>().mute == true)
            {
                AudioManager.Instance.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                AudioManager.Instance.GetComponent<AudioSource>().mute = true;

            }
            UpdateAudioIcon();
        }

    }

    public void UpdateAudioIcon()
    {
        if(AudioManager.Instance!=null)
        {
            audioButton.image.sprite = AudioManager.Instance.GetComponent<AudioSource>().mute? audioIconSprites[1]: audioIconSprites[0];
        }
    }
    public void ExitGame()
    {
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "UI按钮"))), transform.localPosition);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif

    }
}
