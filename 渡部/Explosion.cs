using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public string[] breaktagname = new string[3];
	// Use this for initialization
	void Start () {
	
	}/*
    void OnCollisionStay(Collision col)
    {
        Debug.Log(col.gameObject.name);
        for (int i = 0; i < breaktagname.Length; i++)
        {
            if (col.gameObject.tag == breaktagname[i])
            {
                col.gameObject.SendMessage("Bom");   //相手のBom関数を実行する
            }
        }
    }*/
    void OnTriggerStay(Collider col)
    {
        Debug.Log(col.name);
        for (int i = 0; i < breaktagname.Length;i++ ){
            if (col.gameObject.tag==breaktagname[i])
            {
                col.gameObject.SendMessage("Bom");   //相手のBom関数を実行する
            }
        }
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
