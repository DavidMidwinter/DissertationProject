using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    public bool dead;
    public GameObject killer;
    public string killReport;
    public bool reported;
    public makeReport reporter;
    void Start()
    {
        dead = false;
        killer = null;
        reported = false;
        reporter = GameObject.Find("ReportMaker").GetComponent<makeReport>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true && reported == false)
        {
            killReport = "FAILURE: Human was eliminated by: " + killer.name.ToString() + ", which was a " + killer.tag.ToString();
            reporter.MakeReport(killReport);
            reported = true;
        }
    }
}
