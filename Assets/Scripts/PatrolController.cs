using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PatrolController : MonoBehaviour
{
    [Header("Settings")]
    public List<Transform> WayPoints;
    public float DistanceToTarget = 0.3f;
    public float MoveSpeed = 0;
    public bool StartPatrolOnAwake = false;
    [Tooltip("On active: the patrol is paused")]
    public bool Paused = false;

    private Vector3 currentTarget;
    private int nextTargetIndex = 1;
    private bool indexForward = true;
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
        SetWayPoints();
        currentTarget = GetNextTarget();

        if (StartPatrolOnAwake)
            StartPatrol();
    }

    public void StartPatrol()
    {
        StartCoroutine(Patrol());
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
            print(nextTargetIndex);
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

    private IEnumerator Patrol()
    {
        if (!Paused)
        {
            var distance = CheckDistanceToTarget(currentTarget);

            if (distance > DistanceToTarget)
            {
                var heading = currentTarget - myTransform.position;
                var d = heading.magnitude;
                var direction = heading / d; // This is now the normalized direction.
                myRigid.MovePosition(myTransform.position + direction * MoveSpeed * Time.deltaTime);
            }
            else if (distance <= DistanceToTarget)
            {
                currentTarget = GetNextTarget();

                var heading = currentTarget - myTransform.position;
                var d = heading.magnitude;
                var direction = heading / d; // This is now the normalized direction.
                myRigid.MovePosition(myTransform.position + direction * MoveSpeed * Time.deltaTime);
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(Patrol());
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(Patrol());
        }

    }

    private float CheckDistanceToTarget(Vector3 patrolTarget)
    {
        return Vector3.Distance(myTransform.position, patrolTarget);
    }
}

