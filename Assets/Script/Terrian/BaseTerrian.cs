using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTerrian : MonoBehaviour
{
    public Vector3 goalPos;
    public bool locked = false;
    public enum curState
    {
        idle,
        move,
    }
    public curState _curState = curState.idle;
    // Start is called before the first frame update
    void Start()
    {
        goalPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_curState)
        {
            case curState.idle:
                break;

            case curState.move:
                DoMove();
                break;
        }
    }
    public void DoMove()
    {
        Vector3 _dirVec = (Vector2)goalPos - (Vector2)transform.position;
        if (_dirVec.sqrMagnitude < 0.01f)
        {
            _curState = curState.idle;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, 10f);
            locked = false;
            RotationCenter.onMove = false;
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * 10f * Time.deltaTime);
    }
}
