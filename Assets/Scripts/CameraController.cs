using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ToFollow;
    [SerializeField] private float CameraFollowSpeed = 0.7f;
    [SerializeField] private float HightAboveToFollow = 1;
    [SerializeField] private float DistanceFromToFollow = -10;
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

    public void Fade()
    {
        if (faderCoroutine != null)
        {
            StopCoroutine(faderCoroutine);
            // To Do Reset Fading prop
        }

        StartCoroutine(Fader());

    }

    private void Follow()
    {
        Vector3 wantedPos = new Vector3(ToFollow.transform.position.x, ToFollow.transform.position.y + HightAboveToFollow, ToFollow.transform.position.z + DistanceFromToFollow);
        transform.position = Vector3.Lerp(transform.position, wantedPos, Time.deltaTime * CameraFollowSpeed);
    }

    private void LookAt()
    {
        lookAtPosition = Vector3.Lerp(lookAtPosition, ToFollow.position, Time.deltaTime * CameraLookAtSpeed);
        transform.LookAt(lookAtPosition);
    }

    private IEnumerator Fader()
    {
        yield return new WaitForFixedUpdate();
        // To Do Set Fader Prop regarding to time
    }
}
