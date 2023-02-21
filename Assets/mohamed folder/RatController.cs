using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{

    // create a reference to our thirst bar
    public thirstbar thirstbar;

    // The speed at which the rat moves
    public float speed = 2f;

    // The speed multiplier for when the space bar is pressed
    public float speedMultiplier = 1.25f;

    // The current level of thirst, from 0 (completely thirsty) to 1 (fully hydrated)
    public float thirst = 1f;

    // The rate at which the thirst bar decreases while walking, in units of thirst per second
    public float walkThirstLossRate = 0.1f;

    // The rate at which the thirst bar decreases while running, in units of thirst per second
    public float runThirstLossRate = 0.125f;

    // The amount of thirst that is restored when drinking from a tiny hole of water, as a percentage of the total thirst bar
    public float tinyHoleHydrationAmount = 0.05f;

    // The amount of thirst that is restored when drinking from an oasis, as a percentage of the total thirst bar
    public float oasisHydrationAmount = 1f;

    private void Start() 
    {
        thirstbar.SetMaxthirst(thirst);
    }

    private void Update()
    {
        // Calculate the elapsed time since the last frame
        float elapsedTime = Time.deltaTime;

        // Get the horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector based on the input values
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector, so that the rat doesn't move faster when moving diagonally
        movement.Normalize();

        // Calculate the thirst loss rate based on whether the character is walking or running
        float thirstLossRate = Input.GetKey(KeyCode.Space) ? runThirstLossRate : walkThirstLossRate;

        // Decrease the thirst level by the appropriate amount
        thirst = Mathf.Max(thirst - thirstLossRate * elapsedTime, 0f);
        thirstbar.SetThirst(thirst);
        
        // Apply the speed multiplier if the space bar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            movement *= speedMultiplier;
        }

        // Rotate the rat to face the direction it is moving
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        transform.position += (Vector3)movement * speed * Time.deltaTime;

    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
            // Move the rat and check for collisions with the oasis and tiny holes of water
            //RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, movement, movement.magnitude * Time.deltaTime, LayerMask.GetMask("Oasis", "TinyHoleOfWater"));
            //if (hit)
            
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

