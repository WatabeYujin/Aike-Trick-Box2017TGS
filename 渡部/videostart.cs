using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class videostart : MonoBehaviour {
    Result result;
    VideoPlayer video;
    RawImage image;
    float time=0;
    bool push=false,videofinish=false;
    [SerializeField]
    Image skipimage,loop;
	void Start () {
        image = GetComponent<RawImage>();
        video = GetComponent<VideoPlayer>();
        if (video.clip != null)
        {
            image.enabled = true;
            video.Play();
        }
        else videoend();
    }
    public void Loop()
    {
        videofinish = false;
        video.frame = 0;
        loop.enabled = false;
        video.Play();
    }
    public void OnPointerDown()
    {
        if (videofinish) videoend();
        push = true;
    }
    public void OnPointerUp()
    {
        skipimage.fillAmount =0;
        time = 0;
        push = false;
    }
    // Update is called once per frame
    void videoend()
    {
        Debug.Log("動画終了");
        image.enabled = false;
        skipimage.enabled = false;
        GameObject.Find("playerset").GetComponent<Result>().countchack(true);
        Destroy(gameObject);
    }
    void Update () {
        if (push)
        {
            time += Time.deltaTime;
            skipimage.fillAmount = time / 1;
            if (time >= 1)
            {
                videoend();
            }
        }
        if ((ulong)video.frame == video.frameCount&&!videofinish)
        {
            video.frame = 0;
            video.Stop();
            videofinish = true;
            loop.enabled = true;
            Debug.Log("a");
        }
    }
}
