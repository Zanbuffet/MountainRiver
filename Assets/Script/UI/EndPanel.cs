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
    [SerializeField] private Toggle goal1;
    [SerializeField] private Toggle goal2;
    [SerializeField] private Toggle goal3;

    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;


    [SerializeField] private Image result;
    [SerializeField] private Sprite[] resultSprites;

    [SerializeField] private Sprite[] starSprites;
    
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

    public void NextLevel()
    {
        endPanel.SetActive(false);
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index >= SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(index);
    }
    private IEnumerator Start()
    {
        //test
        yield return new WaitForSeconds(1f);
        levelManager = FindObjectOfType<LevelManager>();
        if(levelManager.lvObject.Complete) OnWin();
        else OnLose();
        
    }
}
