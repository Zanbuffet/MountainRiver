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
        SceneManager.LoadScene(1);  //temp
    }

    public void ShowCredits()
    {
        //show credits panel
        creditsPanel.SetActive(true);
        
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void ToggleAudio()
    {
        if (AudioManager.Instance!=null)
        {
            if(AudioManager.Instance.isOn)
            {
                AudioManager.Instance.ToggleAudio(false);
            }
            else
            {
                AudioManager.Instance.ToggleAudio(true);

            }
            UpdateAudioIcon();
        }

    }

    public void UpdateAudioIcon()
    {
        if(AudioManager.Instance!=null)
        {
            audioButton.image.sprite = AudioManager.Instance.isOn? audioIconSprites[0]: audioIconSprites[1];
        }
    }
}
