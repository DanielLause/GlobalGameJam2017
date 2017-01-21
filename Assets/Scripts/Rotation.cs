using UnityEngine;

public class Rotation : MonoBehaviour {

    public enum RotationDirection { None,Right,Left,Up,Down,AllSidesForward,AllSidesBackward}

    public RotationDirection RotateDirection;
    public float RotationSpeed;
    public bool Paused = false;

    private Transform myTransform;
    private GameStateController gameStateController;

    void Awake()
    {
        myTransform = GetComponent<Transform>();
        gameStateController = GameStateController.Instance;
    }

    void FixedUpdate()
    {
        if (!Paused && (gameStateController.PausedState == PausedStates.UnPause))
        {
            Rotate();
        }
    }


    private void Rotate()
    {
        switch (RotateDirection)
        {
            case RotationDirection.None:
                break;
            case RotationDirection.Right:
                myTransform.Rotate(new Vector3(0, RotationSpeed, 0), Space.Self);
                break;
            case RotationDirection.Left:
                myTransform.Rotate(new Vector3(0, -RotationSpeed, 0), Space.Self);
                break;
            case RotationDirection.Up:
                myTransform.Rotate(new Vector3(RotationSpeed, 0,0), Space.Self);
                break;
            case RotationDirection.Down:
                myTransform.Rotate(new Vector3(-RotationSpeed, 0,0), Space.Self);
                break;
            case RotationDirection.AllSidesForward:
                myTransform.Rotate(new Vector3(RotationSpeed, RotationSpeed, RotationSpeed), Space.Self);
                break;
            case RotationDirection.AllSidesBackward:
                myTransform.Rotate(new Vector3(-RotationSpeed, -RotationSpeed, -RotationSpeed), Space.Self);
                break;
        }
    }
}
