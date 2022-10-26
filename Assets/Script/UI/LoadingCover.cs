using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCover : MonoBehaviour
{
    public static LoadingCover Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
