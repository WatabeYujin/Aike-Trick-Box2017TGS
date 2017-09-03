using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour {
    private bool isrunnning = false;
    private phasechange PhaseChange;
    [SerializeField]
    private float MagneticForce = 10;
    void Start () {
        PhaseChange = GameObject.Find("Main Camera").GetComponent<phasechange>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if (col.tag == "Movefloor")
        {
            col.GetComponent<movefloor>().isRunning = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        //&&isrunnning&& PhaseChange.GetMode
        if (col.tag == "Iron")
        {
            Debug.Log("a");
            Vector2 vec = (transform.position - col.transform.position).normalized;
            //col.gameObject.transform.LookAt(transform);
            col.transform.rotation= Quaternion.FromToRotation(Vector3.up, vec);
            //col.transform.eulerAngles = new Vector3(col.transform.eulerAngles.x, col.transform.eulerAngles.y,houkou.x*-1);
            col.GetComponent<Rigidbody>().velocity=col.transform.up * MagneticForce;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Iron")
        {
            col.GetComponent<Rigidbody>().useGravity = true;
        }
        if (col.tag == "Movefloor")
        {
            col.GetComponent<movefloor>().isRunning = true;
        }
    }
    public bool isRunning
    {
        get
        {
            return isrunnning;
        }
        set
        {
            isrunnning = value;
        }
    }
}
