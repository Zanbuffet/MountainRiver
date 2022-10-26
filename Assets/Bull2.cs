using System;
using System.Collections;
using System.Collections.Generic;
//using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class Bull2 : MonoBehaviour
{
    public List<GameObject> aroundTerrain = new List<GameObject>();

    public GameObject bullState;
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
        Bull.bullCode++;
        Debug.Log(Bull.bullCode);
        combineState();
    }

    private void FixedUpdate()
    {
        CheckAroundTerrain();
        CheckSprite();
        CheckStatement();
    }
    private void combineState()
    {
        GameObject root = GameObject.Find("Canvas");

        if(Bull.bullCode == 1)
        {
            bullState = root.transform.Find("BullState1").gameObject;
            bullState.SetActive(true); 
        }else if(Bull.bullCode == 2)
        {
            bullState = root.transform.Find("BullState2").gameObject;
            bullState.SetActive(true); 
        }
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
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "强盗牛吃东西"))), transform.localPosition);
                terrain.transform.tag = "Used";
                Failed();
            }
            if (terrain.CompareTag("Lion"))
            {
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "狮子吃东西"))), transform.localPosition);
                terrain.transform.tag = "Used";
                Failed();
            }
            if (terrain.CompareTag("Grass"))
            {
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "牛叫1"))), transform.localPosition);
                switch (state)
                {
                    case State.Small:
                        //m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull2");   // 牛变为中等大小
                        state = State.Middle;
                        break;
                    case State.Middle:
                        //m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull3");   // 牛变大
                        state = State.Big;
                        break;
                    case State.Big:                                                 // 游戏失败
                        Failed();
                        break;
                }
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                terrain.transform.tag = "Used";
            }
            if (terrain.CompareTag("Cropland"))
            {
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "温泉合_终版"))), transform.localPosition);
                switch (state)
                {
                    case State.Small:                                               // 游戏失败
                        Failed();
                        break;
                    case State.Middle:
                        //m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull1");   // 牛变小
                        state = State.Small;
                        break;
                    case State.Big:
                        //m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/bull2");   // 牛变为中等大小
                        state = State.Middle;
                        break;
                }
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                terrain.transform.tag = "Used";
                // DoMove
                GetComponent<BaseTerrian>().goalPos = terrain.GetComponent<BaseTerrian>().goalPos;     //牛被田野吸过去
                GetComponent<BaseTerrian>()._curState = BaseTerrian.curState.move;

            }
        }
    }
    private void CheckSprite()
    {
        switch (state)
                {
                    case State.Small:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/瘦牛2");   
                        break;
                    case State.Middle:
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/牛2");   
                        break;
                    case State.Big:                                                 
                        m_SpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/肥牛2");   
                        break;
                }
    }
    private void CheckStatement()
    {
        switch (state)
        {
            case State.Small:                                               // 游戏失败
                bullState.GetComponentInChildren<TMP_Text>().text = string.Format("{0}", "50%");
                break;
            case State.Middle:
                bullState.GetComponentInChildren<TMP_Text>().text = string.Format("{0}", "100%");
                break;
            case State.Big:
                bullState.GetComponentInChildren<TMP_Text>().text = string.Format("{0}", "200%");
                break;
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

    private void Failed()
    {
        // #if UNITY_EDITOR
        //     UnityEditor.EditorApplication.isPlaying = false;
        // #endif
        
        GameObject root = GameObject.Find("Canvas");

        var audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioSource>().clip = null;

        if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
        AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "失败"))), transform.localPosition);
        root.transform.Find("FailedMenu").gameObject.SetActive(true); 
        GameObject.Find("TerrianManager").GetComponent<TerrianManager>().DeSelect();
        GameObject.Find("TerrianManager").GetComponent<TerrianManager>().enabled = false;
        // InputSystem.DisableDevice(Keyboard.current);      
    }
}
