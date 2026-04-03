using UnityEngine;

public class Enemy : MonoBehaviour, IDamageTakeable
{
	protected EnemyIds id;
	protected EnemyKilledEvent ev;

	// enemyKilledEvent의 killCnt는 디폴트로 1이지만
	// 이후 enemyManager에서 특정 주기로 publish 할거임
	// 그럼 killCnt가 변함. 

	private void Start()
	{
		ev = new EnemyKilledEvent(id, enemyKilledCnt: 1);
	}


	public void TakeDamage(int damage)
	{
		Debug.Log("Enemy Hit" + damage);
	}

	protected void Die()
	{
		EventBus.Publish<EnemyKilledEvent>(ev);
	}
}
