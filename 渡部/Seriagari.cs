using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seriagari : MonoBehaviour {
    [SerializeField]
    private Gear gear;
    private float power;
    [SerializeField]
    private float MinRimit = 0;
    [SerializeField]
    private float MaxRimit=2;
    private Vector3 FastPosition;
    private Transform child;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float stopmove;
    bool movemode=false;
	// Use this for initialization
	void Start () {
        child = transform.GetChild(0).transform;
        FastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        power=gear.GetPower/(360/MaxRimit/2);
        power=Mathf.Clamp(power, MinRimit, MaxRimit);
        if (movemode) power = Mathf.Clamp(power, 0, stopmove/1.2f);
        child.position = transform.up * power+ FastPosition;
        child.localScale = new Vector3(1, 1 + power, 1);
        SetAnimationFrame((power / MaxRimit));
    }
    public void SetAnimationFrame(float i_frame)
    {
        var clipInfoList = animator.GetCurrentAnimatorClipInfo(0);
        var clip = clipInfoList[0].clip;
        var time = 15 * i_frame / clip.frameRate;
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var animationHash = stateInfo.shortNameHash;
        animator.Play(animationHash, 0, time);
    }
    public void seriagaristop(bool movebool_)
    {
        if (!movemode)stopmove = power;
        movemode = movebool_;
    }
}
