using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform Start;
    public Transform End;

    public float DistanceToPortalToActivate;

    private CharacterMovementController player;
    private CameraController cameraController;
    private bool isAbleToActivate = true;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CharacterMovementController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if ((Vector3.Distance(player.transform.position, Start.position) <= DistanceToPortalToActivate) && isAbleToActivate)
        {
            player.CanMove = false;
            isAbleToActivate = false;
            StartCoroutine(Delay());
            GameUIController.Instance.Fade();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        player.transform.position = End.position;
        cameraController.transform.position = new Vector3(player.transform.position.x, player.transform.position.y * cameraController.HeightAboveToFollow, player.transform.position.z + cameraController.DistanceFromToFollow);
        cameraController.transform.LookAt(player.transform.position);
        player.CanMove = true;
        isAbleToActivate = true;
        GameUIController.Instance.Fade();
    }
}
