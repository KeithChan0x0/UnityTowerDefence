using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : MonoBehaviour
{
	public string enemyTag = "Enemy";
	public int damage;
	Rigidbody rb;


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(enemyTag))
		{
			EnemyBase enemyClass = collision.gameObject.GetComponent<EnemyBase>();
			if (enemyClass)
			{
				enemyClass.Damage(damage);
				// TODO: エフェクト
				// TODO: SE
				Debug.Log("OK");
			}
			else
			{
				Debug.LogWarning("EnemyBaseクラス見つからない");
			}
			gameObject.SetActive(false);
		}
	}
}
