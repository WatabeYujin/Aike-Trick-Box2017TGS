using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pincet : MonoBehaviour
{
    private Vector3 magnification;
    private int DefaultLayer;
    private ArtefactSet artefactset;
    private phasechange PhaseChange;
    private bool Movemode;
    Camera camera;
    Ray ray;
    private Vector3 DefaultPosition;//このアーティファクトを適用する直前の座標
    private Vector3 StartPosition;//モードを切り替える直前の座標
    private RaycastHit[] hit = new RaycastHit[2];
    Touch touch;
    void Start()
    {
        BoxCollider boxcol = GetComponent<BoxCollider>();
        magnification = new Vector3(boxcol.size.x, boxcol.size.y, boxcol.size.z);
        DefaultLayer = gameObject.layer;
        Debug.Log("アーティファクト『ピンセット』の適用完了\n適用したオブジェクトはアーティファクトフェイズでのみ移動可能");
        artefactset = Camera.main.gameObject.GetComponent<ArtefactSet>();
        PhaseChange = Camera.main.gameObject.GetComponent<phasechange>();
        DefaultPosition = transform.position;
        camera = GetComponent<Camera>();
        StartPosition = transform.position;
    }
    void Update()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit[0] = new RaycastHit();
        if (!PhaseChange.GetMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit[0]))
                {
                    if (hit[0].collider.transform == gameObject.transform)
                    {
                        gameObject.layer = 2;
                        Movemode = true;
                    }
                }
            }
            if (Input.GetMouseButton(0) && Movemode)
            {
                if (Physics.Raycast(ray, out hit[0]) && hit[0].transform.tag == "walls")
                {
                    transform.position = new Vector3(hit[0].point.x, hit[0].point.y, 0);
                    ObjectWallCheck();
                }
                else
                {
                    for (int i = 0; i < artefactset.ArtefactGet.Length; i++)
                    {
                        if ((int)artefactset.ArtefactGet[i] == 2)
                        {
                            Destroy(artefactset.outlinetrans);
                            artefactset.RemainingGet[i]++;
                            artefactset.ReaningSet(i);
                            i = GetComponent<ArtefactBuff>().delete;

                            break;
                        }
                    }
                    gameObject.layer = DefaultLayer;
                    transform.position = DefaultPosition;
                    //transform.position = DefaultPosition;
                }
            }
            if (Input.GetMouseButtonUp(0) && Movemode)
            {
                Movemode = false;
                if (Physics.Raycast(ray, out hit[0]) && hit[0].transform.tag == "walls")
                {
                    StartPosition = transform.position;
                    gameObject.layer = DefaultLayer;
                }
                else
                {
                    for (int i = 0; i < artefactset.ArtefactGet.Length; i++)
                    {
                        if ((int)artefactset.ArtefactGet[i] == 2)
                        {
                            Destroy(artefactset.outlinetrans);
                            artefactset.RemainingGet[i]++;
                            artefactset.ReaningSet(i);
                            i = GetComponent<ArtefactBuff>().delete;

                            break;
                        }
                    }
                    gameObject.layer = DefaultLayer;
                    transform.position = DefaultPosition;
                }
            }
        }
#elif UNITY_ANDROID
        if (!PhaseChange.GetMode)
        {
            for (int j = 0; j < Input.touchCount; j++)
            {
                touch = Input.GetTouch(j);
                Vector2 click = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(click);
                hit[0] = new RaycastHit();
                if (touch.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit[0]))
                    {
                        if (hit[0].collider.transform == gameObject.transform)
                        {
                            gameObject.layer = 2;
                            Movemode = true;
                        }
                    }
                }
                if (touch.phase == TouchPhase.Moved && Movemode)
                {
                    if (Physics.Raycast(ray, out hit[0]) && hit[0].transform.tag == "walls")
                    {
                        transform.position = new Vector3(hit[0].point.x, hit[0].point.y, 0);
                        ObjectWallCheck();
                    }
                    else
                    {
                        for (int i = 0; i < artefactset.ArtefactGet.Length; i++)
                        {
                            if ((int)artefactset.ArtefactGet[i] == 2)
                            {
                                Destroy(artefactset.outlinetrans);
                                artefactset.RemainingGet[i]++;
                                artefactset.ReaningSet(i);
                                i = GetComponent<ArtefactBuff>().delete;

                                break;
                            }
                        }
                        gameObject.layer = DefaultLayer;
                        transform.position = DefaultPosition;
                        //transform.position = DefaultPosition;
                    }
                }
                if (touch.phase == TouchPhase.Ended && Movemode)
                {
                    Movemode = false;
                    if (Physics.Raycast(ray, out hit[0]) && hit[0].transform.tag == "walls")
                    {
                        StartPosition = transform.position;
                        gameObject.layer = DefaultLayer;
                    }
                    else
                    {
                        for (int i = 0; i < artefactset.ArtefactGet.Length; i++)
                        {
                            if ((int)artefactset.ArtefactGet[i] == 2)
                            {
                                Destroy(artefactset.outlinetrans);
                                artefactset.RemainingGet[i]++;
                                artefactset.ReaningSet(i);
                                i = GetComponent<ArtefactBuff>().delete;

                                break;
                            }
                        }
                        gameObject.layer = DefaultLayer;
                        transform.position = DefaultPosition;
                    }
                }
            }
        }
#endif
    }
    private void ObjectWallCheck()
    {
        bool isHit = Physics.BoxCast(transform.position, new Vector3(0.01f, (transform.lossyScale.x * magnification.x) / 4, 0.1f), Vector3.right, out hit[1], transform.rotation, transform.localScale.x * magnification.x / 2 + 0.1f);
        if (isHit)
        {
            transform.position += new Vector3(hit[1].distance - (transform.lossyScale.x * magnification.x / 2), 0, 0);
        }
        else
        {
            isHit = Physics.BoxCast(transform.position, new Vector3(0.01f, transform.lossyScale.x * magnification.x / 4f, 0.1f), Vector3.right * -1, out hit[1], transform.rotation, transform.lossyScale.x * magnification.x / 2 + 0.1f);
            if (isHit)
            {
                transform.position -= new Vector3(hit[1].distance - (transform.lossyScale.x * magnification.x / 2), 0, 0);
            }
        }
        isHit = Physics.BoxCast(transform.position, new Vector3(transform.lossyScale.y * magnification.y / 4f, 0.01f, 0.1f), Vector3.up, out hit[1], transform.rotation, transform.lossyScale.y * magnification.y / 2 + 0.1f);
        if (isHit)
        {
            transform.position += new Vector3(0, hit[1].distance - (transform.lossyScale.y * magnification.y / 2), 0);
        }
        else
        {
            isHit = Physics.BoxCast(transform.position, new Vector3(transform.lossyScale.x * magnification.y / 4f, 0.01f, 0.1f), Vector3.up * -1, out hit[1], transform.rotation, transform.lossyScale.y * magnification.y / 2 + 0.1f);
            if (isHit)
            {
                transform.position -= new Vector3(0, hit[1].distance - (transform.lossyScale.y * magnification.y / 2), 0);
            }
        }
    }
    public Vector3 GetDefaultPos
    {
        get
        {
            return DefaultPosition;
        }
    }
}
