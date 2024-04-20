using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMovingsen : MonoBehaviour
{
    private Animator animator;
    private StaminaInterface staminaScript; // Reference to the StaminaInterface script

    void Start()
    {
        // Get the Animator component for the current object
        animator = GetComponent<Animator>();

        // Find the object with the StaminaInterface component
        GameObject staminaObject = GameObject.Find("HEROSPACE5");

        if (staminaObject != null)
        {
            // Get the StaminaInterface component from the found object
            staminaScript = staminaObject.GetComponent<StaminaInterface>();
        }

        if (staminaScript == null)
        {
            Debug.LogError("Unable to find StaminaInterface component!");
        }
    }

    void Update()
    {
        if (staminaScript != null)
        {
            // Get the current Stamina value
            float staminaValue = staminaScript.Stamina;

            // Check if Stamina value is 0
            if (staminaValue <= 0)
            {
                // If Stamina is 0 or less, disable all animations
                animator.SetBool("SprintBOOL", false);
            }
            else
            {
                // If Stamina is greater than 0, set animation states according to current conditions
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 moveDirection = new Vector3(0, verticalInput, horizontalInput);

                if (moveDirection != Vector3.zero)
                {
                    if (horizontalInput > 0 && transform.rotation.eulerAngles.y < 10)
                    {
                        animator.SetBool("walk", true);
                    }
                    else if (horizontalInput < 0 && transform.rotation.eulerAngles.y > 170)
                    {
                        animator.SetBool("walk", true);
                    }
                    else
                    {
                        animator.SetBool("walk", false);
                    }

                    if (Input.GetKey(KeyCode.LeftShift) && horizontalInput > 0 && transform.rotation.eulerAngles.y < 10 && moveDirection != Vector3.zero)
                    {
                        animator.SetBool("SprintBOOL", true);
                    }
                    else if (Input.GetKey(KeyCode.LeftShift) && horizontalInput < 0 && transform.rotation.eulerAngles.y > 170 && moveDirection != Vector3.zero)
                    {
                        animator.SetBool("SprintBOOL", true);
                    }
                    else
                    {
                        animator.SetBool("SprintBOOL", false);
                    }
                }
                else
                {
                    animator.SetBool("SprintBOOL", false);
                    animator.SetBool("walk", false);
                }
            }
        }
        else
        {
            Debug.LogError("Unable to find StaminaInterface component!");
        }

        // The rest of your UpMovings script...
    }
}