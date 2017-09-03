using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipetto : MonoBehaviour {
    [SerializeField]
    private Gear gear;
    // Use this for initialization
    void Start () {
        gear=GetComponent<Gear>();
        if(gear!=null)gear.SetRotationMagnification = 2;
	}
    void OnDisable()
    {
        if (gear != null) gear.SetRotationMagnification = 1;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
