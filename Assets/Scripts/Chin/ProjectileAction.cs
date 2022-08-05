using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAction : MonoBehaviour
{
	public string enemyTag = "Enemy";
	public int damage;

	private void Start()
	{
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(enemyTag))
		{
			HandleCollisionWithEnemy(collision.gameObject);
		}
		else if(collision.gameObject.CompareTag(enemyTag))
		{
			HandleCollisionWithEnvironment(collision.gameObject);
		}

	}

	void HandleCollisionWithEnemy(GameObject enemy)
	{
		EnemyBase enemyClass = enemy.GetComponent<EnemyBase>();
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

	void HandleCollisionWithEnvironment(GameObject environemnt)
	{
		gameObject.SetActive(false);
	}
}
