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

<<<<<<< HEAD:Assets/Scripts/CharacterMovementController.cs
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float inAirControl = 0.1f;
    [SerializeField] private float jumpHeight = 1.7f;
    [SerializeField] private float maxVelocityChange = 10.0f;
    private float walkBackwardSpeed = 4.0f;
    private float runBackwardSpeed = 6.0f;
    private float sidestepSpeed = 8.0f;
    private float runSidestepSpeed = 12.0f;

    private bool grounded = false;
    private Vector3 groundVelocity;
    private CapsuleCollider capsule;
    private Rigidbody myRigidBody;
    private bool jumpFlag = false;
    private float currentSpeed = 0;

    private bool CanRunSidestep = true;
    private bool CanJump = true;
    private bool CanRun = true;
    private bool canMove = true;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        myRigidBody.freezeRotation = true;
        myRigidBody.useGravity = true;
=======
    private CharacterController controller;
    private Vector3 velocity = new Vector3();
    private float velocitySpeed = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
>>>>>>> develop:Assets/Scripts/Controller/CharacterMovementController.cs
    }

    private void Update()
    {
        if (GameStateController.Instance.PausedState == PausedStates.Paused) return;

<<<<<<< HEAD:Assets/Scripts/CharacterMovementController.cs
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform == transform.parent)
            transform.parent = null;
    }

    void OnCollisionStay(Collision col)
    {
        TrackGrounded(col);
    }

    void OnCollisionEnter(Collision col)
    {
        TrackGrounded(col);
    }

    private void GetInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && grounded)
            jumpFlag = true;

        Vector3 inputVector = new Vector3();
        if (Input.GetKey(KeyCode.A) && grounded && canMove)
            currentSpeed = Mathf.Lerp(currentSpeed, -MAX_SPEED, Time.deltaTime * walkSpeed);
        else if (Input.GetKey(KeyCode.D) && grounded && canMove)
            currentSpeed = Mathf.Lerp(currentSpeed, MAX_SPEED, Time.deltaTime * walkSpeed);
=======
        if (Input.GetKey(KeyCode.A) && CanMove)
            velocity.x = Mathf.Lerp(velocity.x, -MaxSpeed, Speed);
        else if (Input.GetKey(KeyCode.D) && CanMove)
            velocity.x = Mathf.Lerp(velocity.x, MaxSpeed, Speed);
>>>>>>> develop:Assets/Scripts/Controller/CharacterMovementController.cs
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
