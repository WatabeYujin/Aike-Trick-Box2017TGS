using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMove : MonoBehaviour {
    [SerializeField]
    private float moveDistanceX,moveDistanceY;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject _moveBox;
    private Vector3 mPos;
    private float firstMPosX,firstMPosY;
    

    // Use this for initialization
    void Start () {
        mPos = _moveBox.transform.position;
        firstMPosX = mPos.x;
        firstMPosY = mPos.y;
    }
	
	// Update is called once per frame
	void Update () {
        _moveBox.transform.localPosition = mPos;
        if (mPos.x <= moveDistanceX + firstMPosX && mPos.y <= firstMPosY)
        {
            Debug.Log("a");
            mPos.x += moveSpeed;
        }else if(mPos.x >= moveDistanceX + firstMPosX && mPos.y <= moveDistanceY + firstMPosY)
        {
            mPos.y += moveSpeed;
            Debug.Log("b");
        }
        else if(mPos.x > firstMPosX)
        {
            mPos.x -= moveSpeed;
            Debug.Log("c");
        }
        else if(mPos.x < firstMPosX && mPos.y > firstMPosY)
        {
            mPos.y -= moveSpeed;
            Debug.Log("d");
        }
    }
}
