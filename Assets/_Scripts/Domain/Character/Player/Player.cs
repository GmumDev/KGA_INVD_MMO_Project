using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class Player : MonoBehaviour
{
	IInventory inventory;
	private PlayerStateMachine stateMachine;
	public PlayerIdleState IdleState { get; private set; }
	public PlayerWatchState WatchState { get; private set; }



	public InputAction fireAction { get; private set; }
	public InputAction lookAction { get; private set; }
	public InputAction moveAction { get; private set; }
	public InputAction interactAction { get; private set; }

	private void Awake()
	{
		stateMachine = new PlayerStateMachine();
		IdleState = new PlayerIdleState(this);
		WatchState = new PlayerWatchState(this);

		fireAction = InputSystem.actions.FindAction("Fire");
		moveAction = InputSystem.actions.FindAction("Move");
		lookAction = InputSystem.actions.FindAction("Look");
		interactAction = InputSystem.actions.FindAction("Interact");

		inventory = GetComponent<InventorySystem>();

	}
	private void Start() => stateMachine.Initialize(IdleState);
	private void Update() => stateMachine.Update();
	private void FixedUpdate() => stateMachine.FixedUpdate();
	public void ChangeState(PlayerBaseState newState) => stateMachine.ChangeState(newState);

    private void OnDestroy()
    {
		stateMachine.curState.Exit();
    }
}
