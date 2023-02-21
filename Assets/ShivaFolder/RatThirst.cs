using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatThirst : MonoBehaviour
{
    // create a reference to our thirst bar
    public thirstbar thirstbar;

    // The speed at which the rat moves
    public float speed = 2f;

    // The speed multiplier for when the space bar is pressed
    public float speedMultiplier = 1.25f;

    // The current level of thirst, from 0 (completely thirsty) to 1 (fully hydrated)
    public float thirst = 0.01f;

    // The rate at which the thirst bar decreases while walking, in units of thirst per second
    public float walkThirstLossRate = 0.01f;

    // The rate at which the thirst bar decreases while running, in units of thirst per second
    public float runThirstLossRate = 0.125f;

    // The amount of thirst that is restored when drinking from a tiny hole of water, as a percentage of the total thirst bar
    public float tinyHoleHydrationAmount = 0.05f;

    // The amount of thirst that is restored when drinking from an oasis, as a percentage of the total thirst bar
    public float oasisHydrationAmount = 1f;
    private float _speed = 0.01f;

    private void Start()
    {
        thirstbar.SetMaxthirst(thirst);
    }

    private void Update()
    {
        // Calculate the elapsed time since the last frame
        float elapsedTime = Time.deltaTime * _speed;

        // Decrease the thirst level by the appropriate amount
        thirst = Mathf.Max(thirst * elapsedTime, 0f);
        thirstbar.SetThirst(thirst);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("TinyHoleOfWater"))
        {
            // Restore the thirst level by the appropriate amount
            thirst = Mathf.Min(thirst + tinyHoleHydrationAmount, 1f);
            thirstbar.SetThirst(thirst);
        }
        else if (other.CompareTag("Oasis"))
        {
            // Restore the thirst level by the appropriate amount
            thirst = Mathf.Min(thirst + oasisHydrationAmount, 1f);
            thirstbar.SetThirst(thirst);
        }
    }
}
