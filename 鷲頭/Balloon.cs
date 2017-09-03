using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

    private Vector3 localGravity;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {
        localGravity = Physics.gravity;
        rb.AddForce(-localGravity, ForceMode.Acceleration);
	}
}
