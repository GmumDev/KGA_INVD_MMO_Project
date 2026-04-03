using UnityEngine;

public abstract class PlayerBaseState
{
	protected Player player;
	public PlayerBaseState(Player player) { this.player = player; }
    public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
	public abstract void FixedUpdate();

}
