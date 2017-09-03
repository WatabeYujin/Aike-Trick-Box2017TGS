using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balanceOnCol : MonoBehaviour
{
    [SerializeField]
    private GameObject _balCon;

    private bool isMove = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        balOperate BalO = _balCon.GetComponent<balOperate>();
        if (isMove)
        {
            if (this.gameObject.CompareTag("BalA"))
            {
                transform.SetParent(this.transform);
                BalO.move();
            }
            else if (this.gameObject.CompareTag("BalB"))
            {
                BalO.move2();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //transform.SetParent(null);
            isMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iTween.Stop();
            isMove = false;
        }
    }
}