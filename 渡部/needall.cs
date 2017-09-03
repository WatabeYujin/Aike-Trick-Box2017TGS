using UnityEngine;
using System.Collections;

public class needall : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        //Gyro = GameObject.Find("Main Camera").GetComponent<gyro>();//Main Cameraの名前変えんといて
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<DedEvent>().Dedevent();
        }
        if (col.tag == "press")
        {
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<AudioSource>().Play();
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void Bom()
    {
        //爆発にまきこまれたときの処理（任意）

        //演出入れるならこの間に
        Destroy(gameObject); 
    }
}
