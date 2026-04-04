using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWatchState : PlayerBaseState
{
	public static event Action OnNextNodeRequested;

	SubscriptionToken token;

	public PlayerWatchState(Player player) : base(player) 
	{

	}


	void HandleWatchingFinished(ScenarioFinishedEvent ev)
	{
		player.ChangeState(player.IdleState);
	}
	public override void Enter()
	{
        token = EventBus.Subscribe<ScenarioFinishedEvent>(HandleWatchingFinished);
	}
	public override void Exit()
    {
        EventBus.Unsubscribe(token);
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
