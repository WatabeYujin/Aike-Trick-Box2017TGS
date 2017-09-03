using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript;


public class ArtefactSet : MonoBehaviour {
    /// <summary>
    /// アーティファクトを適用させるスクリプトです。
    /// ボタンからスライドし、オブジェクトの上で離した時適用可能であれば、ArtefactBuff.csを入れてそこから対応するアーティファクトを適用させます。
    /// すでにアーティファクトが入っていた場合、古いものを削除し、新しいものをいれます。
    /// タップしたオブジェクトにアーティファクトが入っていたらそれを削除します。
    /// 削除した場合、使用回数を増やします。
    /// </summary>
    [SerializeField]
    private int set = -1;
    [SerializeField]
    private GameObject LoupeObj;//ルーペのオブジェクトを入れてください。
    [SerializeField]
    private GameObject CloneLoupe;
    [SerializeField]
    private GameObject[] ButtonObj;
    Ray ray;
    private RaycastHit hit;
    Touch touch;
    private Vector2 TouchBeginPos;
    public enum Artefact
    {
        Non,
        Loupe,
        Pincet,
        Pipetto
    }
    [SerializeField]
    private Artefact[] artefact;//アーティファクトの効果です。
    [SerializeField]
    private Image UseArtefactObj;
    [SerializeField]
    private Sprite[] UseArtefactImage;
    [SerializeField]
    private int[] remaining;//残りの使用回数です。-1に設定した場合、何度でも使えます。
                            //また、要素数は上の列挙体のartefactと同じにしてください。
    private bool StartReningSet = false;
    private phasechange PhaseChange;
    [SerializeField]
    private GameObject outlineobj;
    public GameObject outlinetrans;
    private void Start()
    {
        PhaseChange = GetComponent<phasechange>();
        if (!StartReningSet)
        {
            for (int i = 0; i < artefact.Length; i++)
            {
                ReaningSet(i);
            }
            StartReningSet = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            TouchBeginPos = Input.mousePosition;
            switch (set)
            {
                case -1:
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.GetComponent<ArtefactBuff>() &&
                            hit.collider.GetComponent<ArtefactBuff>().UseArtefactGet != (int)Artefact.Pincet)
                        {
                            int useartefact = hit.collider.gameObject.GetComponent<ArtefactBuff>().delete;
                            for (int i = 0; i < artefact.Length; i++)
                            {
                                if (useartefact == (int)artefact[i])
                                {
                                    remaining[i]++;
                                    ReaningSet(i);
                                    ImageSet();
                                    break;
                                }
                            }
                        }
                    }
                    break;
                default:
                    if (!Physics.Raycast(ray, out hit)||(artefact[set]!= Artefact.Loupe&&hit.collider.tag=="walls"))
                    {
                        set = -1;
                        ImageSet();
                    }
                    else if(artefact[set] == Artefact.Loupe&& CloneLoupe == null)
                    {
                        remaining[set]=0;
                        CloneLoupe = GameObject.Instantiate(LoupeObj)as GameObject;
                        CloneLoupe.transform.position = hit.point;
                    }
                    else if (remaining[set] != -1&& Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("適用判定");
                        Debug.Log((int)artefact[set]);
                        if (artefact[set] != Artefact.Loupe&&hit.collider.GetComponent<ArtefactBuff>())
                        {
                            Debug.Log("既に存在していた為、重複作業を行います。");
                            int useartefact = hit.collider.gameObject.GetComponent<ArtefactBuff>().delete;
                            for (int i = 0; i < artefact.Length; i++)
                            {
                                if (useartefact == i)
                                {
                                    remaining[i]++;
                                    break;
                                }
                            }
                        }
                        ArtefactSetIf(hit.collider);
                        ImageSet();
                    }
                    break;
            }
        }
        if (CloneLoupe!=null&&Input.GetMouseButton(0))
        {
            Vector2 pos=TouchBeginPos - (Vector2)Input.mousePosition;
            pos /= 1000;
            CloneLoupe.transform.position = new Vector3(CloneLoupe.transform.position.x-pos.x, CloneLoupe.transform.position.y - pos.y,0);
        }
