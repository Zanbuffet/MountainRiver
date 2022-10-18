using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchLevel(int level)
    {
        switch (level)
        {
            case 1:
                level1.SetActive(true);
                level2.SetActive(false);
                break;
            case 2:
                level1.SetActive(false);
                level2.SetActive(true);
                level3.SetActive(false);
                break;
            case 3:
                level2.SetActive(false);
                level3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
