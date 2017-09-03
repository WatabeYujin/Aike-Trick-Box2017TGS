using UnityEngine;
using System.Collections;
public class press: MonoBehaviour {
    RaycastHit hit;
    Rigidbody ri;
    [SerializeField]
    private Target target;
    enum Target
    {
        Player,
        WoodBox,
        IronBox
    }
    private phasechange Phasechange;
    private ColObject colobject;
    enum ColObject
    {
        seriagari,
        movefloor,
        other,
        none
    }
    private bool isbreak;
    // Use this for initialization
    void Start () {
        Phasechange = GameObject.Find("Main Camera").GetComponent<phasechange>();
        ri = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isbreak && !Phasechange.GetMode) BreakOrReset(true);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Movefloor")
        {
            colobject = ColObject.movefloor;
        }
    }

    void OnCollisionStay(Collision col){
        if (col.collider.tag == "Seriagari") colobject = ColObject.seriagari;
        else colobject = ColObject.other;
        Vector2 rotate_ = col.transform.position- transform.position;
        Ray ray = new Ray(transform.position, rotate_);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
        if (Physics.Raycast(ray, out hit))
        {
            Vector2 point1 = hit.point;
            ray = new Ray(transform.position, rotate_*-1);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
            if (Physics.Raycast(ray, out hit))
            {
                if(Vector2.Distance(point1 , (Vector2)hit.point) < 1.7f)
                {
                    switch (target)
                    {
                        case Target.Player:
                            Smash();
                            break;
                        case Target.WoodBox:
                            if (colobject != ColObject.other) BreakOrReset(false);
                            break;
                        case Target.IronBox:
                            if (colobject == ColObject.seriagari) col.transform.parent.GetComponent<Seriagari>().seriagaristop(true);
                            else if (colobject == ColObject.movefloor) col.collider.GetComponent<movefloor>().movewait();
                                break;
                    }
                    colobject = ColObject.none;
                }
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Seriagari")
        {
            Debug.Log("呼ばれた");
            col.transform.parent.GetComponent<Seriagari>().seriagaristop(false);
        }
    }
    void Smash(){
        GetComponent<DedEvent>().Dedevent();
	}
    void BreakOrReset(bool SetMode_)
    {
        GetComponent<Collider>().enabled = SetMode_;
        GetComponentInChildren<MeshRenderer>().enabled = SetMode_;
        isbreak = SetMode_;
    }
}
