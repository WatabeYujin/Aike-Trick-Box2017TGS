using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFloor_p : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("ELA") || other.gameObject.CompareTag("ELB"))
        transform.SetParent(other.transform);
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ELA") || other.gameObject.CompareTag("ELB"))
            transform.SetParent(null);
    }
}
