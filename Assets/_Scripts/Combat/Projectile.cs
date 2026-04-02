using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    TagEnums[] targetTags;


    public int damage;
    public Vector3 dir;
    public float speed;
    public bool isRunning;


	private void Update()
	{
        transform.position += dir * speed * Time.deltaTime;
	}
	void OnCollisionEnter(Collision collision)
	{
        TagEnums enumTag;
        Enum.TryParse(collision.gameObject.tag, out enumTag);
		if(targetTags.Contains(enumTag))
        {
            var obj = collision.gameObject.GetComponent<IDamageTakeable>();
            obj.TakeDamage(damage);
			OnApplyDamage();
        }
	}

    protected abstract void OnApplyDamage();
}
