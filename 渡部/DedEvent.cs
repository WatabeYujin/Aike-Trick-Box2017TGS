using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedEvent : MonoBehaviour {
    [SerializeField]
    private GameObject explosion;
    private bool isded=false;
    private phasechange PhaseChange;
	// Use this for initialization
	void Start () {
        PhaseChange= GameObject.Find("Main Camera").GetComponent<phasechange>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Dedevent()
    {
        //死亡時のイベント
        //今のシーンを再度読み込む
        if (!isded)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isded = true;
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps.isPlaying == true) return;
            explosion.SetActive(true);
            GetComponent<Renderer>().enabled = false;
            Invoke("Respawn",1.5f);
        }
    }
    public void Respawn()
    {
        explosion.SetActive(false);
        PhaseChange.modechange();
        isded = false;
        GetComponent<Renderer>().enabled = true;
    }
}
