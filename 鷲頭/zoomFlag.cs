using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomFlag : MonoBehaviour
{
    private GameObject MainCamera;

    // Use this for initialization
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void isFlag()
    {
        Debug.Log("call");
        MainCamera.transform.localPosition = lupe.firstCameraPos;
        objsetmode.zFlag = false;
    }

}
