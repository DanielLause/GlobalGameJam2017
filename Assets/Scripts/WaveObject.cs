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

    public AudioClip[] Sounds;

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
            PlaySound();
            StartCoroutine(Timer());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Camera.main.GetComponent<CameraController>().Present(transform.position, 4);
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

    private void PlaySound()
    {
        if (Sounds.Length != 0)
        {
            AudioClip toPlay = Sounds[Random.Range(0, Sounds.Length + 1)];
        }
    }

    IEnumerator Timer()
    {
        isAbleToPush = false;
        yield return new WaitForSeconds(ResetDelay);
        isAbleToPush = true;
    }
}

