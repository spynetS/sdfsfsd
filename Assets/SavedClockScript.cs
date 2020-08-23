using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System;

public class SavedClockScript : MonoBehaviour
{
    Text txt;
    void Start()
    {
        GetTime();
    }
    public void GetTime()
    {
        
        txt = gameObject.GetComponent<Text>();
        DateTime now = DateTime.Now;
        string nowto = now.ToString().Split(' ')[1];
        PlayerPrefs.SetString("SavedTime", nowto);
        txt.text = PlayerPrefs.GetString("SavedTime");
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
