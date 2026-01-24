using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input State")]
    public float turnSpeed;
    private bool isUsingComputer = false;

    void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    void Update()
    {
        if(!isUsingComputer)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(PlayerCamera.Instance.MoveCamera(MoveDirection.Left, turnSpeed));
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(PlayerCamera.Instance.MoveCamera(MoveDirection.Right, turnSpeed));
            }
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(PlayerCamera.Instance.isCameraZooming()) return;
            Debug.Log("Interact");
            StartCoroutine(PlayerCamera.Instance.AdjustFOV(isUsingComputer, 5f));
            isUsingComputer = !isUsingComputer;
        }
    }

    public bool IsUsingComputer()
    {
        return isUsingComputer;
    }
}