using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{

	ProjectileShooter shooter;
	float mouseSenceX = 100f;
	float speed = 10f;

	bool interactHovering;

	IInteractable target;

	int npcLayerMask = 1 << LayerMask.NameToLayer("NPC");

	public PlayerIdleState(Player player) : base(player) 
	{
		shooter = player.GetComponentInChildren<ProjectileShooter>();
	}

	#region StateMachine
	public override void Enter()
	{
		interactHovering = false;
	}
	public override void Update()
	{
		if (player.fireAction.ReadValue<float>() > 0)
			shooter.Shoot();

		Move();
		Look();
		Interact();
	}
	public override void Exit()
	{
		EventBus.Publish(new PlayerInteractUnTargetedEvent(target));
	}

	public override void FixedUpdate()
	{

	}
	#endregion

	private void Move()
	{
		Vector2 v = player.moveAction.ReadValue<Vector2>();

		Vector3 dir = player.transform.forward * v.y + player.transform.right * v.x;
		dir.y = 0;
		player.transform.position += dir.normalized * speed * Time.deltaTime;
	}
	private void Look()
	{
		var v = player.lookAction.ReadValue<Vector2>();
		player.transform.Rotate(new Vector3(0, v.x, 0) * mouseSenceX * Time.deltaTime);
	}
	private void Interact()
	{
		Debug.DrawRay(player.transform.position, player.transform.forward * 2f, Color.red);
		if (interactHovering && player.interactAction.WasPressedThisFrame() && IsNPCInteractReady())
		{
			target?.OnInteract();
			player.ChangeState(player.WatchState);
		}
		else
		{
			if (IsNPCInteractReady())
			{
				if (interactHovering == false)
                {
                    EventBus.Publish(new PlayerInteractTargetedEvent(target));
                }
				interactHovering = true;
			}
			else
			{
				if (interactHovering == true)
                {
                    EventBus.Publish(new PlayerInteractUnTargetedEvent(target));
                }
				interactHovering = false;
			}
		}
	}

	bool IsNPCInteractReady()
	{
		bool flag = Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hitInfo, 2f, npcLayerMask);

		target = hitInfo.collider?.gameObject.GetComponent<IInteractable>();
		
		return flag && target != null;
	}
}
