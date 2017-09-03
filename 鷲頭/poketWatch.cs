using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poketWatch : MonoBehaviour {
    private Vector3 localGravity = new Vector3(0,-30f,0);
    private Rigidbody rb;
    private bool isTime = false;
    private float limit;
    [SerializeField]
    private float limitTime;
    private gyro2 Gyro;
    private float[] setGyro = new float[2];
    private float gravity = 15f;

    void Start () {
        rb = this.GetComponent<Rigidbody>();
        //rb.useGravity = false;
        isTime = true;
        limit = limitTime;
        Gyro = GameObject.Find("Main Camera").GetComponent<gyro2>();

    }

    void Update () {
        Debug.Log("GetNowGyro=" + Gyro.GetNowGyro);
        setLocalGravity();
        if (isTime)
        {
            limitTime -= Time.deltaTime;
            //Debug.Log("TIMELIMIT=" + limitTime);
        }
        if (limitTime <= 0)
        {
            limitTime = limit;
            isTime = false;
        }

    }

    void setLocalGravity()
    {
        if (Gyro.GetNowGyro <= 90 || Gyro.GetNowGyro >= 270)
        {
            if (Gyro.GetNowGyro >= 270) setGyro[0] = 270 - Gyro.GetNowGyro;
            else setGyro[0] = Gyro.GetNowGyro - 90;
        }
        else
        {
            if (Gyro.GetNowGyro <= 180) setGyro[0] = Gyro.GetNowGyro - 90;
            else setGyro[0] = Gyro.GetNowGyro - 180;
        }

        if (Gyro.GetNowGyro <= 180)
        {
            if (Gyro.GetNowGyro <= 90) setGyro[1] = Gyro.GetNowGyro * -1;
            else setGyro[1] = Gyro.GetNowGyro - 180;
        }
        else
        {
            if (Gyro.GetNowGyro <= 270) setGyro[1] = Gyro.GetNowGyro - 180;
            else setGyro[1] = 360 - Gyro.GetNowGyro;
            
        }

        for (int i = 0; i < 2; i++)
        {
            setGyro[i] /= 90;
        }

        rb.velocity = new Vector3(setGyro[1] * gravity, setGyro[0] * gravity, 0);
        rb.velocity *= -1;
        //Physics.gravity = new Vector3();
    }
}
