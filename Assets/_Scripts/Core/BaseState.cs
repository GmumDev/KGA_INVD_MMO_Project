using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseState<T>
{
	protected T owner;
	protected BaseState(T owner)
	{
		this.owner = owner;
	}

	public abstract void OnStateEnter();
	public abstract void OnStateUpdate();
	public abstract void OnStateExit();
}
