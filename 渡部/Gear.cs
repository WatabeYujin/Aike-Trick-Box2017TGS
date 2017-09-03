using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private gyro2 Gyro;
    [SerializeField]
    private Vector3 FirstRotate;
    private bool isRotate;
    [SerializeField]
    private float RotationMagnification=1;
    private phasechange PhaseChange;
    private AudioSource se;
    private float SEGyro;
    void Start () {
        se=GetComponent<AudioSource>();
        Gyro = GameObject.Find("Main Camera").GetComponent<gyro2>();
        FirstRotate =transform.eulerAngles;
        PhaseChange = GameObject.Find("Main Camera").GetComponent<phasechange>();
    }
	
	void Update () {
        if (PhaseChange.GetMode)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Gyro.GetNowGyro * (-1*RotationMagnification));
            if(transform.eulerAngles.z>SEGyro+15|| transform.eulerAngles.z < SEGyro - 15)
            {
                se.Play();
                SEGyro = transform.eulerAngles.z;
            }
        }else
        {
            transform.eulerAngles = FirstRotate;
        }
    }
    public float GetPower
    {
        get
        {
            float power = transform.eulerAngles.z - FirstRotate.z;
            if (power >=180) power = (power-360)*-1;
            return power;
        }
    }
    public float SetRotationMagnification
    {
        set
        {
            RotationMagnification = value;
        }
    }

}
