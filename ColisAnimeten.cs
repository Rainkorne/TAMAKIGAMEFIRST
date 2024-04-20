using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisAnimeten : MonoBehaviour
{     
    public Animator animator; // Reference to the Animator component
    public string animationBoolName; // Name of the bool parameter for animation

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OtherObject")) // Check collision with a specific object
        {
            animator.SetBool(animationBoolName, true); // Set the animator's bool parameter to true
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("OtherObject")) // Check collision with a specific object
        {
            animator.SetBool(animationBoolName, false); // Reset the animator's bool parameter to false
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OtherObject")) // Check collision with a specific object
        {
            animator.SetBool(animationBoolName, true); // Set the animator's bool parameter to true
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OtherObject")) // Check collision with a specific object
        {
            animator.SetBool(animationBoolName, false); // Reset the animator's bool parameter to false
        }
    }
}