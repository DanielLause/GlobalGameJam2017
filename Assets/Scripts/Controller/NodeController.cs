using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{

    public List<GameObject> ObjectsToDeactivate;
    public InstromentType Type;
    public float DistanceToPlayer = 1;

    private Transform playerTransform;
    private Transform myTransform;
    BackgroundMusicController backGroundMusicController;

    void Awake()
    {
        myTransform = GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").transform;
        backGroundMusicController = BackgroundMusicController.Instance;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(myTransform.position, playerTransform.position);

        if (distance <= DistanceToPlayer)
        {
            backGroundMusicController.PlayPickUpSound(Type);
            SetActiveAllObjectsInList(false);
            myTransform.gameObject.SetActive(false);
        }
    }

    private void SetActiveAllObjectsInList(bool enable)
    {
        if (ObjectsToDeactivate == null)
        {
            Debug.LogError(string.Format("ObjectsToDeactivate is null on {0}", gameObject.name));
            return;
        }

        foreach (var objectToDeactivate in ObjectsToDeactivate)
        {
            objectToDeactivate.SetActive(enable);
        }
    }


}
