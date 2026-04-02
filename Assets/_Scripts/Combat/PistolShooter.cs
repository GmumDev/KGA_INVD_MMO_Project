using UnityEngine;

public class PistolShooter : ProjectileShooter
{
	int shootStep;

	protected override void Start()
	{
		base.Start();
		shootStep = 0;
	}
	public override void Shoot()
	{
		if (CanShoot() == false) return;
		lastShootTime = Time.time;

		var projectile = _Pool.Get();
		projectile.dir = transform.forward;
		projectile.gameObject.transform.position = projectileSpawnPoint.position;
		projectile.speed = 4f;
		projectile.isRunning = true;
	}

	protected override Projectile CreateProjectile()
	{
		shootStep++;
		shootStep %= projectilePrefabs.Length;

		Projectile projectile = Instantiate(projectilePrefabs[shootStep]).GetComponent<Projectile>();
		projectile.SetManagedPool(_Pool);

		return projectile;
	}

	protected override void OnGetProjectile(Projectile projectile)
	{
		projectile.gameObject.SetActive(true);
	}

	protected override void OnReleaseProjectile(Projectile projectile)
	{
		projectile.gameObject.SetActive(false);
	}

	protected override void OnDestroyProjectile(Projectile projectile)
	{
		Destroy(projectile.gameObject);
	}
}
