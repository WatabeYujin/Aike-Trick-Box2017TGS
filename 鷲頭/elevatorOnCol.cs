using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorOnCol : MonoBehaviour {

    [SerializeField]
    private GameObject _elOperate,_elA;

    private bool isMove = false;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update ()
    {
        elOperate EL = _elOperate.GetComponent<elOperate>();
        if (isMove)
        {
            if (this.gameObject.CompareTag("ELA"))
            {
                transform.SetParent(this.transform);
                EL.moveElevatorA();
            }
            else if (this.gameObject.CompareTag("ELB"))
            {
                EL.moveElevatorB();
            }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                isMove = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {    
            iTween.Stop();
            isMove = false;
            iTween.Stop(_elA, "move");
        }
    }
}
