using Unity.Cinemachine;
using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance { get => instance; }


    [SerializeField]
    Camera builtinCamera;

    [SerializeField]
    CinemachineCamera playerFollowCamera;

    [SerializeField]
    CinemachineCamera focusCamera;

    readonly Vector3 followOffset_ofFocusedTalker = new Vector3(0, 0.7f, 2f);
    readonly Vector3 targetOffset_ofFocusedTalker = new Vector3(0, 1f, 0);

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
    }
    // .7 -1 1
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
