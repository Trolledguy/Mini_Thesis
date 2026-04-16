using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input State")]
    private bool isUsingComputer = false;
    private bool isOnBoard = false;
    public bool isInspecting = false;
    [Header("Sound")]
    public AudioClip clickSound;

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
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(PlayerCamera.Instance.isCameraZooming()) 
                return;

            if (isOnBoard && isInspecting)
            {
                selectCatProfile.ResetPosition();
                selectCatProfile = null;
                isInspecting = false;
                return;
            }
            else if(isOnBoard && !isInspecting)
            {
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
            StartCoroutine(Sound.PlaySoundAtPoint(clickSound, this.transform.position));
            if(isOnBoard || isUsingComputer) return;
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
        Instance.isOnBoard = false;
    }

    public bool IsUsingComputer()
    {
        return isUsingComputer;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Camera.main == null) return;
        if(!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Ray ray = PlayerCamera.Instance.playerCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * 20);
    }
#endif
}