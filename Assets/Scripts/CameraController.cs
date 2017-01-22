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
    private bool isPresenting = false;

    private Coroutine presentTimerC;
    private Coroutine presentingUpdateC;

    private void Start()
    {
        lookAtPosition = ToFollow.transform.position;
    }

    private void Update()
    {
        if (!isPresenting)
        {
            Follow();
            LookAt();
        }
    }

    public void Present(Vector3 target, float duration)
    {
        if (presentingUpdateC != null)
            StopCoroutine(presentingUpdateC);

        if (presentTimerC != null)
            StopCoroutine(presentTimerC);

        presentTimerC = StartCoroutine(PresentTimer(duration));
        presentingUpdateC = StartCoroutine(PresentingUpdate(target));
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

    private IEnumerator PresentingUpdate(Vector3 target)
    {
        yield return new WaitForFixedUpdate();

        Vector3 wantedPos = new Vector3(target.x, target.y + HeightAboveToFollow, target.z + DistanceFromToFollow);
        transform.position = Vector3.Lerp(transform.position, wantedPos, Time.deltaTime * CameraFollowSpeed);

        lookAtPosition = Vector3.Lerp(lookAtPosition, target, Time.deltaTime * CameraLookAtSpeed);
        transform.LookAt(lookAtPosition);

        if (isPresenting)
            presentingUpdateC = StartCoroutine(PresentingUpdate(target));
    }

    private IEnumerator PresentTimer(float duration)
    {
        isPresenting = true;
        yield return new WaitForSeconds(duration);
        isPresenting = false;
    }
}
