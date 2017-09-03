using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balOperate : MonoBehaviour
{

    // シーソー
    [SerializeField]
    private GameObject _balA, _balB;

    // 最大値、最小値
    [SerializeField]
    private int max;
    private int min = 1;

    // 伸縮時間
    [SerializeField]
    private float time;

    private int maxPosY, minPosY;

    private Hashtable maxMoveHash = new Hashtable();
    private Hashtable minMoveHash = new Hashtable();
    private Hashtable maxScaleHash = new Hashtable();
    private Hashtable minScaleHash = new Hashtable();

    // Use this for initialization
    void Start()
    {
        maxPosY = 1 + (max - 1) / 2;
        minPosY = 1 + (min - 1) / 2;

        maxMoveHash.Add("y", maxPosY);
        maxMoveHash.Add("time", time);
        maxScaleHash.Add("y", max);
        maxScaleHash.Add("time", time);
        minMoveHash.Add("y", minPosY);
        minMoveHash.Add("time", time);
        minScaleHash.Add("y", min);
        minScaleHash.Add("time", time);

    }

    // Update is called once per frame
    void Update()
    {

    }

    // _balA着地時
    public void move()
    {
        if (_balA.transform.localScale.y <= max)
        {
            iTween.MoveTo(_balA, minMoveHash);
            iTween.ScaleTo(_balA, minScaleHash);

            iTween.MoveTo(_balB, maxMoveHash);
            iTween.ScaleTo(_balB, maxScaleHash);
        }
    }

    // _balB着地時
    public void move2()
    {
        if (_balB.transform.localScale.y <= max)
        {
            iTween.MoveTo(_balB, minMoveHash);
            iTween.ScaleTo(_balB, minScaleHash);

            iTween.MoveTo(_balA, maxMoveHash);
            iTween.ScaleTo(_balA, maxScaleHash);
        }

    }

}