#elif UNITY_ANDROID
        for (int j = 0; j < Input.touchCount; j++)
        {
            touch = Input.GetTouch(j);
            Vector2 click = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(click);
            hit = new RaycastHit();
            if (touch.phase == TouchPhase.Began)
            {
                TouchBeginPos = touch.position;
                switch (set)
                {
                    case -1:
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.GetComponent<ArtefactBuff>() &&
                                hit.collider.GetComponent<ArtefactBuff>().UseArtefactGet != (int)Artefact.Pincet)
                            {
                                int useartefact = hit.collider.gameObject.GetComponent<ArtefactBuff>().delete;
                                for (int i = 0; i < artefact.Length; i++)
                                {
                                    if (useartefact == (int)artefact[i])
                                    {
                                        remaining[i]++;
                                        ReaningSet(i);
                                        ImageSet();
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        if (!Physics.Raycast(ray, out hit) || (artefact[set] != Artefact.Loupe && hit.collider.tag == "walls"))
                        {
                            set = -1;
                            ImageSet();
                        }
                        else if (artefact[set] == Artefact.Loupe && CloneLoupe == null)
                        {
                            remaining[set] = 0;
                            CloneLoupe = GameObject.Instantiate(LoupeObj) as GameObject;
                            CloneLoupe.transform.position = hit.point;
                        }
                        else if (remaining[set] != -1 && Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("適用判定");
                            Debug.Log((int)artefact[set]);
                            if (hit.collider.GetComponent<ArtefactBuff>())
                            {
                                Debug.Log("既に存在していた為、重複作業を行います。");
                                int useartefact = hit.collider.gameObject.GetComponent<ArtefactBuff>().delete;
                                for (int i = 0; i < artefact.Length; i++)
                                {
                                    if (useartefact == i)
                                    {
                                        remaining[i]++;
                                        break;
                                    }
                                }
                            }
                            ArtefactSetIf(hit.collider);
                            ImageSet();
                        }
                        break;
                }
            }
            if (CloneLoupe != null && touch.phase == TouchPhase.Moved||touch.phase == TouchPhase.Stationary)
            {
                Vector2 pos = TouchBeginPos - touch.position;
                pos /= 1000;
                CloneLoupe.transform.position = new Vector3(CloneLoupe.transform.position.x - pos.x, CloneLoupe.transform.position.y - pos.y, 0);
            }
        }
#endif
        //ここはフェーズが変わったときルーペが存在した場合、ズームを戻します。
        if (PhaseChange.GetMode)
        {
            for (int i = 0; i < artefact.Length; i++) {
                if (artefact[i] == Artefact.Loupe)
                {
                    if (remaining[i] == 0)
                    {
                        remaining[i] = 1;
                        set = -1;
                        CloneLoupe.GetComponent<lupe>().Reset();
                        Destroy(CloneLoupe);
                    }
                }
            }
        }
    }
    public void ButtonPush(int i)
    {
        Debug.Log((Artefact)artefact[i]);
        Debug.Log(remaining[i]);
        if (remaining[i] >= 1|| remaining[i] == -1){
            set = i;
        }
        else if((Artefact)artefact[i] == Artefact.Loupe){
            if (remaining[i] == 0){
                remaining[i] = 1;
                set = -1;
                CloneLoupe.GetComponent<lupe>().Reset();
                Destroy(CloneLoupe);
            }
        }
        ImageSet();
    }
    public int[] RemainingGet
    {
        get
        {
            return remaining;
        }
        set
        {
            remaining = value;
        }
    }
    public Artefact[] ArtefactGet
    {
        get
        {
            return artefact;
        }
    }
    private void ArtefactSetIf(Collider col)
    {
        ImageSet();
        switch (artefact[set])
        {
            ///
            /// ここに各アーティファクトの適用可能なオブジェクトかどうか判断します。
            /// やめるのだフェネック！スマホ・PCで2つ同じことをするならメソッド分けた方がいいと思ったのだ！
            /// 分けないと大変なのだ！パークの危機なのだ！
            ///
            case Artefact.Non:
                if (col.tag == "Untagged")
                {
                    NewArtefactSet();
                }
                break;
            case Artefact.Pincet:
                if (col.tag == "press"|| col.tag == "Magnet"|| col.tag == "Iron")
                {
                    NewArtefactSet();
                }
                break;
            case Artefact.Pipetto:
                if (col.tag == "Gear")
                {
                    NewArtefactSet();
                }
                break;
            default:
                break;
        }
    }
    private void NewArtefactSet()
    {
        //何度も行われるであろうアーティファクトを適用させる部分です。
        if (hit.collider.GetComponent<ArtefactBuff>())
        {
            
            int useartefact = hit.collider.gameObject.GetComponent<ArtefactBuff>().delete;
            for(int i = 0; i < artefact.Length; i++)
            {
                if ((int)artefact[i] == useartefact) remaining[i]++;
            }
        }
        if (artefact[set] == Artefact.Pincet) {
            outlinetrans=Instantiate(outlineobj, hit.transform);
            outlinetrans.transform.position = hit.transform.position + new Vector3(0, 0, -0.6f);
            outlinetrans.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        hit.collider.gameObject.AddComponent<ArtefactBuff>().Addition = (int)artefact[set];
        remaining[set]--;
        ReaningSet(set);
        set = -1;
    }
    public void ReaningSet(int id)
    {
        ButtonObj[id].GetComponentInChildren<Text>().text = remaining[id].ToString();
        ButtonObj[id].transform.Find("Image").GetComponent<Image>().sprite = UseArtefactImage[(int)artefact[id]];
    }
    private void ImageSet (){
        if (set >= 0)
        {
            UseArtefactObj.enabled = true;
            UseArtefactObj.sprite = UseArtefactImage[(int)artefact[set]];
        }
        else
        {
            UseArtefactObj.sprite = null;
            UseArtefactObj.enabled = false;
        }
    }
}
