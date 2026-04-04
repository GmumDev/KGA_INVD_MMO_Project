using UnityEngine;

public class Enemy : MonoBehaviour, IDamageTakeable
{
	[SerializeField]
	protected EnemyIds id;
	protected EnemyKilledEvent ev;

	private void Start()
	{
		ev = new EnemyKilledEvent(id, enemyKilledCnt: 1);
	}


	public void TakeDamage(int damage)
	{
		Debug.Log("Enemy Hit" + damage);
        EventBus.Publish<EnemyKilledEvent>(ev); // 
    }

	protected void Die()
	{
		EventBus.Publish<EnemyKilledEvent>(ev);
	}
}
