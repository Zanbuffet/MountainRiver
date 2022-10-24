using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelObject lvObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!lvObject.Complete)
        {
        lvObject.Complete = Goal.completeLevel;
        }
        if(lvObject.Complete){
        if(!lvObject.FirstStar)
        lvObject.FirstStar = Goal.completeLevel;
        if(!lvObject.SecondStar)
        lvObject.SecondStar = Goal.secondStar;
        if(lvObject.IsFirstWorld)
        lvObject.SecondStar = true;
        if(!lvObject.ThirdStar)
        lvObject.ThirdStar = Goal.thirdStar;
        }
    }
}
