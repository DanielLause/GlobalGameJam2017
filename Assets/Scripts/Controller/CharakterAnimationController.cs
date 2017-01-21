using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharakterAnimationController : MonoBehaviour
{
    private CharacterMovementController controller;
    private Animator anim;

    private void Awake()
    {
        controller = FindObjectOfType<CharacterMovementController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAnimationStates();
    }

    private void CheckAnimationStates()
    {
        if (controller.IsMoving)
        {
            if (controller.IsJumping)
            {
                anim.SetBool("walk", false);
                anim.SetBool("jump", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("walk", true);
                anim.SetBool("jump", false);
                anim.SetBool("idle", false);
            }
        }
        else if (controller.IsIdleing)
        {
            if (controller.IsJumping)
            {
                anim.SetBool("walk", false);
                anim.SetBool("jump", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("jump", false);
                anim.SetBool("walk", false);
                anim.SetBool("idle", true);
            }
        }
    }
}
