using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedFall : MonoBehaviour {
    /// <summary>
    /// 落下を始めた場所から着地した場所の座標の距離を求めて、
    /// その距離が一定以上であれば落下死するという内容です
    /// </summary>
    [SerializeField]
    private Vector3[] fall = new Vector3[2];
    private float spead;
    [SerializeField]
    private float dedfallrange = 7;//この数値より高い距離から落ちた場合死にます。
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public Vector3 Landing_point
    {
        //groundcheckより着地している間の最新の座標を取得しています。
        set
        {
            fall[1] = value;
            if (fall[0] != Vector3.zero) FallDeathCheck();
        }
    }
    public Vector3 Takeoff_point
    {
        //groundcheckより落下を始めた際の座標を取得しています。
        set
        {
            fall[0] = value;
        }
    }
    void FallDeathCheck(){
        //ここで落下を始めた地点と着地地点の距離を求め、指定の距離（高さ）であれば死ぬ、という処理を行っています。
        Debug.Log((fall[1] - fall[0]).magnitude);
        if ((fall[1] - fall[0]).magnitude >= dedfallrange)
        {
            fall[0] = Vector3.zero;
            GetComponent<DedEvent>().Dedevent();
        }
        else
        {
            fall[0] = Vector3.zero;
        }
    }
}
