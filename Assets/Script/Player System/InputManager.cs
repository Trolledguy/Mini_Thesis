using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input State")]
    public float turnSpeed;
    private bool isUsingComputer = false;
    private bool isOnBoard = false;
    public bool isInspecting = false;

    public CatProfile selectCatProfile;

    void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    void Update()
    {
        if(!isUsingComputer && !isOnBoard)
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
            if(PlayerCamera.Instance.isCameraZooming()) 
                return;

            if (isOnBoard && isInspecting)
            {
                Debug.Log("Reset Cat Profile");
                selectCatProfile.ResetPosition();
                selectCatProfile = null;
                isInspecting = false;
                return;
            }
            else if(isOnBoard && !isInspecting)
            {
                Debug.Log("Back to chair");
                Transform point = FindAnyObjectByType<SceneAnchor>().cameraPoint;
                StartCoroutine(PlayerCamera.Instance.MoveCamera(point,5));
                isOnBoard = false;
                return;
            }
            
            StartCoroutine(PlayerCamera.Instance.AdjustFOV(isUsingComputer, 5f));
            isUsingComputer = !isUsingComputer;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(isOnBoard) return;
            Ray ray = PlayerCamera.Instance.playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit hitInfo,20))
            {
                if(hitInfo.collider.tag == "Cat Board")
                {
                    SceneAnchor anchor = FindAnyObjectByType<SceneAnchor>();
                    StartCoroutine(PlayerCamera.Instance.MoveCamera(anchor.boardCamPoint,5f));
                    isOnBoard = true;
                }
            }
        }
    }

    public static void SetInput(bool _input)
    {
        Instance.isUsingComputer = _input;
    }

    public bool IsUsingComputer()
    {
        return isUsingComputer;
    }


    void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Ray ray = PlayerCamera.Instance.playerCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * 20);
    }
}