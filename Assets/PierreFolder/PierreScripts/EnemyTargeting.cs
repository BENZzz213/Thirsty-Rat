using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;    
    private Vector2 spawnPosition;

    public WaypointFollower waypoints;
    public bool iAmBird;

    //GameObjectRotationTorward
    public float rotationModifier;
    private Vector3 spawnRotation; 
       
    

    private void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.position;        


        if (waypoints != null)
        {
            waypoints.enabled = true;
            Debug.Log("WaypointACTIVATED");
        }
            
    }

    private void Update()
    {

        float step = speed * Time.deltaTime;

        Vector2 myPosition = gameObject.transform.position;

        // target players
        if (target != null)
        {
            //move torward player
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);

            //rotatetorward player
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, step);

        }
        else if(!target && myPosition != spawnPosition ) 
        {
            if(waypoints != null && waypoints.enabled == false)
            {
                //Debug.Log("ReturnHome");
                transform.position = Vector2.MoveTowards(transform.position, spawnPosition, step);       //Return to spawn position

                //rotate toward spawn
                Vector3 vectorToSpawn = spawnRotation - transform.position;
                float angle = Mathf.Atan2(vectorToSpawn.y, vectorToSpawn.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, step);
            }

            if (waypoints == null)
            {
                //Debug.Log("ReturnHome");
                transform.position = Vector2.MoveTowards(transform.position, spawnPosition, step);       //Return to spawn position

                //rotate toward spawn
                Vector3 vectorToSpawn = spawnRotation - transform.position;
                float angle = Mathf.Atan2(vectorToSpawn.y, vectorToSpawn.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, step);
            }            
        }       

        if (myPosition == spawnPosition && waypoints != null )
        {
            waypoints.enabled = true;
        }
    }




    // target player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "FlashLight")
        {
            target = other.transform.parent;  //targettheplayer         
            
            if (waypoints != null)
            {
                waypoints.enabled = false;
            }
                

            //Debug.Log("Player!");
        }

        if (iAmBird)
        {
            if (other.gameObject.tag == "HideZone")
            {
                target = null;     // untarget the player                                

                //Debug.Log("HideZoneTrigger");
            }
        }         
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (iAmBird)
        {
            if (other.gameObject.tag == "HideZone")
            {
                target = null;     // untarget the player                                

                //Debug.Log("HideZoneTrigger");
            }
        }
    }

    
   
}
