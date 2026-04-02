using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(QuestManager))]
public class Player : MonoBehaviour
{
    IInventory inventory;

    ProjectileShooter shooter;

	InputAction fireAction;
	InputAction lookAction;

	public float mouseSenceX;

	void Start()
    {
        inventory = GetComponent<InventorySystem>();
		shooter = GetComponentInChildren<ProjectileShooter>();

		fireAction = InputSystem.actions.FindAction("Fire");
		lookAction = InputSystem.actions.FindAction("Look");
	}
	private void Update()
	{
		if (fireAction.ReadValue<float>() > 0)
			shooter.Shoot();

		var v = lookAction.ReadValue<Vector2>();
		transform.Rotate(new Vector3(0, v.x, 0) * mouseSenceX * Time.deltaTime);

	}
}
