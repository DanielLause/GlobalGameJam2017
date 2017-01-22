using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INIT : MonoBehaviour
{

    public GameObject GameController;

    void Awake()
    {
        if (GameStateController.Instance == null)
        {
            GameObject gameController = new GameObject();
            gameController = Instantiate(GameController);
            gameController.name = "GameController";

            DontDestroyOnLoad(this.gameObject);
        }
    }
}
