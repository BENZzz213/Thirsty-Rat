using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // store a reference to the waypoint system this object will use
    [SerializeField] private WaypointsManager waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceThreshold = 0.1f;



    //current waypoint we are moving to
    private Transform currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        // set initial position on start waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
        transform.rotation = currentWaypoint.rotation;

        //set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.rotation = currentWaypoint.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.stopTank == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                Debug.Log("target next waypoint");
                currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
                transform.rotation = currentWaypoint.rotation;
            }
        }

    }
}
