using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour {

    public List<GameObject> ObjectsToDeactivate;
    public InstromentType Type;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Play node at this point.");
            SetActiveAllObjectsInList(false);
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
