using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMoveHeight : MonoBehaviour
{

    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject _moveBox;
    private Vector3 mPos;
    private float firstMPos;


    // Use this for initialization
    void Start()
    {
        mPos = _moveBox.transform.position;
        firstMPos = mPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        _moveBox.transform.localPosition = mPos;
        mPos.y += moveSpeed;
        if (mPos.y >= moveDistance + firstMPos)
        {
            moveSpeed *= -1;
        }
        else if (mPos.y < firstMPos)
        {
            moveSpeed *= -1;
        }
    }
}