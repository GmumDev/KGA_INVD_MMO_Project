using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerBaseState curState;

	// 초기 상태 설정
	public void Initialize(PlayerBaseState startingState)
	{
		curState = startingState;
		curState.Enter();
	}

	// 상태 전환 (중요: Exit -> Enter 흐름 제어)
	public void ChangeState(PlayerBaseState newState)
	{
		curState.Exit();
		curState = newState;
		curState.Enter();
	}

	// MonoBehaviour의 Update에서 호출될 메서드
	public void Update() => curState?.Update();
	public void FixedUpdate() => curState?.FixedUpdate();
}
