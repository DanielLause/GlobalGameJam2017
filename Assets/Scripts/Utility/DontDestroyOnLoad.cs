using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    void Awake()
    {
        if (!GameStateController.Instance.isOnDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
            GameStateController.Instance.isOnDestroy = true;
        }
    }
}
