using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollisionSound : MonoBehaviour {
    private AudioSource se;
    private Rigidbody ri;
    // Use this for initialization
    void Start () {
        se = GetComponent<AudioSource>();
        ri = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        soundplay();
    }
    void OnTriggerEnter(Collider col)
    {
        soundplay();
    }
    void soundplay()
    {
        
        se.Play();/*
        Debug.Log(ri.velocity.magnitude);
        if (ri.velocity.magnitude > 3) se.Play();*/
    }
    // Update is called once per frame

}
