using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.ExceptionServices;
using System.IO;
using UnityEditor;
using System.Linq;

public class Main2 : MonoBehaviour
{
    int currentnumberofpauses;
    public GameObject EndButton;
    public GameObject SAVEDCLOCK;
    public GameObject PAUSEMENU;
    public GameObject TimerCanvas;
    public GameObject SessionsCanvas;
    public GameObject Pausebutton;
    public GameObject Scrollview;
    public GameObject SettingsScreen;
    public GameObject Continue;
    public GameObject Overwrite;
    public Text scrolltext;
    public Text FinishText;
    public GameObject SureScreen;
    bool sure = false;
    double totalwork;
    double totalPause;
    string whole="nothing";
    int gLine;
    void Start()
    {
      



        currentnumberofpauses = PlayerPrefs.GetInt("currentnumberofpauses");
        int startid = PlayerPrefs.GetInt("StartOrEnd");
        if (startid == 1)
        {
            // StartSession();
        }
        else if (startid == 2)
        {
            PAUSEMENU.SetActive(true);
            EndButton.SetActive(true);
            EnablePause();
        }
        else
        {
            EnablePause();
            SAVEDCLOCK.SetActive(true);
            EndButton.SetActive(true);
        }

    }
    string time = DateTime.Now.ToString().Split(' ')[1];
    private string filePath;

