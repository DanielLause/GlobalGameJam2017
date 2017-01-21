using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodlePlattformController : MonoBehaviour
{
    public float PlattformWidth = 0.1f;
    public float SecondsToReset = 1.8f;
    public float SecondsToReact = 0.3f;

    private CharacterMovementController controller;
    private Collider myColl;
    private MeshRenderer meshRenderer;
    private Coroutine timerCoroutine;

    private bool isActive = true;

    private void Awake()
    {
        controller = GameObject.Find("Player").GetComponent<CharacterMovementController>();
        myColl = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();

        myColl.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
            timerCoroutine = StartCoroutine(Timer());
    }

    private void Update()
    {
        if (!isActive) return;

        if (controller.FootPosition.position.y > transform.position.y + (PlattformWidth / 2))
            myColl.enabled = true;
        else
            myColl.enabled = false;
    }

    private void Activate(bool value)
    {
        if (value)
        {
            meshRenderer.enabled = true;
            myColl.enabled = true;
            isActive = true;
        }
        else
        {
            meshRenderer.enabled = false;
            myColl.enabled = false;
            isActive = false;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(SecondsToReact);
        Activate(false);
        yield return new WaitForSeconds(SecondsToReset);
        Activate(true);
    }
}
