using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager: MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance { get => instance; }


    [SerializeField]
    Camera builtinCamera;

    [SerializeField]
    CinemachineCamera playerFollowCamera;
    CinemachineFollow _follow;
    public float zoomSpd;
    public float zoomLerpSpd;
    float targetZoom = 5.5f;

    [SerializeField]
    CinemachineCamera focusCamera;

    InputAction scrollAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        playerFollowCamera.Prioritize();
        scrollAction = InputSystem.actions.FindAction("Scroll");

        _follow = playerFollowCamera.gameObject.GetComponent<CinemachineFollow>();
    }
    private void Update()
    {
        if (playerFollowCamera.IsLive)
        {
            Vector2 scroll = scrollAction.ReadValue<Vector2>();
            targetZoom = Mathf.Clamp(targetZoom - scroll.y * zoomSpd * Time.deltaTime, -1f, 5.5f);

            var newzoom = Mathf.Lerp(_follow.FollowOffset.y, targetZoom, zoomLerpSpd * Time.deltaTime);
            _follow.FollowOffset.y = newzoom;
        }
    }
    public void FocusTalker(Transform target)
    {
        focusCamera.Target.TrackingTarget = target;

        focusCamera.Prioritize();

        builtinCamera.cullingMask ^= 1 << LayerMask.NameToLayer("Player");

    }
    public void ResetCameraTarget()
    {

        playerFollowCamera.Prioritize();

        builtinCamera.cullingMask ^= 1 << LayerMask.NameToLayer("Player");

    }
}
