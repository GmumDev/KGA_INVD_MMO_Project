using UnityEngine;

public class FSM<T>
{

	private BaseState<T> curState;

	public FSM(BaseState<T> initState)
	{
		this.curState = initState;
	}

	public void ChangeState(BaseState<T> nextState)
	{
		if (nextState == curState) return;

		if (curState != null)
			curState.OnStateExit();

		curState = nextState;
		curState.OnStateEnter();
	}

	public void UpdateState()
	{
		if(curState != null)
			curState.OnStateUpdate();
	}
}
