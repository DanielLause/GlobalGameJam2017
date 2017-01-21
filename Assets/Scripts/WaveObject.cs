using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaveObject : MonoBehaviour
{
    public GameObject WaveParticleObject;
    public float PushPower = 650;
    public float PushRadius = 4;
    public float ResetDelay = 3;

    private bool isAbleToPush = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (isAbleToPush)
        {
            AddExplosionForce();
            InstantiateWave();
            StartCoroutine(Timer());
        }
    }

    private void AddExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, PushRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(PushPower, transform.position, PushRadius, 3.0F);
        }
    }

    private void InstantiateWave()
    {
        if (WaveParticleObject == null) return;

        GameObject wave = Instantiate(WaveParticleObject, transform.position, Quaternion.identity) as GameObject;
        Destroy(wave, 8);
    }

    IEnumerator Timer()
    {
        isAbleToPush = false;
        yield return new WaitForSeconds(ResetDelay);
        isAbleToPush = true;
    }
}

