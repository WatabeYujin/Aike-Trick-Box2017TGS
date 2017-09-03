using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class LoadScene : MonoBehaviour {
    [SerializeField]
    private string Scenename;
    [SerializeField]
    private Image fedeimage;
    private bool isfade=false;
    private Animator anim;
    Result result;
    // Use this for initialization
    void Start () {
        if(GameObject.Find("playerset")!=null)result = GameObject.Find("playerset").GetComponent<Result>();
        anim =GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SceneMove()
    {
        if (fedeimage != null && !isfade) StartCoroutine("fede");
        else if (anim != null)
        {
            result.Clear();
            anim.SetBool("isgoal", true);
        }
        else SceneManager.LoadScene(Scenename);
    }
    IEnumerator fede()
    {
        fedeimage.enabled = true;
        isfade = true;
        for(int i = 1; i < 51; i++)
        {
            fedeimage.color = new Color(1, 1, 1, 0 + (float)i/50);
            yield return null;
        }
        SceneManager.LoadScene(Scenename);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            col.transform.position = transform.Find("Goal").position + new Vector3(0, 0, 0.1f);
            SceneMove();
        }
    }
}
