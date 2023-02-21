using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject DeathUI;

    // create a reference to our thirst bar
    public thirstbar thirstbar;

    public Vector3 lastCheckPoint = new Vector3 (0,0,0);

    public bool inHole = false;
    public bool isDead = false;

    public ParticleSystem currentHoleDust;

    // The current level of thirst, from 0 (completely thirsty) to 1 (fully hydrated)
    public float thirst = 1f;

    // The rate at which the thirst bar decreases while walking, in units of thirst per second
    public float walkThirstLossRate = 0.005f;

    // The rate at which the thirst bar decreases while running, in units of thirst per second
    public float runThirstLossRate = 0.002f;

    // The amount of thirst that is restored when drinking from a tiny hole of water, as a percentage of the total thirst bar
    public float tinyHoleHydrationAmount = 1.0f;

    // The amount of thirst that is restored when drinking from an oasis, as a percentage of the total thirst bar
    public float oasisHydrationAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        thirstbar.SetMaxthirst(thirst);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the elapsed time since the last frame
        float elapsedTime = Time.deltaTime;

        // Calculate the thirst loss rate based on whether the character is walking or running
        float thirstLossRate = Input.GetKey(KeyCode.LeftShift) ? runThirstLossRate : walkThirstLossRate;

        // Decrease the thirst level by the appropriate amount
        thirst = Mathf.Max(thirst - thirstLossRate * elapsedTime, 0f);
        thirstbar.SetThirst(thirst);

        if (thirst <= 0)
        {
            die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            currentHoleDust = collision.transform.parent.GetComponentInChildren<ParticleSystem>();
            inHole = true;
        }

        if (collision.CompareTag("TinyHoleOfWater"))
        {
            // Restore the thirst level by the appropriate amount
            thirst = Mathf.Min(thirst + tinyHoleHydrationAmount, 1f);
            thirstbar.SetThirst(thirst);
        }
        if (collision.CompareTag("Oasis"))
        {
            // Restore the thirst level by the appropriate amount
            thirst = Mathf.Min(thirst + oasisHydrationAmount, 1f);
            thirstbar.SetThirst(thirst);
            //lastCheckPoint = collision.transform.position;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            inHole = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster")
            die();
        
    }

    private void die()
    {
        isDead = true;
        DeathUI.gameObject.SetActive(true);
    }

    //transform.position = lastCheckPoint;
}
