using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public class Bull : MonoBehaviour
{
    public List<GameObject> aroundTerrain = new List<GameObject>();

    public enum State
    {
        Small,
        Middle,
        Big,
    }

    public State state = State.Middle;
    
    private SpriteRenderer m_SpriteRenderer;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bull2");
    }

    private void FixedUpdate()
    {
        CheckAroundTerrain();
    }
    
    /// <summary>
    /// 检查周围地块是否需要进行交互
    /// <para> 1. 当周围地块存在敌人时游戏失败 </para>
    /// <para> 2. 当周围地块存在粮食时根据state进行逻辑判断 </para>
    /// <para> 3. 当周围地块存在农田时根据state进行逻辑判断 </para>
    /// </summary>
    private void CheckAroundTerrain()
    {
        foreach (var terrain in aroundTerrain)
        {
            if (terrain.CompareTag("Enemy"))
            {
                GetExit();
            }
            if (terrain.CompareTag("Grass"))
            {
                switch (state)
                {
                    case State.Small:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull2");   // 牛变为中等大小
                        state = State.Middle;
                        break;
                    case State.Middle:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull3");   // 牛变大
                        state = State.Big;
                        break;
                    case State.Big:                                                 // 游戏失败
                        GetExit();
                        break;
                }
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                terrain.transform.tag = "Base";
            }
            if (terrain.CompareTag("Cropland"))
            {
                switch (state)
                {
                    case State.Small:                                               // 游戏失败
                        GetExit();
                        break;
                    case State.Middle:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull1");   // 牛变小
                        state = State.Small;
                        break;
                    case State.Big:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull2");   // 牛变为中等大小
                        state = State.Middle;
                        break;
                }
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                terrain.transform.tag = "Base";
                // DoMove
                GetComponent<BaseTerrian>().goalPos = terrain.transform.localPosition;     //牛被田野吸过去
                GetComponent<BaseTerrian>()._curState = BaseTerrian.curState.move;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        aroundTerrain.Add(other.gameObject);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        aroundTerrain.Remove(other.gameObject);
    }

    private void GetExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
