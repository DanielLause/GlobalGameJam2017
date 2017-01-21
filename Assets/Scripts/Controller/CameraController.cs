using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float HeightAboveToFollow = 1;
    public float DistanceFromToFollow = -10;
    [SerializeField] private Transform ToFollow;
    [SerializeField] private float CameraFollowSpeed = 0.7f;
    [SerializeField] private float CameraLookAtSpeed = 1;

    private Vector3 lookAtPosition;
    private Coroutine faderCoroutine;

    private void Start()
    {
        lookAtPosition = ToFollow.transform.position;
    }

    private void Update()
    {
        Follow();
        LookAt();
    }

    private void Follow()
    {
        Vector3 wantedPos = new Vector3(ToFollow.transform.position.x, ToFollow.transform.position.y + HeightAboveToFollow, ToFollow.transform.position.z + DistanceFromToFollow);
        transform.position = Vector3.Lerp(transform.position, wantedPos, Time.deltaTime * CameraFollowSpeed);
    }

    private void LookAt()
    {
        lookAtPosition = Vector3.Lerp(lookAtPosition, ToFollow.position, Time.deltaTime * CameraLookAtSpeed);
        transform.LookAt(lookAtPosition);
    }
}
