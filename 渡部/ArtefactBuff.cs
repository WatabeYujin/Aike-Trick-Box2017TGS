using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactBuff : MonoBehaviour
{
    /// <summary>
    /// アーティファクトを新たに適用させるときは先のこのスクリプトが入り、
    /// それから対応したアーティファクトを適用させます。
    /// </summary>
    private ArtefactSet artefactset;
    [SerializeField]
    private int UseArtefact;
    private void Start()
    {
        artefactset=GameObject.Find("Main Camera").GetComponent<ArtefactSet>();
    }
    public int Addition
    {
        set
        {
            UseArtefact = value;
            ADDandDESTROY(true);
        }
    }
    public int delete
    {
        get
        {
            if (UseArtefact == 2) transform.position = GetComponent<pincet>().GetDefaultPos;
            Destroy(this);
            ADDandDESTROY(false);
            return UseArtefact;
        }
    }
    private void ADDandDESTROY(bool isAdd)
    {
        //対応するアーティファクトを適用、あるいは削除するメソッドです。
        //boolには適用させる場合はtrueを削除する場合はfalseと入力してください。
        switch (UseArtefact)
        {
            case 0:
                //Non
                if(isAdd) gameObject.AddComponent<TestArtefact>();
                else Destroy(GetComponent<TestArtefact>());
                break;
            case 2:
                //PIncet
                if (isAdd) gameObject.AddComponent<pincet>();
                else Destroy(GetComponent<pincet>());
                break;
            case 3:
                //Pipetto
                if (isAdd) gameObject.AddComponent<Pipetto>();
                else Destroy(GetComponent<Pipetto>());
                break;
            default:
                break;
        }
    }
    public int UseArtefactGet
    {
        get
        {
            return UseArtefact;
        }
    }
}
