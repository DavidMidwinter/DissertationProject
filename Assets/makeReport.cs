    using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class makeReport : MonoBehaviour
{
    public string report;
    public List<GameObject> enemies;
    private GameObject[] entities;
    public int nEnemies = 0;
    public KillCondition human;
    public bool reported;
    public DateTime testTime;


    // Start is called before the first frame update
    void Start()
    {
        testTime = DateTime.Now;
        enemies = new List<GameObject>();
        reported = false;
        findEnemies("Robot");
        findEnemies("Human");
        nEnemies = enemies.ToArray().Length;
        enemies.Clear();
        human = GameObject.FindGameObjectWithTag("Player").GetComponent<KillCondition>();
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (testEntities("Dead").Length == nEnemies && !reported)
        {
            
            MakeReport("SUCCESSFUL");
        }
    }

    void findEnemies(string tag)
    {
        entities = testEntities(tag);
        if (entities.Length>=1)
        {
            foreach (GameObject i in entities)
                enemies.Add(i);
        }
    }

    GameObject[] testEntities(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag);
    }

    

    public void MakeReport(string success)
    {
        reported = true;
        findEnemies("Dead");
        report = "Test Report, Time: "+ testTime + "\n" +
            "Test status: " + success + "\n" +
            "Number of Enemies: " + nEnemies + "\n\n" +
            "Enemy Name | Type | Distance from Human when eliminated\n";
        foreach (GameObject i in entities) {
            report = report + i.GetComponent<KillCondition>().killReport;
        }
        string path = "Reports/Report" + testTime.ToString("_ddMMyyyy_HHmmss") + ".txt";
        File.WriteAllText(path, report);

        Application.Quit();
    }
}
