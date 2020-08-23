using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishscreen : MonoBehaviour
{

    public GameObject FinishPanel;
    public GameObject TimerPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Finish()
    {

        FinishPanel.SetActive(true);
        TimerPanel.SetActive(false);
        
    }
    public void Continue()
    {
        FinishPanel.SetActive(false);
        TimerPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
