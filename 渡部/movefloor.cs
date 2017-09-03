using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movefloor : MonoBehaviour {
    /// <summary>
    /// 移動先のオブジェクトをmovetransformに入れると初期地点と移動先を往復します。
    /// 移動先のオブジェクトはカラのゲームオブジェクトにしてください。
    /// 移動先にオブジェクトがあった場合のことはまだ仕様が固まってないので未定です。
    /// </summary>
    [SerializeField]
    private Transform movetransform;
    [SerializeField]
    private float movespeed=3;
    [SerializeField]
    private float stopwait=0;
    [SerializeField]
    private bool isrunning = true;
    private int moveint=1;
    private bool move_=false;
    private Vector2[] movepos = new Vector2[2];
    [SerializeField]
    private Animator animator;
    Rigidbody ri;
    void Start () {
        ri = GetComponent<Rigidbody>();
        movetransform.LookAt(transform);
        movepos[0] = transform.position;
        movepos[1] = movetransform.position;
	}
    void Update() {
        if (isrunning)
        {
            animator.speed =    1;
            ri.constraints = RigidbodyConstraints.FreezeRotation;
            animator.SetInteger("move", moveint);
            animator.SetBool("movebool", move_);
            if (!move_)
            {
                switch (moveint)
                {
                    case 0:
                        ri.velocity= movetransform.forward * movespeed;
                        //transform.position += movetransform.forward * movespeed * Time.deltaTime;
                        break;
                    case 1:
                        ri.velocity = movetransform.forward * movespeed*-1;
                        //transform.position += movetransform.forward * movespeed * -1 * Time.deltaTime;
                        break;
                }
            }
            if (!move_ && Vector2.Distance(transform.position, movepos[moveint]) < 0.5f )
            {
                move_ = true;
                Invoke("movewait", stopwait);
            }
        }else
        {
            animator.speed = 0;
        }
    }
    public void movewait()
    {
        
        switch (moveint)
        {
            case 0:
                moveint = 1;
                break;
            case 1:
                moveint = 0;
                break;
        }
        move_ = false;
    }
    public bool isRunning
    {
        get
        {
            return isrunning;
        }
        set
        {
            isrunning = value;
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
