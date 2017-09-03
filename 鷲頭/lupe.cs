using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lupe : MonoBehaviour {
    public static Vector3 cameraPos,firstCameraPos;
    private GameObject MainCamera;
    [SerializeField]
    private int zoomZ;
    private phasechange PhaseChange;
    // Use this for initialization
    void Start () {
        MainCamera = GameObject.Find("Main Camera");
        firstCameraPos = MainCamera.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        cameraPos = this.transform.position;
        cameraPos.z = zoomZ*-1;
        MainCamera.transform.localPosition = cameraPos;

        // Debug.Log("hakoの場所: " + this.transform.position);
    }
    public void Reset()
    {
        MainCamera.transform.localPosition = firstCameraPos;
    }
}
