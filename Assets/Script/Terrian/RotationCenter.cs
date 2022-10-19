using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotationCenter : MonoBehaviour
{
    public List<GameObject> AroundTerrian = new List<GameObject>();
    public bool selected = false;
    public int starStep;
    public int normalStep;
    public static int leftStarStep;
    public static int leftNormalStep;
    public static bool tmpThirdStar;
    [SerializeField] TextMeshProUGUI leftStarStepText;
    [SerializeField] TextMeshProUGUI leftNormalText;
    // Start is called before the first frame update
    void Start()
    {
        leftStarStep = starStep;
        leftNormalStep = normalStep;
        Debug.Log("三星旋转步数条件："+leftStarStep+"次");
        tmpThirdStar = true;
        leftStarStepText.text = starStep.ToString();
        leftNormalText.text = normalStep.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
         if (Input.GetKeyDown (KeyCode.Q))  
            {  
                leftStarStep--;
                leftNormalStep--;
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
            }  
              
        if (Input.GetKeyDown (KeyCode.E))  
            {  
                leftStarStep--;
                leftNormalStep--;
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

            } 
        }
        if (leftStarStep <=0)
        {
            Debug.Log("三星挑战失败");//第三颗星获得条件，过关时不超过指定步数
            leftStarStep = 0;
            tmpThirdStar = false;
        }  
        if (leftNormalStep<=0)
        {
            Failed();//如果超过了指定步数则游戏失败
        }  
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = leftStarStep.ToString();
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = leftNormalStep.ToString();
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
    private void Failed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
