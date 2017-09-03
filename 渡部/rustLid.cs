using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rustLid : MonoBehaviour
{
    private phasechange PhaseChange;
    private bool isMove = false;
    [SerializeField]
    Transform parent;
    Rigidbody[] ri=new Rigidbody[2];
    bool OldlMode;
    // Use this for initialization
    void Start()
    {
        PhaseChange = GameObject.Find("Main Camera").GetComponent<phasechange>();
        ri[1] = parent.GetComponent<Rigidbody>();
        ri[0] = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhaseChange.GetMode!=OldlMode)
        {
            OldlMode = PhaseChange.GetMode;
            if (OldlMode)
            {
                if (!GetComponent<Pipetto>())
                {
                    ri[0].useGravity = false;
                    ri[0].constraints = RigidbodyConstraints.FreezeAll;
                }else
                {
                    ri[0].useGravity = true;
                }
                ri[1].constraints = RigidbodyConstraints.FreezeAll;
                ri[1].useGravity = false;
            }

        }
    }
}
    