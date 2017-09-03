using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDecoration : MonoBehaviour {
    gyro2 Gyro;
    Animator animator;
    phasechange PhaseChange;
	// Use this for initialization
	void Start () {
        Gyro = GameObject.Find("Main Camera").GetComponent<gyro2>();
        PhaseChange = GameObject.Find("Main Camera").GetComponent<phasechange>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (PhaseChange.GetMode)
        {
            Debug.Log(Gyro.GetNowGyro);
            SetAnimationFrame(Gyro.GetNowGyro / 360);
        }
        else{
            SetAnimationFrame(0);
        }
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
}
