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
        foreach (var terrain in aroundTerrain)
        {
            if (terrain.CompareTag("Cropland"))
            {
                terrain.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
                terrain.transform.tag = "Base";
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