using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] CinemachineCamera freeCam;
	[SerializeField] CinemachineCamera tpsCam;

	InputAction freeLookAction;
	bool isFreeLook = false;
	private void Start()
	{
		//freeLookAction = InputSystem.actions.FindAction("FreeLook");
		//SetFreeLookActive(false);

		//freeLookAction.started += (x) => SetFreeLookActive(true);
		//freeLookAction.canceled += (x) => SetFreeLookActive(false);
	
	}
	private void SetFreeLookActive(bool value)
	{
		isFreeLook = value;
		if(isFreeLook)
		{
			freeCam.Priority = 2;
			tpsCam.Priority = 1;
		}
		else
		{
			freeCam.Priority = 1;
			tpsCam.Priority = 2;

		}
	}
	private void Update()
	{
		
	}
}
