using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Result : MonoBehaviour {
    [SerializeField]
    private float time;
    bool timecount=false;
    [SerializeField]
    private float[] runk = new float[3];
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image[] StarImage=new Image[3];
    [SerializeField]
    private GameObject ResultUi;
    //ランクの高い順に書きんしゃい
    [SerializeField]
    private string NextScene;
    GameObject MainCamera;
    [SerializeField]
    GameObject GoalSteam;
    // Use this for initialization
    void Start () {
        MainCamera=GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
        if(timecount)time += Time.deltaTime;
	}
    public void Clear()
    {
        GoalSteam.SetActive(true);
        GameObject.Find("setopen").SetActive(false);
        MainCamera.GetComponent<gyro2>().enabled = false;
        time -=time % 0.1f;
        ResultUi.SetActive(true);
        timecount = false;
        ScoreText.text = time.ToString()+" s";
        for(int i = 0; i < 3; i++)
        {
            if (runk[i] > time)
            {
                for (int j = 0; j < 3-i; j++)
                {
                    
                    StarImage[j].enabled = true;
                }
                break;
            }
        }
    }
    public float timeget
    {
        get
        {
            return time;
        }
    }
    public void movenextscene()
    {
        SceneManager.LoadScene(NextScene);
    }
    public void movetitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void movestageselect()
    {
        SceneManager.LoadScene("stageselect");
    }
    public void countchack(bool isstart)
    {
        timecount = isstart;
    }
}
