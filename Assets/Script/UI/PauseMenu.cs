using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNumber;
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start() {
        if(levelNumber!=null)
        levelNumber.text = GameObject.Find("Level").GetComponent<TmxReader>().levelFile;
    }
    
    
}
