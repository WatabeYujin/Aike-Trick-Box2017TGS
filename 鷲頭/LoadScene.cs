using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void stage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void stage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void stage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void stage4()
    {
        SceneManager.LoadScene("Stage4");
    }

    public void stage5()
    {
        SceneManager.LoadScene("Stage5");
    }

    public void title()
    {
        SceneManager.LoadScene("Title");
    }
}
