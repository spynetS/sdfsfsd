using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class listscript : MonoBehaviour
{

    [SerializeField] GameObject buttonPref;
    public GameObject panal;
    public Text text;
    public Transform content;
    int lenght = 10;


    // Start is called before the first frame update
    void Start()
    {
        string[] lines = File.ReadAllLines(Application.persistentDataPath + "/Dates.txt");
        foreach (string line in lines)
        {
            GameObject button = (GameObject)Instantiate(buttonPref);
            button.GetComponentInChildren<Text>().text = line;

            button.transform.parent = content;
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(
                () => { Click(button.GetComponentInChildren<Text>().text.ToString()); });
        }


    }
    void Click(String button)
    {
        int hoursworked;
        string moneyearned;
        hoursworked = int.Parse(File.ReadAllText(Application.persistentDataPath+"/"+button+"_Houres.txt"));
        double salary =  double.Parse(File.ReadAllText(Application.persistentDataPath + "/Salary.txt"));
        double tax =  double.Parse(File.ReadAllText(Application.persistentDataPath + "/Tax.txt"));
        moneyearned = ((salary * (hoursworked / 60 / 60)) * tax).ToString();

        int secunds;
        int hours;
        int minutes;

        hours = hoursworked / 60 / 60;
        minutes = (hoursworked - (hours * 60 * 60)) / 60;
        secunds = hoursworked - (hours * 60 * 60) - (minutes * 60);



        text.text = "You have worked for " + hours + ":" + minutes + ":" + secunds+"\r\n and you earned "+((salary*hoursworked/60/60)*tax).ToString();

        
    }

    public void newScale(GameObject theGameObject, float newSize)
    {

        float size = theGameObject.GetComponent<Renderer>().bounds.size.y;

        Vector3 rescale = theGameObject.transform.localScale;

        rescale.y = newSize * rescale.y / size;

        theGameObject.transform.localScale = rescale;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
