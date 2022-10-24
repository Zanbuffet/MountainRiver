using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrianManager : MonoBehaviour
{
    public RotationCenter rotationCenter;
    public Collider2D rotationCenterCollider;
    public Rigidbody2D rotationCenterRigidbody;
    public Enemy enemy;
    public Bull bull;
    public GameObject hint;

    // Start is called before the first frame update
    void Start()
    {

    }

    /*
     * 1. 第一次点击地块时
     * 2. 点击同一重复地块时
     * 3. 点击不同地块时
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Center"))
            {
                if (rotationCenter == null )
                {
                    rotationCenter = hit.collider.GetComponent<RotationCenter>();
                    rotationCenterCollider = hit.collider.GetComponent<Collider2D>();
                    rotationCenterCollider.isTrigger = true;

                    //rotationCenterRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                    //rotationCenterRigidbody.simulated = true;

                    rotationCenter.selected = true;
                    Debug.Log(rotationCenter.name);
                    hint.transform.localPosition = rotationCenter.transform.localPosition;

                }
                else if (hit.collider.GetComponent<RotationCenter>() == rotationCenter )
                {
                    rotationCenter.selected = false;
                    //rotationCenter.AroundTerrian.Clear();
                    //rotationCenterCollider.isTrigger = false;
                    //rotationCenterRigidbody.simulated = false;
                    rotationCenter = null;
                    hint.transform.localPosition = new Vector3(-2000,2000,0);
                }else
                {
                    rotationCenter.selected = false;

                    rotationCenter = hit.collider.GetComponent<RotationCenter>();
                    rotationCenterCollider = hit.collider.GetComponent<Collider2D>();
                    rotationCenterCollider.isTrigger = true;

                    //rotationCenterRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                    //rotationCenterRigidbody.simulated = true;

                    rotationCenter.selected = true;
                    Debug.Log(rotationCenter.name);
                    hint.transform.localPosition = rotationCenter.transform.localPosition;
                }
            }
            
    }else if (Input.GetMouseButtonDown(2)){
        if(rotationCenter != null)
        {
            rotationCenter.selected = false;
            //rotationCenter.AroundTerrian.Clear();
            //rotationCenterCollider.isTrigger = false;
            //rotationCenterRigidbody.simulated = false;
            rotationCenter = null;
        }
        }
}
    public void DeSelect()
    {
        if (rotationCenter != null )
        {
            rotationCenter.selected = false;
            rotationCenter = null;
        }
    }
}
