using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCondition : MonoBehaviour
{
    public bool dead;
    public float distance;
    public GameObject trig;
    public GameObject sinBin;
    public AllyAI otherScript;
    public EndCondition playerScript;
    public GameObject self;
    public Pathfinding.AIDestinationSetter aides;
    public Transform target;
    public string killReport;
    void Start()
    {
        dead = false;
        trig = GameObject.Find("Trigger");
        self = gameObject;
        sinBin = GameObject.Find("sinBin");
        aides = GetComponent<Pathfinding.AIDestinationSetter>();
        target = aides.target;
        playerScript = target.GetComponent<EndCondition>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ally") {
            dead = true;
            distance = getEuclideanDistance(target, transform);
            transform.position = sinBin.transform.position;

            killReport = this.name.ToString() + " | " + this.tag.ToString()
                + " | " + this.distance + "\n";

            tag = "Dead";
            otherScript = other.GetComponent<AllyAI>();
            otherScript.targeting = true;
        }
        else if (other.tag == "Player")
        {
            playerScript.dead = true;
            playerScript.killer = self;
            other.transform.position = sinBin.transform.position;
        }

    }

    float getEuclideanDistance(Transform toDefend, Transform target)
    {
        float euclid = Mathf.Pow(target.position[0] - toDefend.position[0], 2) + Mathf.Pow(target.position[1] - toDefend.position[1], 2);

        euclid = Mathf.Sqrt(euclid);

        return euclid;
    }


}
