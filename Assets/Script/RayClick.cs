using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClick : MonoBehaviour
{
    public Ray myRay;
    public Collider2D myCollider;
    public GameObject target;
    void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log (collider.name);
    }
    void Update()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(myRay.origin.x, myRay.origin.y), Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            // Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit2D hit = Physics2D.Raycast(new Vector2(myRay.origin.x, myRay.origin.y), Vector2.zero);

            if (hit.collider.CompareTag("Center"))
            {
                Debug.Log("1");
                Debug.Log( hit.collider.name);
            }
        }
    }

}