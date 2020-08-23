using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System;

public class Clockscript : MonoBehaviour
{
    Text txt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txt = gameObject.GetComponent<Text>();
        DateTime now = DateTime.Now;
        string nowto = now.ToString().Split(' ')[1];
        txt.text = nowto;
    }
}
