using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCenter : MonoBehaviour
{
    public List<GameObject> AroundTerrian = new List<GameObject>();
    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
         if (Input.GetKeyDown (KeyCode.Q))  
            {  
                foreach (var at in AroundTerrian)
                {
                    if(at.GetComponent<BaseTerrian>().locked == false){
                    //at.transform.RotateAround(this.transform.localPosition, Vector3.forward, 60f);
                    //at.transform.position = Vector3.Lerp(at.transform.position, goalPos, 10f * Time.deltaTime);
                    at.GetComponent<BaseTerrian>().goalPos = RotateRound(at.transform.localPosition, this.transform.localPosition, Vector3.forward,60f);
                    at.GetComponent<BaseTerrian>()._curState = BaseTerrian.curState.move;
                    at.GetComponent<BaseTerrian>().locked = true;
                    }
                }
                Debug.Log("逆时针旋转");  
            }  
              
        if (Input.GetKeyDown (KeyCode.E))  
            {  
                foreach (var at in AroundTerrian)
                {
                    if(at.GetComponent<BaseTerrian>().locked == false){
                    //at.transform.RotateAround(this.transform.localPosition, Vector3.back, 60f);
                    //at.transform.localPosition = RotateRound(at.transform.localPosition, this.transform.localPosition, Vector3.back,60f);
                    at.GetComponent<BaseTerrian>().goalPos = RotateRound(at.transform.localPosition, this.transform.localPosition, Vector3.back,60f);
                    at.GetComponent<BaseTerrian>()._curState = BaseTerrian.curState.move;
                    at.GetComponent<BaseTerrian>().locked = true;
                    }
                }
                Debug.Log("顺时针旋转");  
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("OnTriggerEnter");
        AroundTerrian.Add(other.gameObject);
    }
 
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerStay");
    // }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("OnTriggerExit");
        AroundTerrian.Remove(other.gameObject);
    }
    public Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * (position - center) + center;
    }
}
