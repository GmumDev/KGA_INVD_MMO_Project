using System;
using UnityEngine;

public class PlayerWatchState : PlayerBaseState
{
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

	}
}
