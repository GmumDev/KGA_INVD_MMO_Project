using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWatchState : PlayerBaseState
{
	public static event Action OnNextNodeRequested;
	public PlayerWatchState(Player player) : base(player) 
	{

	}


	void HandleWatchingFinished()
	{
		player.ChangeState(player.IdleState);
	}
	public override void Enter()
	{
		ScenarioManager.Instance.OnScenarioEnded += HandleWatchingFinished;
	}
	public override void Exit()
	{
		ScenarioManager.Instance.OnScenarioEnded -= HandleWatchingFinished;
	}
	public override void FixedUpdate()
	{

	}

	public override void Update()
	{
		if(player.interactAction.WasPressedThisFrame())
		{
			OnNextNodeRequested?.Invoke();
		}
	}
}
