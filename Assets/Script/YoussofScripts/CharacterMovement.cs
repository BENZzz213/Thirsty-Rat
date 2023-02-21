using System;
using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets._2D
{
    public class CharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float runBoost = 1f;

        public Rigidbody2D rb;
        public Animator animator;

        public bool isHiding = false;
        bool isMoving = false;
        bool isRunning = false;

        public Player player;

        Vector2 movement;

        private void Update()
        {
            

            if (!player.isDead)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isHiding)
                    {
                        holeAnimation(false);
                    }
                    else if (player.inHole)
                    {
                        if(isRunning)
                        {
                            isRunning = false;
                            transform.GetComponentInChildren<ParticleSystem>().Stop();
                            runBoost = 1f;
                        }
                        holeAnimation(true);
                    }
                }

                if (!isHiding)
                {

                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");


                    if (movement.x != 0 || movement.y != 0)
                    {
                        animator.SetFloat("Horizontal", movement.x);
                        animator.SetFloat("Vertical", movement.y);
                        if (!isMoving)
                        {
                            isMoving = true;
                        }
                    }
                    else
                    {
                        if (isMoving)
                        {
                            isMoving = false;
                        }
                    }




                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        isRunning = true;
                        transform.GetComponentInChildren<ParticleSystem>().Play();
                        runBoost = 2f;
                    }

                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        isRunning = false;
                        transform.GetComponentInChildren<ParticleSystem>().Stop();
                        runBoost = 1f;
                    }

                    animator.SetBool("IsMoving", isMoving);
                    animator.SetBool("IsRunning", isRunning);
                }
            } 
        }


        private void FixedUpdate()
        {
            movement.Normalize();
            if (!isHiding && !player.isDead)
                rb.MovePosition(rb.position + movement * moveSpeed * runBoost * Time.fixedDeltaTime);
        }


        private void holeAnimation (bool newIsHiding)
        {
            isHiding = newIsHiding;
            animator.SetBool("IsHiding", isHiding);
            player.currentHoleDust.Play();
        }


    }
}
