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
        ResetBools();

        if (controller.IsMoving)
        {
            if (controller.IsFalling)
                anim.SetBool("fall", true);
            else if (controller.IsRising)
                anim.SetBool("rise", true);
            else
                anim.SetBool("walk", true);
        }
        else
        {
            if (controller.IsFalling)
                anim.SetBool("fall", true);
            else if (controller.IsRising)
                anim.SetBool("rise", true);
            else
                anim.SetBool("walk", false);
        }
    }

    private void ResetBools()
    {
        anim.SetBool("walk", false);
        anim.SetBool("rise", false);
        anim.SetBool("fall", false);
    }
}
