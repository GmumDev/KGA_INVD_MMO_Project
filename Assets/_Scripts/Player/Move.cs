using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 10f;

	InputAction moveAction;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
	}

    // Update is called once per frame
    void Update()
    {
		Move_With_InputSystem();
	}
	private void Move_With_InputSystem()
	{
		Vector2 v = moveAction.ReadValue<Vector2>();

		Vector3 dir = transform.forward * v.y + transform.right * v.x;
		dir.y = 0;
		transform.position += dir.normalized * speed * Time.deltaTime;
	}

}
