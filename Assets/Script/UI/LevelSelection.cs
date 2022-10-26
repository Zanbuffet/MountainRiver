 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;
    [SerializeField] private GameObject endCover;
    bool loading = false;
    [SerializeField] private LatestLevel m_LatestLevel;


    public void BackToMainMenu()
    {
        m_LatestLevel.latestLevelID = 0;
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        SwitchLevel(m_LatestLevel.latestLevelID);
    }

    public void SwitchLevel(int level)
    {
        switch (level)
        {
            case 1:
                level1.SetActive(true);
                level2.SetActive(false);
                level3.SetActive(false);
                break;
            case 2:
                level1.SetActive(false);
                level2.SetActive(true);
                level3.SetActive(false);
                break;
            case 3:
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(true);
                break;
            default:
                level1.SetActive(true);
                level2.SetActive(false);
                level3.SetActive(false);
                break;
        }
    }
    
    public void SelectLevel(int levelID)
    {
        endCover.SetActive(true);
        if (!loading)
            StartCoroutine(SelectLevelMenuDelay(1f, levelID));
    }

    IEnumerator SelectLevelMenuDelay(float WaitSecond, int levelID)
    {
        loading = true;
        yield return new WaitForSeconds(WaitSecond); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        SceneManager.LoadScene(levelID);
        loading = false;
    }
}
