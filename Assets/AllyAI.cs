using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AllyAI : MonoBehaviour
{
    public bool targeting;
    public Transform target;
    public List<Transform> targets;
    public Transform toDefend;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    private float lowestEuclideanDistance;
    private float EuclideanDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        toDefend = GameObject.Find("Human").transform;
        targeting = true;
        SelectTarget();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void UpdatePath()
    {
        if (targeting == true)
        {
            SelectTarget();
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;

    }

    void GetTargetList()
    {
        targets = new List<Transform>();
        GameObject[] entity;
        entity = GameObject.FindGameObjectsWithTag("Robot");
        if (entity.Length > 0)
        {
            foreach (GameObject i in entity)
                targets.Add(i.transform);
        }
        else
        {
            targets.Add(null);
        }


    }

    void SelectTarget()
    {

        GetTargetList();
        target = null;
        //SimpleTargetSelect();
        //EuclideanTargetSelect();
        ETSDefenceProt();
        
        targeting = false;
    }

    void SimpleTargetSelect()
    {
        if (targets[0])
        {
            target = targets[0];
        }
        else
        {
            target = null;
        }
    }

    void EuclideanTargetSelect()
    {
        lowestEuclideanDistance = 1000;
        for (int i = 0; i < targets.Count; i++)
        {
            EuclideanDistance = getEuclideanDistance(toDefend, targets[i]);
            if (EuclideanDistance < lowestEuclideanDistance)
            {
                lowestEuclideanDistance = EuclideanDistance;
                target = targets[i];
            }
        }
    }

    void ETSDefenceProt()
    {

        if (targets[0])
            EuclideanTargetSelect();
        else
            target = toDefend;  
    }


    float getEuclideanDistance(Transform toDefend, Transform target)
    {
        float euclid = Mathf.Pow(target.position[0]-toDefend.position[0],2) + Mathf.Pow(target.position[1] - toDefend.position[1], 2);
        
        euclid = Mathf.Sqrt(euclid);

        return euclid;
    }
}


