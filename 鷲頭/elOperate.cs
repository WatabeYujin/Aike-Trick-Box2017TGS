using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elOperate : MonoBehaviour {

    [SerializeField]
    private GameObject _elA, _elB;

    [SerializeField]
    private int maxPosY;
    private int minPosY = 1;

    [SerializeField]
    private float time;

    private Hashtable maxMovePos = new Hashtable();
    private Hashtable minMovePos = new Hashtable();


    // Use this for initialization
    void Start () {
        maxPosY = maxPosY + 1;
        maxMovePos.Add("time", time);
        minMovePos.Add("time", time);
        maxMovePos.Add("y", maxPosY);
        minMovePos.Add("y", minPosY);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveElevatorA()
    {
        Vector3 Apos = _elA.transform.position;

        if (Apos.y <= maxPosY)
        {
            iTween.MoveTo(_elA, minMovePos);

            iTween.MoveTo(_elB, maxMovePos);
        }
    }

    public void moveElevatorB()
    {
        Vector3 Bpos = _elB.transform.position;
        if (Bpos.y <= maxPosY)
        {
            iTween.MoveTo(_elA, maxMovePos);

            iTween.MoveTo(_elB, minMovePos);
        }
    }
}
