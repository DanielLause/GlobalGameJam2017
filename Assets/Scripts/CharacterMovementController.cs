using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovementController : MonoBehaviour
{
    private const float MAX_SPEED = 1.5f;

    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 14.0f;
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

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        myRigidBody.freezeRotation = true;
        myRigidBody.useGravity = true;
    }

    private void Update()
    {
        GetInput();
    }

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            jumpFlag = true;

        Vector3 inputVector = new Vector3();
        if (Input.GetKey(KeyCode.A))
            currentSpeed = Mathf.Lerp(currentSpeed, -MAX_SPEED, Time.deltaTime * walkSpeed);
        else if (Input.GetKey(KeyCode.D))
            currentSpeed = Mathf.Lerp(currentSpeed, MAX_SPEED, Time.deltaTime * walkSpeed);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * walkSpeed * 3);

        currentSpeed = Mathf.Clamp(currentSpeed, -MAX_SPEED, MAX_SPEED);

        inputVector.x = currentSpeed;

        if (grounded)
        {
            var velocityChange = CalculateVelocityChange(inputVector);

            myRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);

            if (CanJump && jumpFlag)
            {
                jumpFlag = false;
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, myRigidBody.velocity.y + CalculateJumpVerticalSpeed(), myRigidBody.velocity.z);
            }

            grounded = false;
        }
        else
        {
            var velocityChange = transform.TransformDirection(inputVector) * inAirControl;
            myRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    private Vector3 CalculateVelocityChange(Vector3 inputVector)
    {
        var relativeVelocity = transform.TransformDirection(inputVector);
        if (inputVector.z > 0)
            relativeVelocity.z *= (CanRun && Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        else
            relativeVelocity.z *= (CanRun && Input.GetKey(KeyCode.LeftShift)) ? runBackwardSpeed : walkBackwardSpeed;
        relativeVelocity.x *= (CanRunSidestep && Input.GetKey(KeyCode.LeftShift)) ? runSidestepSpeed : sidestepSpeed;

        var currRelativeVelocity = myRigidBody.velocity - groundVelocity;
        var velocityChange = relativeVelocity - currRelativeVelocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        return velocityChange;
    }

    private void TrackGrounded(Collision collision)
    {
        var maxHeight = capsule.bounds.min.y + capsule.radius * .9f;
        foreach (var contact in collision.contacts)
        {
            if (contact.point.y < maxHeight)
            {
                if (isKinematic(collision))
                {
                    groundVelocity = collision.rigidbody.velocity;
                    transform.parent = collision.transform;
                }
                else if (isStatic(collision))
                    transform.parent = collision.transform;
                else
                    groundVelocity = Vector3.zero;

                grounded = true;
            }

            break;
        }
    }

    private float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y * 2));
    }

    private bool isKinematic(Collision collision)
    {
        return isKinematic(capsule.transform);
    }

    private bool isKinematic(Transform transform)
    {
        return myRigidBody && myRigidBody.isKinematic;
    }

    private bool isStatic(Collision collision)
    {
        return isStatic(collision.transform);
    }

    private bool isStatic(Transform transform)
    {
        return transform.gameObject.isStatic;
    }
}
