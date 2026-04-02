using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ProjectileShooter: MonoBehaviour
{

	public Transform projectileSpawnPoint;
	public GameObject[] projectilePrefabs;
	public float MaxCooldown;

	protected float lastShootTime;


	public virtual bool CanShoot() => Time.time - lastShootTime > MaxCooldown;
	public abstract void Shoot();
}
