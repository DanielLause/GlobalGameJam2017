using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaveObject : MonoBehaviour
{
    public GameObject WaveParticleObject;
    public float PushPower = 170;
    public float PushRadius = 1.5f;
    public float ResetDelay = 3;

    private bool isAbleToPush = true;
    private CharacterMovementController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CharacterMovementController>();
    }

    private void Update()
    {
        if (!isAbleToPush) return;

        if (Vector3.Distance(player.transform.position, transform.position) <= PushRadius)
        {
            AddExplosionForce(player.transform);
            InstantiateWave();
            StartCoroutine(Timer());
        } 
    }

    private void AddExplosionForce(Transform other)
    {
        CharakterControllerExplosionForce impactReceiver = other.transform.GetComponent<CharakterControllerExplosionForce>();

        if (impactReceiver != null)
        {
            Vector3 dir = other.position - transform.position;
            impactReceiver.AddImpact(dir, PushPower);
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

