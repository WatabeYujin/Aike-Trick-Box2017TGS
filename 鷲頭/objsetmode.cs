using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objsetmode : MonoBehaviour {
    public int set;
    public GameObject moveobj;
    Camera camera;
    Ray ray;
	assetset Assetset;
    [SerializeField]
    private List<GameObject> asset=new List<GameObject>();
    [SerializeField]
    private List<Transform> button = new List<Transform>();
    [SerializeField]
    private List<int> Usagefrequency=new List<int>();
    private List<int> Usagefrequency2 = new List<int>();
    private RaycastHit hit;
    Touch touch;
    public static bool zFlag = false;

    void Start () {
        for(int i = 0;i< Usagefrequency.Count; i++)
        {
            Usagefrequency2.Add(0);
        }
        UsagefrequencySet();
        camera =GetComponent<Camera>();
		Assetset=GetComponent<assetset> ();
        
	}
    public void UsagefrequencyReset()
    {
        for (int i = 0; i < Usagefrequency2.Count; i++)
        {
            Usagefrequency2[i] = 0;
        }
    }
    // Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = new RaycastHit();
        //else if (touch.phase == TouchPhase.Moved)
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < Assetset.obj.Count; i++)
                {
                    if (Assetset.obj[i]!=null&&hit.transform == Assetset.obj[i].transform)
                    {
                        set = -1;
                        moveobj = Assetset.obj[i];
                        moveobj.layer = 2;
                        break;
                    }
                }
            }
            if (moveobj == null)
            {
                set = -2;
            }
        }
        if (Input.GetMouseButton(0))

        {
            if (Physics.Raycast(ray, out hit))
            {
                if (moveobj != null && hit.transform.tag == "walls")
                {
                    moveobj.SetActive(true);
                    moveobj.transform.position = hit.point + new Vector3(0, 0, moveobj.transform.lossyScale.z / -2);
                }
                else
                {
                    moveobj.SetActive(false);
                }
            }
            else if (moveobj != null)
            {
                moveobj.SetActive(false);
            }
         //   moveobj.position = Camera.main.ScreenToWorldPoint(click);
        }
        if (Input.GetMouseButtonUp(0)) {
            zFlag = true;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "walls")
                {
                    if (set >= 0)
                    {
                        moveobj.name = set.ToString();//何番目のアセットかわかるようにオブジェクトの名前を変更している
                        Usagefrequency2[set]++;
                        UsagefrequencySet();
                        Assetset.NewAssetSet = moveobj;
                        if (moveobj.GetComponent<Rigidbody>()) moveobj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        moveobj.layer = 0;
                        moveobj = null;
                        set = -2;
                    }
                    else if (set == -1)
                    {
                        Assetset.ChangeAssetSet = moveobj;
                        if (moveobj.GetComponent<Rigidbody>()) moveobj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        moveobj.layer = 0;
                        moveobj = null;
                    }

                }else
                {
                    if (set >= 0)
                    {
                        //Usagefrequency2[set]++;
                        //UsagefrequencySet();
                        Destroy(moveobj);
                        moveobj = null;
                    }
                    else if (set == -1)
                    {
                        Usagefrequency2[int.Parse(moveobj.transform.name)]--;
                        UsagefrequencySet();
                        Destroy(moveobj);
                        moveobj = null;
                    }
                }
            }
            else
            {
                if (set >= 0)
                {
                    //Usagefrequency2[set]++;
                    //UsagefrequencySet();
                    Destroy(moveobj);
                    moveobj = null;
                }
                else if (set == -1)
                {
                    Usagefrequency2[int.Parse(moveobj.transform.name)]--;
                    UsagefrequencySet();
                    Destroy(moveobj);
                    moveobj = null;
                }
            }
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
                if (Physics.Raycast(ray, out hit))
                {
                    for (int i = 0; i < Assetset.obj.Count; i++)
                    {
                        if (Assetset.obj[i]!=null&&hit.transform == Assetset.obj[i].transform)
                        {
                            Debug.Log("変更");
                            set = -1;
                            moveobj = Assetset.obj[i];
                            moveobj.layer = 2;
                            break;
                        }
                    }
                }
                if (moveobj == null)
                {
                    set = -2;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (moveobj != null && hit.transform.tag == "walls")
                    {
                        moveobj.SetActive(true);
                        moveobj.transform.position = hit.point;
                    }
                }
                else if(moveobj!=null)
                {
                    moveobj.SetActive(false);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag == "walls")
                    {
                        if (set >= 0)
                        {
                            moveobj.name = set.ToString();//何番目のアセットかわかるようにオブジェクトの名前を変更している
                            Usagefrequency2[set]++;
                            UsagefrequencySet();
                            Assetset.NewAssetSet = moveobj;
                            if (moveobj.GetComponent<Rigidbody>()) moveobj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                            moveobj.layer = 0;
                            moveobj = null;
                            set = -2;
                        }
                        else if (set == -1)
                        {
                            Assetset.ChangeAssetSet = moveobj;
                            if (moveobj.GetComponent<Rigidbody>()) moveobj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                            moveobj.layer = 0;
                            moveobj = null;
                        }
                    
                    }
                }
                else
                {
                    if (set >= 0)
                    {
                        //Usagefrequency2[set]++;
                        //UsagefrequencySet();
                        Destroy(moveobj);
                        moveobj = null;
                    }
                    else if (set == -1)
                    {
                        Usagefrequency2[int.Parse(moveobj.transform.name)]--;
                        UsagefrequencySet();
                        Destroy(moveobj);
                        moveobj = null;
                    }
                }
            }
        }
#endif
    }
    public void objectchack(int i)
    {
        if (Usagefrequency[i] > Usagefrequency2[i])
        {
            set = i;
            moveobj = GameObject.Instantiate(asset[i]);
            moveobj.layer = 2;
        }
    }
    void UsagefrequencySet()
    {
        for (int i = 0; i < asset.Count; i++) {
            button[i].GetComponentInChildren<Text>().text = (Usagefrequency[i] - Usagefrequency2[i]).ToString();
        }
    }
}
