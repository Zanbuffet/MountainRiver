using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelObject", menuName = "CreateLevelObject")]
public class LevelObject : ScriptableObject
{
    public string LevelName;
    public bool Complete;
    public bool FirstStar;
    public bool SecondStar;
    public bool ThirdStar;
}
