using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndPanel : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Toggle goal1;
    [SerializeField] private Toggle goal2;
    [SerializeField] private Toggle goal3;

    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Button pause;

    [SerializeField] private Image result;
    [SerializeField] private Sprite[] resultSprites;

    [SerializeField] private Sprite[] starSprites;
    [SerializeField] private GameObject endCover;
    [SerializeField] private GameObject endLongCover;
    [SerializeField] private GameObject CG;
    bool loading = false;


    
    public void OnWin()
    {
        
        levelManager = FindObjectOfType<LevelManager>();
        endPanel.SetActive(true);
        result.sprite = resultSprites[1];

        star1.sprite = levelManager.lvObject.FirstStar ? starSprites[1] : starSprites[0];
        goal1.isOn = levelManager.lvObject.FirstStar ? true : false;
        star2.sprite = levelManager.lvObject.SecondStar ? starSprites[1] : starSprites[0];
        goal2.isOn = levelManager.lvObject.SecondStar ? true : false;
        star3.sprite = levelManager.lvObject.ThirdStar ? starSprites[1] : starSprites[0];
        goal3.isOn = levelManager.lvObject.ThirdStar ? true : false;
        
    }

    public void OnLose()
    {
        levelManager = FindObjectOfType<LevelManager>();
        endPanel.SetActive(true);
        result.sprite = resultSprites[0];
    }
    public void OnPause()
    {
        pauseMenu.SetActive(true);
    }

    public void NextLevel()
    {
        endCover.SetActive(true);
        if (!loading)
            StartCoroutine(NextLevelDelay(1f));
    }

    public void FistNextLevel()
    {
        endLongCover.SetActive(true);
        CG.SetActive(true);
        if (!loading)
            StartCoroutine(NextLevelDelay(5f));
    }
    IEnumerator NextLevelDelay(float WaitSecond)
    {
        loading = true;
        
        yield return new WaitForSeconds(WaitSecond); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        endPanel.SetActive(false);
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index >= SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(index);
        loading = false;
    }
    

    private IEnumerator Start()
    {
        //test
        yield return new WaitForSeconds(0.1f);
        levelManager = FindObjectOfType<LevelManager>();
        if(levelManager.lvObject.Complete) OnWin();
        else OnLose();
        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "胜利"))), transform.localPosition);
        
    }

 
    // private void Start()
    // {
    //     levelManager = FindObjectOfType<LevelManager>();
    //     if(levelManager.lvObject.Complete) OnWin();
    //     else OnLose();
    // }
}
