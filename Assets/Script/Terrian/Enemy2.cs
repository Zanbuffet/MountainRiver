using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public List<GameObject> aroundTerrain = new List<GameObject>();
    
    private SpriteRenderer m_SpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        foreach (var terrain in aroundTerrain)
        {
            if (terrain.CompareTag("Enemy"))
            {
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "狮子吃东西"))), transform.localPosition);
                terrain.transform.tag = "Used";
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
    
}