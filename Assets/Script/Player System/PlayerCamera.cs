using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; }
    [Header("Camera Settings")]
    [SerializeField] public Camera playerCamera;
    private bool isZooming = false;

    void Awake()
    {
        this.SetUp();
    }
    public IEnumerator MoveCamera(MoveDirection direction, float speed)
    {
        if(isZooming) yield break;
        isZooming = true;
        switch (direction)
        {
            case MoveDirection.Left:
                Quaternion targetYawL = Quaternion.Euler(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y - 90, playerCamera.transform.localEulerAngles.z);
                while(true)
                {
                    playerCamera.transform.localEulerAngles = Quaternion.RotateTowards(playerCamera.transform.rotation, targetYawL, speed * Time.deltaTime).eulerAngles;
                    if(Quaternion.Angle(playerCamera.transform.rotation, targetYawL) < 0.1f)
                    {
                        isZooming = false;
                        yield break;
                    }
                    yield return null;
                }
            case MoveDirection.Right:
                Quaternion targetYawR = Quaternion.Euler(playerCamera.transform.localEulerAngles.x, playerCamera.transform.localEulerAngles.y + 90, playerCamera.transform.localEulerAngles.z);
                while(true)
                {
                    playerCamera.transform.localEulerAngles = Quaternion.RotateTowards(playerCamera.transform.rotation, targetYawR, speed * Time.deltaTime).eulerAngles;
                    if(Quaternion.Angle(playerCamera.transform.rotation, targetYawR) < 0.1f)
                    {
                        isZooming = false;
                        yield break;
                    }
                    yield return null;
                }            
        }
        yield return null;
    }

    public IEnumerator AdjustFOV(bool isOnComputer, float speed)
    {
        if(isZooming) yield break;
        isZooming = true;

        if(!isOnComputer) 
        {

            while(true)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 15 , speed * Time.deltaTime);
                if(Mathf.Abs(playerCamera.fieldOfView - 15) < 0.1f)
                {
                    isZooming = false;
                    yield break;
                }
                yield return null;
            }
        }
        else
        {

            while(true)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60, speed * Time.deltaTime);
                if(Mathf.Abs(playerCamera.fieldOfView - 60) < 0.1f)
                {
                    isZooming = false;
                    yield break;
                }
                yield return null;
            }
        }
    }

    public bool isCameraZooming()
    {
        return isZooming;
    }
    public void SetZoom(bool _input)
    {
        isZooming = _input;
        bool ic = InputManager.Instance.IsUsingComputer();
        if(ic)
        {
            playerCamera.fieldOfView = 15;
        }
        else
        {
            playerCamera.fieldOfView = 60;
        }
    }

    public void SetUp()
    {
        if(Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        playerCamera = GetComponent<Camera>();
        Camera.SetupCurrent(playerCamera);
    }
}