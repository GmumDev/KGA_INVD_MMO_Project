using UnityEngine;

public class Projectile_Simple : Projectile
{
	protected override void OnApplyDamage()
	{
		DestroySelf();
	}
}
