using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageSelect : MonoBehaviour {
    [SerializeField]
    GameObject[] StageButton;
    [SerializeField]
    Image fedeimage;
    bool isfade;
    string Scenename;
    // Use this for initialization
    void Start () {
        fedeimage.enabled = true;
        StartCoroutine("fedeout");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void back()
    {
        if (!isfade)
        {
            for (int i = 0; i < 3; i++)
            {
                StageButton[i].SetActive(false);
            }
            StageButton[3].SetActive(true);
        }
    }
    public void Stagebutton(int i)
    {
        if (!isfade)
        {
            StageButton[3].SetActive(false);
            StageButton[i].SetActive(true);
        }
    }
    public void SceneMove(string scenename)
    {
        if (!isfade)
        {
            Scenename = scenename;
            StartCoroutine("fede");
        }
    }
    IEnumerator fede()
    {
        fedeimage.enabled = true;
        isfade = true;
        for (int i = 1; i < 51; i++)
        {
            fedeimage.color = new Color(1, 1, 1, 0 + (float)i / 50);
            yield return null;
        }
        SceneManager.LoadScene(Scenename);
    }
    IEnumerator fedeout()
    {
        isfade = true;
        for (int i = 1; i < 51; i++)
        {
            fedeimage.color = new Color(1, 1, 1, 1 - (float)i / 50);
            yield return null;
        }
        isfade = false;
        fedeimage.enabled = false;
    }
}
