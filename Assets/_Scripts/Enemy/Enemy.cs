using UnityEngine;

public class Enemy : MonoBehaviour, IDamageTakeable
{
	protected EnemyKilledEvent ev;

    public void TakeDamage(int damage)
	{
		Debug.Log("Enemy Hit" + damage);
	}

	protected void Die()
	{
		EventBus.Publish<EnemyKilledEvent>(ev);
	}
}
