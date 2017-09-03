using UnityEngine;
using System.Collections;

public class Steam : MonoBehaviour {
    public float buoyancy =2;//どれだけの浮力を発生させるか
    //Rigidbody ri;
	public ParticleSystem particleSteam;

    Vector3 up;
    private DedFall dedfall;
    private bool isSteam = true;//falseでオフ、trueでオン

	// Use this for initialization
	void Start () {
        //ri = GameObject.Find("player").GetComponent<Rigidbody>();
        up = transform.TransformDirection(Vector3.up);
		//float startLifeTime = particleSteam.startLifetime;
		Transform parent = this.transform.parent;
        dedfall = GameObject.Find("player").GetComponent<DedFall>();
        particleSteam.startLifetime = parent.localScale.y / 3 * 4;
        particleSteam.startLifetime += (parent.localScale.y - particleSteam.startLifetime) / 6;
    }
    void OnTriggerStay(Collider col)
    {
		if(isSteam == false)return;
		Rigidbody ri = col.GetComponent<Rigidbody>();
        ri.velocity = up*10;
    }
    void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Iron") {
			isSteam = false;
		}
        if (col.tag != "Movefloor" && col.tag != "Gear") col.transform.position = col.transform.position + up * 0.1f;
    }
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Iron") {
			isSteam = true;
		}
    }


	// Update is called once per frame
	void Update () {
	
	}
}
