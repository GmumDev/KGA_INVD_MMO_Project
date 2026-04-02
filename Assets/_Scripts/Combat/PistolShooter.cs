using UnityEngine;

public class PistolShooter : ProjectileShooter
{
	int shootStep;

	void Start()
	{
		shootStep = 0;
	}
	public override void Shoot()
	{
		if (CanShoot() == false) return;
		lastShootTime = Time.time;

		shootStep++;
		shootStep %= projectilePrefabs.Length;

		Projectile projectile = Instantiate(projectilePrefabs[shootStep]).GetComponent<Projectile>();

		projectile.dir = transform.forward;
		projectile.gameObject.transform.position = projectileSpawnPoint.position;
		projectile.speed = 4f;
		projectile.isRunning = true;
	}
}
