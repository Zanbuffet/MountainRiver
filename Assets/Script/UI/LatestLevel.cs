using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "latestLevel", menuName = "CreateLatestLevelData")]
public class LatestLevel : ScriptableObject
{
    public int latestLevelID = 0;
    private void OnEnable() {
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
    }   
}
