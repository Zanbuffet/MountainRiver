using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Goal : MonoBehaviour
{
    public List<GameObject> aroundTerrain = new List<GameObject>();
    public int winNumber = 0;
    public int curNumber = 0;
    public int starNumber = 0;
    public int step = 0;
    public bool onComplete = false;
    public static bool completeLevel;
    public static bool secondStar;
    public static bool thirdStar;
    // Start is called before the first frame update
    void Start()
    {
        completeLevel = false;
        secondStar = false;
        thirdStar = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckAroundTerrain();
        GameObject.Find("Canvas/AchievedNumber/achievedNumber").gameObject.GetComponent<TMP_Text>().text = string.Format("{0}/{1}", curNumber.ToString(),winNumber.ToString());
        if(curNumber == winNumber && onComplete == false)
        {
            Win(starNumber);
            onComplete = true;
        }

    }
    private void CheckAroundTerrain()
    {
        int tmpNumber = 0;
        foreach (var terrain in aroundTerrain)
        {
            
            if (terrain.CompareTag("Bull"))
            {
            if(terrain.GetComponent<Bull>().state==Bull.State.Big)
                {
                    tmpNumber++;//第二颗星获得条件，过关时牛均为大牛状态
                }
            }
            starNumber = tmpNumber;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        aroundTerrain.Add(other.gameObject);
        if (other.CompareTag("Bull"))
            {
                curNumber++;//检测当前周围有多少头牛
            }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        aroundTerrain.Remove(other.gameObject);
        if (other.CompareTag("Bull"))
            {
                curNumber--;
            }
     }

     private void Win(int starNumber)
    {
        completeLevel = true;
        if(starNumber == winNumber)
        {
            secondStar = true;
        }
        if(RotationCenter.tmpThirdStar)
        {
            thirdStar = true;
        }

        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "胜利"))), transform.localPosition);

        var audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioSource>().clip = null;
        GameObject.Find("Canvas").gameObject.GetComponent<EndPanel>().enabled = true;
        GameObject root = GameObject.Find("LevelStarManager");
        root.transform.Find("LevelManager").gameObject.SetActive(true); 
        GameObject.Find("TerrianManager").GetComponent<TerrianManager>().DeSelect();
        GameObject.Find("TerrianManager").GetComponent<TerrianManager>().enabled = false;
    }
}
