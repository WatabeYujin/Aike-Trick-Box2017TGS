using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class goal : MonoBehaviour {
    public string nextsceneName;
    Result result;
	// Use this for initialization
	void Start () {
        result = GameObject.Find("playerset").GetComponent<Result>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player") result.Clear();
    }
}