    public void StartSession()
    {
         if (!File.Exists(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt"))
        {
            PlayerPrefs.SetInt("StartOrEnd", 0);
            PlayerPrefs.SetInt("currentnumberofpauses", PlayerPrefs.GetInt("currentnumberofpauses") + 1);

            using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt", true))
            {
                st.WriteLine("s," + DateTime.Now.ToString().Split(' ')[1]);
                st.Close();

            }
            using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/Dates.txt", true))
            {
                st.WriteLine(DateTime.Now.ToString().Split(' ')[0]);
                st.Close();

            }
            EnablePause();
            EndButton.SetActive(true);
            Continue.SetActive(true);
            Overwrite.SetActive(false);

        }
        else
        {
            Continue.SetActive(false);
            Overwrite.SetActive(true);
            if (sure) 
            {
                PlayerPrefs.SetInt("StartOrEnd", 0);
                PlayerPrefs.SetInt("currentnumberofpauses", PlayerPrefs.GetInt("currentnumberofpauses") + 1);

                File.Delete(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt");
                
                using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt", true))
                {
                    st.WriteLine("s," + DateTime.Now.ToString().Split(' ')[1]);
                    st.Close();

                }
               
                EnablePause();
                EndButton.SetActive(true);
                sure = false;
            }  
        }
    }
    public Text salaryInput;
    public Text taxInput;
    public void Salary()
    {
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Salary.txt"))
        {
            sw.Write(salaryInput.text);
            
        }
    }
    public void Tax()
    {
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Tax.txt"))
        {
            sw.Write(taxInput.text);

        }
    }
    public void Settings()
    {
        TimerCanvas.SetActive(false);
        SessionsCanvas.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    public void PauseSession()
    {
        PAUSEMENU.SetActive(true);
        currentnumberofpauses++;
        PlayerPrefs.SetInt("currentnumberofpauses", PlayerPrefs.GetInt("currentnumberofpauses") + 1);


        using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt", true))
        {
            st.WriteLine("p," + DateTime.Now.ToString().Split(' ')[1]);
            st.Close();

        }

        PlayerPrefs.SetInt("StartOrEnd", 2);

    }
    public void PauseSessionOff()
    {
        PlayerPrefs.SetInt("currentnumberofpauses", 0);

        PAUSEMENU.SetActive(false);

        using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt", true))
        {
            st.WriteLine("g," + DateTime.Now.ToString().Split(' ')[1]);
            st.Close();

        }


    }
    public void EnablePause()
    {
        Pausebutton.SetActive(true);

    }
    public void Deactivevate()
    {
        Pausebutton.SetActive(false);

    }


    public void EndSession()
    {
        Deactivevate();
        PlayerPrefs.SetInt("StartOrEnd", 1);
        EndButton.SetActive(false);

        using (StreamWriter st = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt", true))
        {
            st.WriteLine("e," + DateTime.Now.ToString().Split(' ')[1]);
            st.Close();

        }

        CreateParts(time);

    }

    void CreateParts(string last)
    {
        totalPause = 0;
        gLine = 0;


        string[] readText = File.ReadAllLines(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt");
        foreach (string s in readText)
        {
            gLine++;
            if (s.Contains("s"))
            {
                string[] readText2 = File.ReadAllLines(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt");
                foreach (string s2 in readText)
                {
                    if (s2.Contains("e"))
                    {
                        double hours = int.Parse(s2.Split(',')[1].Split(':')[0]) - int.Parse(s.Split(',')[1].Split(':')[0]);
                        double minutes = int.Parse(s2.Split(',')[1].Split(':')[1]) - int.Parse(s.Split(',')[1].Split(':')[1]);
                        double secunds = int.Parse(s2.Split(',')[1].Split(':')[2]) - int.Parse(s.Split(',')[1].Split(':')[2]);
                        totalwork = hours * 60 * 60 + minutes * 60 + secunds;
                    }
                }

            }
            //Loopen kör alla gn som redan har blivit körda i den s22 loopen.
            else if (s.Contains("p"))
            {

                string s22 = File.ReadLines(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + ".txt").Skip(gLine).Take(1).First();
                Debug.Log(s22);

                if (s22.Contains("g"))
                {
                    if (!whole.Contains(s22))
                    {
                        Debug.Log("s22 " + s22);
                        double hourss = int.Parse(s22.Split(',')[1].Split(':')[0]) - int.Parse(s.Split(',')[1].Split(':')[0]);
                        double minutess = int.Parse(s22.Split(',')[1].Split(':')[1]) - int.Parse(s.Split(',')[1].Split(':')[1]);
                        double secundss = int.Parse(s22.Split(',')[1].Split(':')[2]) - int.Parse(s.Split(',')[1].Split(':')[2]);
                        totalPause = totalPause + hourss * 60 * 60 + minutess * 60 + secundss;
                        whole = whole + s22;
                       
                    }
                }
            }
        }


        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + "_Houres.txt"))
        {
            sw.Write(totalwork - totalPause);
        }


        double salary = double.Parse(File.ReadAllText(Application.persistentDataPath+"/Salary.txt"));
        double tax = double.Parse(File.ReadAllText(Application.persistentDataPath+"/Tax.txt"));
        int hoursWorked = int.Parse(File.ReadAllText(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + "_Houres.txt"));
        double hoursworked = double.Parse(File.ReadAllText(Application.persistentDataPath + "/" + DateTime.Now.ToString().Split(' ')[0] + "_Houres.txt"));


        int Secunds;
        int Hours;
        int Minutes;

        Hours = hoursWorked / 60 / 60;
        Minutes = (hoursWorked - (Hours * 60 * 60)) / 60;
        Secunds = hoursWorked - (Hours * 60 * 60) - (Minutes * 60);



        //text.text = "You have worked for " + hours + ":" + minutes + ":" + secunds + "\r\n and you earned " + ((salary * hoursWorked / 60 / 60) * tax).ToString();


        FinishText.text = "You have worked " + Hours + ":" + Minutes + ":" + Secunds + " hours \r\n and you have made " + ((salary*(hoursworked / 60/60)) * tax).ToString()+" after tax";





        //Ska göra prefs som storar tiden ochh sedan lägger ihop dem när sessionen är slut och lägger in dem i en annan prefs
    }
    

    Text txt;
    public void TimerButton()
    {
        SessionsCanvas.SetActive(false);
        SettingsScreen.SetActive(false);
        TimerCanvas.SetActive(true);
        Debug.Log("asdasd");

    }
    public void SessionsButton()
    {
        SettingsScreen.SetActive(false);
        TimerCanvas.SetActive(false);
        SessionsCanvas.SetActive(true);
      

    }

    public void Sure()
    {
        sure = true;
        SureScreen.SetActive(false);
        StartSession();

    }
    public void No()
    {
        SureScreen.SetActive(false);

    }
    public void StartAgain()
    {
        SureScreen.SetActive(true);
    }


    void UpdateList()
    {
        int line = 5;
        for (int i = 0; i <= line; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
