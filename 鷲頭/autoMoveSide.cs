using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMoveSide : MonoBehaviour {
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject _moveBox;
    private Vector3 mPos;
    private float firstMPos;
    

    // Use this for initialization
    void Start () {
        mPos = _moveBox.transform.position;
        firstMPos = mPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        _moveBox.transform.localPosition = mPos;
        mPos.x += moveSpeed;
        if(mPos.x >= moveDistance + firstMPos)
        {
            moveSpeed *= -1;
        }
        else if (mPos.x < firstMPos)
        {
            moveSpeed *= -1;
        }
    }
}
