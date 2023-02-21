using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTooFar : MonoBehaviour
{
    public EnemyTargeting enemyScript;
    
    



    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            enemyScript.target = null;
           
        }
    }


    

}
