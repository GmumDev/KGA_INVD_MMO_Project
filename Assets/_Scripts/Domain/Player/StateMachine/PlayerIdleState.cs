using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState, IInteractableState
{
	ProjectileShooter shooter;

	InputAction fireAction;
	InputAction lookAction;
	InputAction interactAction;

	public float mouseSenceX = 100f;

	public PlayerIdleState(Player player) : base(player) 
	{
		shooter = player.GetComponentInChildren<ProjectileShooter>();

		fireAction = InputSystem.actions.FindAction("Fire");
		lookAction = InputSystem.actions.FindAction("Look");
		interactAction = InputSystem.actions.FindAction("Interact");
	}
	public override void Enter()
	{

	}
	bool IsNPCInteractReady()
	{
		return Physics.Raycast(player.transform.position, player.transform.forward, 2f, 1 << LayerMask.NameToLayer("NPC"));
	}
	public bool isNowInteract()
	{
		return IsNPCInteractReady() && interactAction.IsPressed();
	}
	public override void Update()
	{
		if (fireAction.ReadValue<float>() > 0)
			shooter.Shoot();

		var v = lookAction.ReadValue<Vector2>();
		player.transform.Rotate(new Vector3(0, v.x, 0) * mouseSenceX * Time.deltaTime);

		Debug.DrawRay(player.transform.position, player.transform.forward*2f, Color.red);
		if(IsNPCInteractReady())
		{
			Debug.Log("NPC focused On");
			// pop Interact key(F) that let know user can interact
		}
		else
		{
			// set active false
		}
	}
	public override void Exit()
	{

	}

	public override void FixedUpdate()
	{

	}

}
