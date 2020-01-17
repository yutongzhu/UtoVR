using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveSlider : MonoBehaviour {

    public Text StartTime;
    public Text CurrentTime;
    private int hour;
    private int minute;
    private int second;
    public MediaPlayerCtrl m_srcVideo;
    public Slider m_srcSlider;
    void Start () {
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        StartTime.text = string.Format("{0:D2}:{1:D2}:{2:D2} ", hour, minute, second);

        InvokeRepeating("GetTotal", 10,10);
    }
	
	// Update is called once per frame
	void Update () {
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        CurrentTime.text = string.Format("{0:D2}:{1:D2}:{2:D2} ", hour, minute, second);
     

    }
    void GetTotal()
    {
        Debug .Log("SeekTo");
        m_srcVideo.SeekTo(1);
       // Debug.Log("GetDuration:" + m_srcVideo.SeekTo(0));
    }
}
