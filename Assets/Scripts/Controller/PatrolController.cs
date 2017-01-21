using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PatrolController : MonoBehaviour
{
    [Header("Settings")]
    public List<Transform> WayPoints;
    public float DistanceToSetNewTarget = 0.3f;
    public float MoveSpeed = 5;
    public bool StartPatrolOnAwake = true;
    [Tooltip("On active: the patrol is paused")]
    public bool Paused = false;

    [Header("SlowToEndPoint")]
    public bool ActivateSlow = false;
    public float DistanceToSlow = 5;
    public float SlowMoveSpeed = 2;

    private Vector3 currentTarget;
    private int nextTargetIndex = 1;
    private bool indexForward = true;
    private float startMoveSpeed;
    private Transform myTransform;
    private Rigidbody myRigid;
    private List<Vector3> targets;

    void Awake()
    {
        myTransform = GetComponent<Transform>();
        myRigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startMoveSpeed = MoveSpeed;
        SetWayPoints();
        currentTarget = GetNextTarget();

        if (StartPatrolOnAwake)
            StartPatrol();
    }

    public void StartPatrol()
    {
        StartCoroutine(PatrolWithoutLerp());
    }

    private Vector3 GetNextTarget()
    {
        var currentTarget = nextTargetIndex;

        if (indexForward)
        {
            this.currentTarget = targets[nextTargetIndex];

            if (nextTargetIndex + 1 < targets.Count)
                nextTargetIndex += 1;
            else
                indexForward = !indexForward;

            return this.currentTarget;
        }
        else
        {
            this.currentTarget = targets[nextTargetIndex];

            if (nextTargetIndex - 1 >= 0)
                nextTargetIndex -= 1;
            else
                indexForward = !indexForward;
            return this.currentTarget;
        }
    }

    private void SetWayPoints()
    {
        if (WayPoints.Count == 0)
        {
            Debug.LogError(string.Format("WayPoints list empty on: {0}", gameObject.name));
            return;
        }

        List<Transform> points = new List<Transform>();

        foreach (var wayPoint in WayPoints)
            points.Add(wayPoint);

        targets = new List<Vector3>();
        Vector3 myStartTransform = myTransform.position;
        targets.Add(myStartTransform);

        foreach (var point in points)
            targets.Add(point.position);
    }

    private IEnumerator PatrolWithoutLerp()
    {
        if (!Paused)
        {
            var distance = CheckDistanceToTarget(currentTarget);

            if (ActivateSlow)
            {
                if (distance < DistanceToSlow)
                {
                    MoveSpeed = Mathf.Lerp(MoveSpeed, SlowMoveSpeed, 0.05f);
                }
                else if (distance > DistanceToSlow && DistanceToSlow != 0)
                {
                    MoveSpeed = Mathf.Lerp(MoveSpeed, startMoveSpeed, 0.05f);
                }
            }

            if (distance > DistanceToSetNewTarget)
            {
                var heading = currentTarget - myTransform.position;
                var d = heading.magnitude;
                var direction = heading / d;
                myRigid.MovePosition(myTransform.position + direction * MoveSpeed * Time.deltaTime);
            }
            else if (distance <= DistanceToSetNewTarget)
            {
                currentTarget = GetNextTarget();

                var heading = currentTarget - myTransform.position;
                var d = heading.magnitude;
                var direction = heading / d;
                myRigid.MovePosition(myTransform.position + direction * MoveSpeed * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(PatrolWithoutLerp());
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(PatrolWithoutLerp());
        }

    }

    private float CheckDistanceToTarget(Vector3 patrolTarget)
    {
        return Vector3.Distance(myTransform.position, patrolTarget);
    }
}

