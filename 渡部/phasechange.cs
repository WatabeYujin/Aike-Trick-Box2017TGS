using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phasechange : MonoBehaviour {
    [SerializeField]
    private bool nowmode = true;
    [SerializeField]
    private GameObject[] obj = new GameObject[2];
    gyro2 Gyro;
    private List<Vector3> objpos = new List<Vector3>();
    private List<Quaternion> objRotate = new List<Quaternion>();
    private GameObject player; 
	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, 0, 0);
        Gyro = GetComponent<gyro2>();
        player = GameObject.Find("player");
        modechange();
    }
     
	// Update is called once per frame
	void Update () {
		
	}
    public void modechange(){
        nowmode = !nowmode;
        rigidbodyfrieze();
        player.transform.eulerAngles = new Vector3(0, 180, 0);
        obj[0].SetActive(!nowmode);
        obj[1].SetActive(nowmode);
        Gyro.enabled = nowmode;
        Physics.gravity = new Vector3(0, 0, 0);
    }
    public void rigidbodyfrieze()
    {
        int n=0;

        foreach (Rigidbody obj in UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)))
        {
            switch (nowmode)
            {
                case true:
                    objpos.Add(obj.transform.position);
                    objRotate.Add(obj.transform.rotation);
                    obj.constraints = RigidbodyConstraints.FreezeRotationX|
                        RigidbodyConstraints.FreezeRotationY|
                        RigidbodyConstraints.FreezePositionZ;
                    break;
                case false:
                    if (objpos.Count!=0)
                    {
                        obj.transform.rotation = objRotate[n];
                        obj.transform.position = objpos[n];
                        

                        obj.constraints = RigidbodyConstraints.FreezeAll;
                    }
                    break;
            }
            n++;
        }
        if (!nowmode)objpos.Clear();
    }
    public bool GetMode
    {
        get
        {
            return nowmode;
        }
    }
}