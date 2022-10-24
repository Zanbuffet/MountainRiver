using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommander : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    // Start is called before the first frame update
    public void OnPause()
    {
        pauseMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            OnPause();
        }
    }
}
