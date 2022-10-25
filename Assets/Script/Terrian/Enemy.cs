using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
        if(this.transform.tag == "enemy"){
        foreach (var terrain in aroundTerrain)
        {
            if (terrain.CompareTag("Grass"))
            {
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/草2");
                if(!GameObject.Find("AudioManager").GetComponent<AudioSource>().mute)
                AudioSource.PlayClipAtPoint(Resources.Load<UnityEngine.AudioClip>((string.Format("{0}/{1}", "Audio", "强盗牛吃东西"))), transform.localPosition);
                terrain.transform.tag = "Used";
            }
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