using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAnimation : MonoBehaviour
{
    public Animator animator;
    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        if (moveX == 0 && moveY == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attacking");
        }




    }
}
