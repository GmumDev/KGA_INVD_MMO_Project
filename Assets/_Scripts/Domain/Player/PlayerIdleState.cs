using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseState<Player>, IInteractor
{
	ProjectileShooter shooter;

	InputAction fireAction;
	InputAction lookAction;

	public float mouseSenceX;
	public PlayerIdleState(Player owner) : base(owner) 
	{

		shooter = owner.GetComponentInChildren<ProjectileShooter>();

		fireAction = InputSystem.actions.FindAction("Fire");
		lookAction = InputSystem.actions.FindAction("Look");

	}
	public override void OnStateEnter()
	{

	}

	public override void OnStateExit()
	{

	}

	public override void OnStateUpdate()
	{

		if (fireAction.ReadValue<float>() > 0)
			shooter.Shoot();

		var v = lookAction.ReadValue<Vector2>();
		owner.transform.Rotate(new Vector3(0, v.x, 0) * mouseSenceX * Time.deltaTime);
	}


	public void Interact(IInteractable target)
	{
		InteractionType type = target.OnInteract();
		
	}
}
