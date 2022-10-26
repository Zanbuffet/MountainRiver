using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] private GameObject endCover;
    bool loading = false;
    public void BackToMainMenu()
    {
        endCover.SetActive(true);
        if (!loading)
            StartCoroutine(BackToMainMenuDelay(1f));
    }

    
    IEnumerator BackToMainMenuDelay(float WaitSecond)
    {
        loading = true;
        yield return new WaitForSeconds(WaitSecond); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        SceneManager.LoadScene(1);
        loading = false;
    }

    public void ReloadScene()
    {
        endCover.SetActive(true);
        if (!loading)
            StartCoroutine(ReloadDelay(1f));
    }

    IEnumerator ReloadDelay(float WaitSecond)
    {
        loading = true;
        yield return new WaitForSeconds(WaitSecond); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        loading = false;
    }


    private void Start() {
        if(levelNumber!=null)
        levelNumber.text = GameObject.Find("Level").GetComponent<TmxReader>().levelFile;
    }
    
    
    
}
