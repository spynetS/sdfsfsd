using System.Collections;
using System.Collections.Generic;
using UnityEngine;using System;
using UnityEngine.UI;
using System.Runtime.ExceptionServices;
using System.IO;
public class MAIN : MonoBehaviour
{
    int currentnumberofpauses;
    public GameObject EndButton;
    public GameObject SAVEDCLOCK;
    public GameObject PAUSEMENU;
    public GameObject TimerCanvas;
    public GameObject SessionsCanvas;
    public GameObject Scrollview;
    public Text scrolltext;

    void Start()
    {
        currentnumberofpauses = PlayerPrefs.GetInt("currentnumberofpauses");
        int startid = PlayerPrefs.GetInt("StartOrEnd");
        if (startid == 1)
        {
           // StartSession();
            SAVEDCLOCK.SetActive(true);

        }
        else
        {
            EndButton.SetActive(false);
            PlayerPrefs.SetInt("currentnumberofpauses", 0);
        }
    }
    string time = DateTime.Now.ToString().Split(' ')[1];
    public void StartSession()
    {

        PlayerPrefs.SetInt("StartOrEnd", 1);
        PlayerPrefs.SetString("StartTime", time);
        EndButton.SetActive(true);
        SAVEDCLOCK.SetActive(true);
    }

    public void PauseSession()
    {
        PAUSEMENU.SetActive(true);
        PlayerPrefs.SetString("EndTime", DateTime.Now.ToString().Split(' ')[1]);
        currentnumberofpauses++;
        CreateParts(false);

    }
    public void PauseSessionOff()
    {
        PAUSEMENU.SetActive(false);
        PlayerPrefs.SetString("StartTime", DateTime.Now.ToString().Split(' ')[1]);

    }
    public void EndSession()
    {
        EndButton.SetActive(false);
        PlayerPrefs.SetInt("StartOrEnd", 0);
        PlayerPrefs.SetInt("currentnumberofpauses", 0);
        PlayerPrefs.SetString("EndTime", DateTime.Now.ToString().Split(' ')[1]);
        CreateParts(true);

    }

    void CreateParts(bool last)
    {
        string first = PlayerPrefs.GetString("StartTime");
        string secunds=first.Split(':')[2];
        string minutes = first.Split(':')[1];
        string hours= first.Split(':')[0];

        string stopsecunds = PlayerPrefs.GetString("EndTime").Split(':')[2];
        string stopminutes = PlayerPrefs.GetString("EndTime").Split(':')[1];
        string stophours = PlayerPrefs.GetString("EndTime").Split(':')[0];

        int secundsworked = Int32.Parse(stopsecunds) - Int32.Parse(secunds);
        int minutesworked = Int32.Parse(stopminutes) - Int32.Parse(minutes);
        int hoursworked = Int32.Parse(stophours) - Int32.Parse(hours);

        //Ska göra prefs som storar tiden ochh sedan lägger ihop dem när sessionen är slut och lägger in dem i en annan prefs
        double total = (secundsworked) + (minutesworked * 60) +hoursworked*60*60;
        

        
        
        if (last)
        {
           /// PlayerPrefs.SetString("WorkedDates", DateTime.Now.ToString().Split(':')[0] + " " + hoursworked.ToString() + " " + minutesworked.ToString() + " " + secundsworked.ToString() + ",");

        }

        Debug.Log(secundsworked.ToString()+" "+ minutesworked.ToString() + " " + hoursworked.ToString());
    }

    public void TimerButton()
    {
        SessionsCanvas.SetActive(false);
        TimerCanvas.SetActive(true);
        Debug.Log("asdasd");

    }
    public void SessionsButton()
    {
        TimerCanvas.SetActive(false);
        SessionsCanvas.SetActive(true);
        Debug.Log("asd");


    }

    void UpdateList()
    {
        int line = 5;
        for (int i = 0; i <=line;i++)
        { 
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
