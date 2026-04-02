using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ProjectileShooter: MonoBehaviour
{
	protected IObjectPool<Projectile> _Pool;

	public Transform projectileSpawnPoint;
	public GameObject[] projectilePrefabs;
	public float MaxCooldown;

	protected float lastShootTime;

	protected virtual void Start()
	{
		Debug.Log("Pool applied");
		_Pool = new ObjectPool<Projectile>(
			createFunc: CreateProjectile,
			actionOnGet: OnGetProjectile,
			actionOnRelease: OnReleaseProjectile,
			actionOnDestroy: OnDestroyProjectile, true, 5, 5);
	}


	protected abstract Projectile CreateProjectile();
	protected abstract void OnGetProjectile(Projectile projectile);
	protected abstract void OnReleaseProjectile(Projectile projectile);
	protected abstract void OnDestroyProjectile(Projectile projectile);




	public virtual bool CanShoot() => Time.time - lastShootTime > MaxCooldown;
	public abstract void Shoot();
}
