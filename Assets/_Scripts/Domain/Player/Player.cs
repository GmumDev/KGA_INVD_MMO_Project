using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private enum State
	{
		Idle
	}
    IInventory inventory;
	State curState;

	FSM<Player> fsm;

	void Start()
    {
        inventory = GetComponent<InventorySystem>();
		fsm = new FSM<Player>(new PlayerIdleState(this));

	}
	private void Update()
	{
		switch(curState)
		{
			case State.Idle:

				break;
		}

		fsm.UpdateState();
	}
}
