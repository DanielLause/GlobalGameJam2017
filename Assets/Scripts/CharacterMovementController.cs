using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovementController : MonoBehaviour
{
    [HideInInspector]
    public bool CanMove = true;

    public float MaxSpeed = 5f;
    public float Speed = 0.2f;
    public float JumpSpeed = 8;
    public float InAirMovementMultipliar = 0.5f;
    public float Gravity = 9.8f;
    public float PlayerPivitToGroundDistance = 1.5f;

    private CharacterController controller;
    private Vector3 velocity = new Vector3();
    private float velocitySpeed = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameStateController.Instance.PausedState == PausedStates.Paused) return;

        if (Input.GetKey(KeyCode.A) && CanMove)
            velocity.x = Mathf.Lerp(velocity.x, -MaxSpeed, Speed);
        else if (Input.GetKey(KeyCode.D) && CanMove)
            velocity.x = Mathf.Lerp(velocity.x, MaxSpeed, Speed);
        else
            velocity.x = Mathf.Lerp(velocity.x, 0, Speed * 3);

        if (controller.isGrounded)
        {
            velocitySpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space) && CanMove)
                velocitySpeed = JumpSpeed;
        }

        velocitySpeed -= Gravity * Time.deltaTime;
        velocity.y = velocitySpeed;

        if (controller.isGrounded)
            controller.Move(velocity * Time.deltaTime);
        else
            controller.Move((new Vector3(velocity.x * InAirMovementMultipliar, velocity.y, velocity.z)) * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.transform.position, Vector2.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 10);

        for (int i = 0; i < hits.Length; i++)
        {
            print(Vector3.Distance(transform.position, hits[i].point));
            if (Vector3.Distance(transform.position, hits[i].point) <= PlayerPivitToGroundDistance)
                return true;
        }

        return false;
    }
}
