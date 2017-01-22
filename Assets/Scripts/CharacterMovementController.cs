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
    [HideInInspector]
    public bool IsMoving;
    [HideInInspector]
    public bool IsRising;
    [HideInInspector]
    public bool IsFalling;
    [HideInInspector]
    public bool IsIdleing;

    public float RotationSpeed = 2;
    public float MaxSpeed = 5f;
    public float Speed = 0.2f;
    public float JumpSpeed = 8;
    public float InAirMovementMultipliar = 0.5f;
    public float Gravity = 9.8f;
    public float PlayerPivitToGroundDistance = 1.5f;

    [Header("Plattform Collider Settings")]
    public Transform FootPosition;

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

        if (Mathf.Abs(velocity.x) >= 0.5f)
        {
            IsMoving = true;
            IsIdleing = false;
        }
        else
        {
            IsMoving = false;
            IsIdleing = true;
        }

        if (controller.isGrounded)
        {
            velocitySpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space) && CanMove)
                velocitySpeed = JumpSpeed;
        }

        if (velocitySpeed >= 0.5f)
        {
            IsMoving = false;
            IsIdleing = false;
            IsRising = true;
        }
        else if (velocitySpeed <= -0.5f)
        {
            IsMoving = false;
            IsIdleing = false;
            IsFalling = true;
        }
        else
        {
            IsFalling = false;
            IsRising = false;
        }

        print(IsFalling + ": is falling");
        print(IsRising + ": is ´rising");

        velocitySpeed -= Gravity * Time.deltaTime;
        velocity.y = velocitySpeed;

        if (controller.isGrounded)
            controller.Move(velocity * Time.deltaTime);
        else
            controller.Move((new Vector3(velocity.x * InAirMovementMultipliar, velocity.y, velocity.z)) * Time.deltaTime);

        FixPosition();
        ControllRotation();
    }

    private void FixPosition()
    {
        Vector3 fixedPos = transform.position;
        fixedPos.z = -0.5f;
        transform.position = fixedPos;
    }

    private void ControllRotation()
    {
        Vector3 currentAngle = transform.eulerAngles;

        if (velocity.x > 0)
        {
            currentAngle = new Vector3( 0, Mathf.LerpAngle(currentAngle.y, 180, Time.deltaTime * RotationSpeed), 0);
            transform.eulerAngles = currentAngle;
        }
        else
        {
            currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, 0, Time.deltaTime * RotationSpeed), 0);
            transform.eulerAngles = currentAngle;
        }
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
