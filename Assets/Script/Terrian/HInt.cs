using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HInt : MonoBehaviour
{
    public List<GameObject> AroundTerrian = new List<GameObject>();
    private SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var at in AroundTerrian)
            {
                if(at.transform.tag == "Base")
                {
                    at.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/空地_选取态");
                }
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        AroundTerrian.Add(other.gameObject);
        if(other.transform.tag == "Base")
            {
                other.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/空地_选取态");
            }
    }
 
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerStay");
    // }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("OnTriggerExit");
        AroundTerrian.Remove(other.gameObject);
        if(other.transform.tag == "Base")
            {
                other.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/空地_常态");
            }
    }
}
