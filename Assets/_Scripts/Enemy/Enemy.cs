using UnityEngine;

public class Enemy : MonoBehaviour, IDamageTakeable
{
	void IDamageTakeable.TakeDamage(int damage)
	{
		Debug.Log("Enemy Hit" + damage);
	}
}
