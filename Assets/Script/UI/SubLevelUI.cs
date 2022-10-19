using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubLevelUI : MonoBehaviour
{
    [SerializeField] private Image envelopeIcon;
    [SerializeField] private Sprite[] envelopSprites;
    
    [SerializeField] private Image starOne;
    [SerializeField] private Image starTwo;
    [SerializeField] private Image starThree;

    [SerializeField] private Sprite[] starSprites;
    [SerializeField] private LevelObject level;

    private void Awake()
    {
        envelopeIcon.sprite = level.Complete ? envelopSprites[1] : envelopSprites[0];

        starOne.sprite = level.FirstStar ? starSprites[1] : starSprites[0];
        starTwo.sprite = level.SecondStar ? starSprites[1] : starSprites[0];
        starThree.sprite = level.ThirdStar ? starSprites[1] : starSprites[0];

    }

    public void SelectLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
}
